using DustInTheWind.Ins.Toolkit.Demo.UseCases;

namespace DustInTheWind.Ins.Toolkit.Demo;

internal static class Program
{
	private static async Task Main(string[] args)
	{
		//YearlyCpiUseCase useCase = new();
		//QuarterlyCpiUseCase useCase = new();
		YearlyAverageWageUseCase useCase = new();
		
		await useCase.Execute();
	}
}