using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class QuarterlyInflationWebPage
{
	private readonly Uri uri;

	public QuarterlyInflationWebPage()
	{
		uri = new Uri("https://insse.ro/cms/ro/content/ipc-serie-de-date-trimestriala");
	}

	public QuarterlyInflationWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
	}
	
	public async IAsyncEnumerable<QuarterlyInflationRecord> EnumerateInflationRecords()
	{
		QuarterlyInflationWebPageRequest inflationWebRequest = new(uri);
		QuarterlyInflationHtmlDocument inflationHtmlDocument = await inflationWebRequest.Execute();

		foreach (QuarterlyInflationRecord inflationRecord in inflationHtmlDocument.EnumerateInflationRecords())
			yield return inflationRecord;
	}
}