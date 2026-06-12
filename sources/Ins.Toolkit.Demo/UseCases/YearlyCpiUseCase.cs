using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class YearlyCpiUseCase
{
	public async Task Execute()
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
}