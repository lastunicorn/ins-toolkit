using System.Globalization;
using HtmlAgilityPack;

namespace DustInTheWind.Ins.Toolkit.Web.MonthlyAverageWage;

internal class MonthlyAverageWageHtmlDocument : IDisposable, IAsyncDisposable
{
	private static readonly CultureInfo CultureInfo = new("ro-RO");
	private readonly Stream stream;

	public MonthlyAverageWageHtmlDocument(Stream stream)
	{
		this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
	}

	public IEnumerable<MonthlyAverageWageRecord> EnumerateRecords()
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.Load(stream);

		HtmlNodeCollection tableNodes = htmlDocument.DocumentNode.SelectNodes("//article//table");

		if (tableNodes.Count != 2)
			throw new InvalidOperationException("Expected exactly 2 tables in the document.");

		HtmlNode netWagesTableNode = tableNodes[0];
		IEnumerable<TableValue> netWages = EnumerateValuesFromTable(netWagesTableNode);

		List<MonthlyAverageWageRecord> records = netWages
			.Select(x => new MonthlyAverageWageRecord
			{
				MonthYear = x.MonthYear,
				AverageNetWage = x.Value
			})
			.ToList();

		HtmlNode grossWagesTableNode = tableNodes[1];
		IEnumerable<TableValue> grossWages = EnumerateValuesFromTable(grossWagesTableNode);

		foreach (TableValue tableValue in grossWages)
		{
			MonthlyAverageWageRecord existingRecord = records
				.SingleOrDefault(x => x.MonthYear == tableValue.MonthYear);

			if (existingRecord != null)
			{
				existingRecord.AverageGrossWage = tableValue.Value;
			}
			else
			{
				records.Add(new MonthlyAverageWageRecord
				{
					MonthYear = tableValue.MonthYear,
					AverageGrossWage = tableValue.Value
				});
			}
		}

		return records;
	}

	private IEnumerable<TableValue> EnumerateValuesFromTable(HtmlNode tableNode)
	{
		HtmlNodeCollection trNodes = tableNode.SelectNodes("tbody/tr");

		foreach (HtmlNode trNode in trNodes.Skip(1))
		{
			IEnumerable<TableValue> records = EnumerateValuesFromRow(trNode)
				.Where(x => x != null);

			foreach (TableValue tableValue in records)
				yield return tableValue;
		}
	}

	private IEnumerable<TableValue> EnumerateValuesFromRow(HtmlNode trNode)
	{
		HtmlNodeCollection cellNodes = trNode.SelectNodes("td");

		if (cellNodes.Count != 13)
			yield break;

		string yearAsString = cellNodes[0].InnerText.Trim();
		int year = ExtractNumber(yearAsString);

		for (int i = 1; i <= 12; i++)
		{
			string rawValue = cellNodes[i].InnerText.Trim();

			if (string.IsNullOrWhiteSpace(rawValue))
				continue;

			int value = int.Parse(rawValue, NumberStyles.AllowThousands, CultureInfo);

			yield return new TableValue
			{
				MonthYear = new MonthDate(year, i),
				Value = value
			};
		}
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