using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo;

internal static class Program
{
	private static async Task Main(string[] args)
	{
		await DisplayYearlyInflation();
		await DisplayQuarterlyInflation();
	}

	private static async Task DisplayYearlyInflation()
	{
		YearlyInflationWebPage yearlyInflationWebPage = new();

		IAsyncEnumerable<YearlyInflationRecord> inflationRecords = yearlyInflationWebPage.EnumerateInflationRecords();
		await Display(inflationRecords);
	}

	private static async Task Display(IAsyncEnumerable<YearlyInflationRecord> inflationRecords)
	{
		DataGrid dataGrid = new()
		{
			Title = "Yearly Inflation"
		};

		dataGrid.Columns.Add("Year");
		dataGrid.Columns.Add("Value %", HorizontalAlignment.Right);

		await foreach (YearlyInflationRecord inflationRecord in inflationRecords)
			dataGrid.Rows.Add(inflationRecord.Year, inflationRecord.Value);
		
		dataGrid.Display();
	}

	private static async Task DisplayQuarterlyInflation()
	{
		QuarterlyInflationWebPage quarterlyInflationWebPage = new();

		IAsyncEnumerable<QuarterlyInflationRecord> inflationRecords = quarterlyInflationWebPage.EnumerateInflationRecords();
		await Display(inflationRecords);
	}

	private static async Task Display(IAsyncEnumerable<QuarterlyInflationRecord> inflationRecords)
	{
		DataGrid dataGrid = new()
		{
			Title = "Quarterly Inflation"
		};

		dataGrid.Columns.Add("Quarter");
		dataGrid.Columns.Add("Value %", HorizontalAlignment.Right);

		await foreach (QuarterlyInflationRecord inflationRecord in inflationRecords)
			dataGrid.Rows.Add(inflationRecord.Quarter, inflationRecord.Value);
		
		dataGrid.Display();
	}
}