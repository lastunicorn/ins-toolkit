using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class QuarterlyInflationWebPage
{
	public async IAsyncEnumerable<QuarterlyInflationRecord> EnumerateInflationRecords()
	{
		const string url = "https://insse.ro/cms/ro/content/ipc-serie-de-date-trimestriala";

		QuarterlyInflationWebPageRequest inflationWebRequest = new(url);
		QuarterlyInflationHtmlDocument inflationHtmlDocument = await inflationWebRequest.Execute();

		foreach (QuarterlyInflationRecord inflationRecord in inflationHtmlDocument.EnumerateInflationRecords())
			yield return inflationRecord;
	}
}