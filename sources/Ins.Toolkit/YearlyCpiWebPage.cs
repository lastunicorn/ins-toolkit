using DustInTheWind.Ins.Toolkit.Web;

namespace DustInTheWind.Ins.Toolkit;

public class YearlyCpiWebPage
{
	private readonly Uri uri;

	public YearlyCpiWebPage()
	{
		uri = new Uri("https://insse.ro/cms/ro/content/ipc%E2%80%93serie-de-date-anuala");
	}

	public YearlyCpiWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
	}

	public async Task<IEnumerable<YearlyCpiRecord>> EnumerateRecords()
	{
		YearlyCpiWebPageRequest yearlyCpiWebPageRequest = new(uri);
		YearlyCpiHtmlDocument yearlyCpiHtmlDocument = await yearlyCpiWebPageRequest.Execute();

		return yearlyCpiHtmlDocument.EnumerateRecords();
	}
}