using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class QuarterlyCpiUseCase
{
	public async Task Execute()
	{
		using QuarterlyCpiWebPage webPage = new();

		IEnumerable<QuarterlyCpiRecord> records = await webPage.EnumerateRecords();
		Display(records);
	}

	private static void Display(IEnumerable<QuarterlyCpiRecord> records)
	{
		DataGrid dataGrid = new()
		{
			Title = "Quarterly CPI"
		};

		dataGrid.Columns.Add("Quarter");
		dataGrid.Columns.Add("Value %", HorizontalAlignment.Right);

		foreach (QuarterlyCpiRecord record in records)
			dataGrid.Rows.Add(record.Quarter, record.Value);

		dataGrid.Display();
	}
}