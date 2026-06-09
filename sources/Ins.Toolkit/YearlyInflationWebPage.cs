using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class YearlyInflationWebPage
{
	private readonly Uri uri;

	public YearlyInflationWebPage()
	{
		uri = new Uri("https://insse.ro/cms/ro/content/ipc%E2%80%93serie-de-date-anuala");
	}

	public YearlyInflationWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
	}

	public async IAsyncEnumerable<YearlyInflationRecord> EnumerateInflationRecords()
	{
		YearlyInflationWebPageRequest inflationWebRequest = new(uri);
		YearlyInflationHtmlDocument inflationHtmlDocument = await inflationWebRequest.Execute();

		foreach (YearlyInflationRecord inflationRecord in inflationHtmlDocument.EnumerateInflationRecords())
			yield return inflationRecord;
	}
}