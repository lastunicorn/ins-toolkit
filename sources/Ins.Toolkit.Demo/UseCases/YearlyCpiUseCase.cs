using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class YearlyCpiUseCase
{
	public async Task Execute()
	{
		YearlyCpiWebPage webPage = new();

		IAsyncEnumerable<YearlyCpiRecord> records = webPage.EnumerateRecords();
		await Display(records);
	}

	private static async Task Display(IAsyncEnumerable<YearlyCpiRecord> records)
	{
		DataGrid dataGrid = new()
		{
			Title = "Yearly CPI"
		};

		dataGrid.Columns.Add("Year");
		dataGrid.Columns.Add("Value %", HorizontalAlignment.Right);

		await foreach (YearlyCpiRecord record in records)
			dataGrid.Rows.Add(record.Year, record.Value);

		dataGrid.Display();
	}
}