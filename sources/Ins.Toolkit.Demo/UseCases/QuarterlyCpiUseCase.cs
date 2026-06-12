using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class QuarterlyCpiUseCase
{
	public async Task Execute()
	{
		QuarterlyCpiWebPage webPage = new();

		IAsyncEnumerable<QuarterlyCpiRecord> records = webPage.EnumerateRecords();
		await Display(records);
	}

	private static async Task Display(IAsyncEnumerable<QuarterlyCpiRecord> records)
	{
		DataGrid dataGrid = new()
		{
			Title = "Quarterly CPI"
		};

		dataGrid.Columns.Add("Quarter");
		dataGrid.Columns.Add("Value %", HorizontalAlignment.Right);

		await foreach (QuarterlyCpiRecord record in records)
			dataGrid.Rows.Add(record.Quarter, record.Value);

		dataGrid.Display();
	}
}