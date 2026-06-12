using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class YearlyAverageWageUseCase
{
	public async Task Execute()
	{
		YearlyAverageWageWebPage webpage = new();

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
			dataGrid.Rows.Add(record.Year, record.AverageGrossWage, record.AverageNetWage);

		dataGrid.Display();
	}
}