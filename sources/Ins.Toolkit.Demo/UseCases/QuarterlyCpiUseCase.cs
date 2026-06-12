using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class QuarterlyCpiUseCase
{
	public async Task Execute()
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