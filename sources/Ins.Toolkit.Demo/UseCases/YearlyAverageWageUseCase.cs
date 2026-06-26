using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class YearlyAverageWageUseCase
{
	public async Task Execute()
	{
		using YearlyAverageWageWebPage webpage = new();

		IEnumerable<YearlyAverageWageRecord> records = await webpage.EnumerateRecords();
		Display(records);
	}

	private static void Display(IEnumerable<YearlyAverageWageRecord> records)
	{
		DataGrid dataGrid = new()
		{
			Title = "Yearly Average Wage"
		};

		dataGrid.Columns.Add("Year");
		dataGrid.Columns.Add("Gross", HorizontalAlignment.Right);
		dataGrid.Columns.Add("Net", HorizontalAlignment.Right);

		foreach (YearlyAverageWageRecord record in records)
		{
			string year = record.Year.ToString();
			string averageGrossWage = record.AverageGrossWage.HasValue
				? record.AverageGrossWage.Value.ToString("N0")
				: string.Empty;
			string averageNetWage = record.AverageNetWage.HasValue
				? record.AverageNetWage.Value.ToString("N0")
				: string.Empty;

			dataGrid.Rows.Add(year, averageGrossWage, averageNetWage);
		}

		dataGrid.Display();
	}
}