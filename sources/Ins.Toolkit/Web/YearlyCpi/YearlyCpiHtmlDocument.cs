using System.Globalization;
using HtmlAgilityPack;

namespace DustInTheWind.Ins.Toolkit.Web.YearlyCpi;

internal sealed class YearlyCpiHtmlDocument : IDisposable, IAsyncDisposable
{
	private readonly Stream stream;

	public YearlyCpiHtmlDocument(Stream stream)
	{
		this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
	}

	public IEnumerable<YearlyCpiRecord> EnumerateRecords()
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.Load(stream);

		HtmlNodeCollection trNodes = htmlDocument.DocumentNode.SelectNodes("//article//table/tbody/tr");

		foreach (HtmlNode trNode in trNodes)
		{
			YearlyCpiRecord yearlyCpiRecord = GetCpi(trNode);

			if (yearlyCpiRecord != null)
				yield return yearlyCpiRecord;
		}
	}

	private YearlyCpiRecord GetCpi(HtmlNode trNode)
	{
		HtmlNodeCollection divNodes = trNode.SelectNodes("td/div");

		if (divNodes.Count == 3)
		{
			string yearAsString = divNodes[0].InnerText;
			string valueAsString = divNodes[1].InnerText;

			int year = int.Parse(yearAsString, CultureInfo.InvariantCulture);
			decimal value = FlexibleDecimal.Parse(valueAsString);

			YearlyCpiRecord yearlyCpiRecord = new()
			{
				Year = year,
				Value = value
			};

			return yearlyCpiRecord;
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