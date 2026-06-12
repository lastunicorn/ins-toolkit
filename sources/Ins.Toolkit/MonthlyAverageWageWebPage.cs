using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class MonthlyAverageWageWebPage
{
	private readonly Uri uri;

	public MonthlyAverageWageWebPage()
	{
		uri = new Uri("https://insse.ro/cms/ro/content/c%C3%A2%C8%99tiguri-salariale-din-1991-serie-lunar%C4%83");
	}

	public MonthlyAverageWageWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
	}

	public async Task<IEnumerable<MonthlyAverageWageRecord>> EnumerateRecords()
	{
		MonthlyAverageWageWebPageRequest webPageRequest = new(uri);
		MonthlyAverageWageHtmlDocument htmlDocument = await webPageRequest.Execute();

		return htmlDocument.EnumerateRecords();
	}
}