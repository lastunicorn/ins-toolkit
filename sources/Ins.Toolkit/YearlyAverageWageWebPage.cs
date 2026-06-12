using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class YearlyAverageWageWebPage
{
	private readonly Uri uri;

	public YearlyAverageWageWebPage()
	{
		uri = new Uri("https://insse.ro/cms/ro/content/c%C3%A2%C8%99tiguri-salariale-din-1938-serie-anual%C4%83-0");
	}

	public YearlyAverageWageWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
	}

	public async IAsyncEnumerable<YearlyAverageWageRecord> EnumerateRecords()
	{
		YearlyAverageWageWebPageRequest webPageRequest = new(uri);
		YearlyAverageWageHtmlDocument htmlDocument = await webPageRequest.Execute();

		foreach (YearlyAverageWageRecord record in htmlDocument.EnumerateRecords())
			yield return record;
	}
}