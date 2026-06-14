using HtmlAgilityPack;

namespace DustInTheWind.Ins.Toolkit.Web.QuarterlyCpi;

internal sealed class QuarterlyCpiHtmlDocument : IDisposable, IAsyncDisposable
{
	private readonly Stream stream;

	public QuarterlyCpiHtmlDocument(Stream stream)
	{
		this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
	}

	public IEnumerable<QuarterlyCpiRecord> EnumerateRecords()
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.Load(stream);

		HtmlNodeCollection trNodes = htmlDocument.DocumentNode.SelectNodes("//article//table/tbody/tr");

		foreach (HtmlNode trNode in trNodes)
		{
			QuarterlyCpiRecord yearlyCpiRecord = GetCpiRecord(trNode);

			if (yearlyCpiRecord != null)
				yield return yearlyCpiRecord;
		}
	}

	private QuarterlyCpiRecord GetCpiRecord(HtmlNode trNode)
	{
		HtmlNodeCollection divNodes = trNode.SelectNodes("td/div");

		if (divNodes.Count == 4)
		{
			string quarterAsString = divNodes[0].InnerText.Trim();
			string valueAsString = divNodes[2].InnerText.Trim();

			if (string.IsNullOrWhiteSpace(quarterAsString) && string.IsNullOrWhiteSpace(valueAsString))
				return null;

			YearQuarter yearQuarter = YearQuarter.Parse(quarterAsString);
			decimal value = FlexibleDecimal.Parse(valueAsString);

			QuarterlyCpiRecord quarterlyCpiRecord = new()
			{
				Quarter = yearQuarter,
				Value = value
			};

			return quarterlyCpiRecord;
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