using System.Globalization;
using HtmlAgilityPack;

namespace DustInTheWind.Ins.Toolkit.Web;

internal class YearlyAverageWageHtmlDocument : IDisposable, IAsyncDisposable
{
	private readonly Stream stream;

	public YearlyAverageWageHtmlDocument(Stream stream)
	{
		this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
	}

	public IEnumerable<YearlyAverageWageRecord> EnumerateRecords()
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.Load(stream);

		HtmlNodeCollection trNodes = htmlDocument.DocumentNode.SelectNodes("//article//table/tbody/tr");

		foreach (HtmlNode trNode in trNodes.Skip(1))
		{
			YearlyAverageWageRecord yearlyAverageWageRecord = GetRecord(trNode);

			if (yearlyAverageWageRecord != null)
				yield return yearlyAverageWageRecord;
		}
	}

	private YearlyAverageWageRecord GetRecord(HtmlNode trNode)
	{
		HtmlNodeCollection cellNodes = trNode.SelectNodes("td");

		if (cellNodes.Count != 3)
			return null;

		string yearAsString = cellNodes[0].InnerText.Trim();
		string grossValueAsString = cellNodes[1].InnerText.Trim();
		string netValueAsString = cellNodes[2].InnerText.Trim();

		if (string.IsNullOrWhiteSpace(yearAsString) && string.IsNullOrWhiteSpace(grossValueAsString))
			return null;

		int year = ExtractNumber(yearAsString);
		int? averageGrossWage = int.Parse(grossValueAsString);
		int? averageNetWage = netValueAsString.StartsWith("...")
			? null
			: int.Parse(netValueAsString);

		YearlyAverageWageRecord yearlyAverageWageRecord = new()
		{
			Year = year,
			AverageGrossWage = averageGrossWage,
			AverageNetWage = averageNetWage
		};

		return yearlyAverageWageRecord;
	}

	private int ExtractNumber(string text)
	{
		int result = 0;

		foreach (char c in text)
		{
			if (char.IsDigit(c))
				result = result * 10 + (c - '0');
			else
				break;
		}

		return result;
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