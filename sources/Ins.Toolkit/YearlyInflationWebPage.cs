using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class YearlyInflationWebPage
{
	public async IAsyncEnumerable<YearlyInflationRecord> EnumerateInflationRecords()
	{
		const string url = "https://insse.ro/cms/ro/content/ipc%E2%80%93serie-de-date-anuala";

		YearlyInflationWebPageRequest inflationWebRequest = new(url);
		YearlyInflationHtmlDocument inflationHtmlDocument = await inflationWebRequest.Execute();

		foreach (YearlyInflationRecord inflationRecord in inflationHtmlDocument.EnumerateInflationRecords())
			yield return inflationRecord;
	}
}