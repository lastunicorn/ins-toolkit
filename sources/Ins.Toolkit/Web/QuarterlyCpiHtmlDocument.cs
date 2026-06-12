using System.Globalization;
using HtmlAgilityPack;

namespace DustInTheWind.Ins.Toolkit.Web;

internal sealed class QuarterlyCpiHtmlDocument : IDisposable, IAsyncDisposable
{
	private readonly CultureInfo cultureInfo = new("en-US");
	private readonly Stream stream;

	public QuarterlyCpiHtmlDocument(Stream stream)
	{
		this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
	}

	public IEnumerable<QuarterlyInflationRecord> EnumerateCpiRecords()
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.Load(stream);

		HtmlNodeCollection trNodes = htmlDocument.DocumentNode.SelectNodes("//article//table/tbody/tr");

		foreach (HtmlNode trNode in trNodes)
		{
			QuarterlyInflationRecord yearlyInflationRecord = GetCpiRecord(trNode);

			if (yearlyInflationRecord != null)
				yield return yearlyInflationRecord;
		}
	}

	private QuarterlyInflationRecord GetCpiRecord(HtmlNode trNode)
	{
		HtmlNodeCollection divNodes = trNode.SelectNodes("td/div");

		if (divNodes.Count == 4)
		{
			string quarterAsString = divNodes[0].InnerText.Trim();
			string valueAsString = divNodes[2].InnerText.Trim();

			if (string.IsNullOrWhiteSpace(quarterAsString) && string.IsNullOrWhiteSpace(valueAsString))
				return null;

			YearQuarter yearQuarter = YearQuarter.Parse(quarterAsString);
			decimal value = decimal.Parse(valueAsString, cultureInfo) - 100;

			QuarterlyInflationRecord quarterlyInflationRecord = new()
			{
				Quarter = yearQuarter,
				Value = value
			};

			return quarterlyInflationRecord;
		}

		return null;
	}

	public void Dispose()
	{
		stream?.Dispose();
	}

	public async ValueTask DisposeAsync()
	{
		if (stream != null) await stream.DisposeAsync();
	}
}