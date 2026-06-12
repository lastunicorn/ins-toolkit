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
		YearlyCpiWebPageRequest cpiWebRequest = new(uri);
		YearlyCpiHtmlDocument cpiHtmlDocument = await cpiWebRequest.Execute();

		foreach (YearlyInflationRecord inflationRecord in cpiHtmlDocument.EnumerateInflationRecords())
			yield return inflationRecord;
	}
}