using System.Globalization;
using HtmlAgilityPack;

namespace DustInTheWind.Ins.Toolkit.Web;

internal sealed class QuarterlyInflationHtmlDocument : IDisposable, IAsyncDisposable
{
	private readonly CultureInfo cultureInfo = new("ro-RO");
	private readonly Stream stream;

	public QuarterlyInflationHtmlDocument(Stream stream)
	{
		this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
	}

	public IEnumerable<QuarterlyInflationRecord> EnumerateInflationRecords()
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.Load(stream);

		HtmlNodeCollection trNodes = htmlDocument.DocumentNode.SelectNodes("//article//table/tbody/tr");

		foreach (HtmlNode trNode in trNodes)
		{
			QuarterlyInflationRecord yearlyInflationRecord = GetInflationRecord(trNode);

			if (yearlyInflationRecord != null)
				yield return yearlyInflationRecord;
		}
	}

	private QuarterlyInflationRecord GetInflationRecord(HtmlNode trNode)
	{
		HtmlNodeCollection divNodes = trNode.SelectNodes("td/div");

		if (divNodes.Count == 3)
		{
			string yearAsString = divNodes[0].InnerText;
			string valueAsString = divNodes[1].InnerText;

			int year = int.Parse(yearAsString, cultureInfo);
			decimal value = decimal.Parse(valueAsString, cultureInfo) - 100;

			QuarterlyInflationRecord quarterlyInflationRecord = new()
			{
				Quarter = year,
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