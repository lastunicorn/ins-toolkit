using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class QuarterlyCpiWebPage
{
	private readonly Uri uri;

	public QuarterlyCpiWebPage()
	{
		uri = new Uri("https://insse.ro/cms/ro/content/ipc-serie-de-date-trimestriala");
	}

	public QuarterlyCpiWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
	}
	
	public async Task<IEnumerable<QuarterlyCpiRecord>> EnumerateRecords()
	{
		QuarterlyCpiWebPageRequest quarterlyCpiWebRequest = new(uri);
		QuarterlyCpiHtmlDocument quarterlyCpiHtmlDocument = await quarterlyCpiWebRequest.Execute();

		return quarterlyCpiHtmlDocument.EnumerateRecords();
	}
}