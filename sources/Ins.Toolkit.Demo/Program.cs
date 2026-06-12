using DustInTheWind.Ins.Toolkit.Demo.UseCases;

namespace DustInTheWind.Ins.Toolkit.Demo;

internal static class Program
{
	private static async Task Main(string[] args)
	{
		// =======================================================
		// Uncomment the use case you want to run.
		// =======================================================

		//await new YearlyCpiUseCase().Execute();
		//await new QuarterlyCpiUseCase().Execute();
		await new YearlyAverageWageUseCase().Execute();
	}
}