namespace DustInTheWind.Ins.Toolkit.Demo;

internal static class Program
{
	private static async Task Main(string[] args)
	{
		//await DisplayYearlyInflation();
		await DisplayQuarterlyInflation();
	}

	private static async Task DisplayYearlyInflation()
	{
		YearlyInflationWebPage yearlyInflationWebPage = new();

		IAsyncEnumerable<YearlyInflationRecord> inflationRecords = yearlyInflationWebPage.EnumerateInflationRecords();

		await foreach (YearlyInflationRecord inflationRecord in inflationRecords)
			Console.WriteLine($"Year: {inflationRecord.Year}, Value: {inflationRecord.Value}");
	}

	private static async Task DisplayQuarterlyInflation()
	{
		QuarterlyInflationWebPage quarterlyInflationWebPage = new();

		IAsyncEnumerable<QuarterlyInflationRecord> inflationRecords = quarterlyInflationWebPage.EnumerateInflationRecords();

		await foreach (QuarterlyInflationRecord inflationRecord in inflationRecords)
			Console.WriteLine($"Quarter: {inflationRecord.Quarter}, Value: {inflationRecord.Value}");
	}
}