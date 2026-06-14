using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Ins.Toolkit.Demo.UseCases;

internal class YearlyCpiUseCase
{
	public async Task Execute()
	{
		using YearlyCpiWebPage webPage = new();

		IEnumerable<YearlyCpiRecord> records = await webPage.EnumerateRecords();
		Display(records);
	}

	private static void Display(IEnumerable<YearlyCpiRecord> records)
	{
		DataGrid dataGrid = new()
		{
			Title = "Yearly CPI"
		};

		dataGrid.Columns.Add("Year");
		dataGrid.Columns.Add("Value %", HorizontalAlignment.Right);

		foreach (YearlyCpiRecord record in records)
			dataGrid.Rows.Add(record.Year, record.Value);

		dataGrid.Display();
	}
}