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
		QuarterlyCpiWebPageRequest cpiWebRequest = new(uri);
		QuarterlyCpiHtmlDocument cpiHtmlDocument = await cpiWebRequest.Execute();

		foreach (QuarterlyInflationRecord inflationRecord in cpiHtmlDocument.EnumerateCpiRecords())
			yield return inflationRecord;
	}
}