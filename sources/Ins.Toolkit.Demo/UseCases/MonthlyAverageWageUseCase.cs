using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class MonthlyAverageWageUseCase
{
	public async Task Execute()
	{
		MonthlyAverageWageWebPage webpage = new();
		IEnumerable<MonthlyAverageWageRecord> records = await webpage.EnumerateRecords();

		IEnumerable<IGrouping<int, MonthlyAverageWageRecord>> groups = records
			.GroupBy(x => x.MonthYear.Year);

		foreach (IGrouping<int, MonthlyAverageWageRecord> group in groups)
			Display(group.Key, group);
	}

	private static void Display(int year, IEnumerable<MonthlyAverageWageRecord> records)
	{
		DataGrid dataGrid = new()
		{
			Title = $"Monthly Average Wage ({year})",
			Margin = new Thickness(0, 1, 0, 0)
		};

		dataGrid.Columns.Add("Month Year");
		dataGrid.Columns.Add("Gross", HorizontalAlignment.Right);
		dataGrid.Columns.Add("Net", HorizontalAlignment.Right);

		foreach (MonthlyAverageWageRecord record in records)
			dataGrid.Rows.Add(record.MonthYear, record.AverageGrossWage, record.AverageNetWage);

		dataGrid.Display();
	}
}