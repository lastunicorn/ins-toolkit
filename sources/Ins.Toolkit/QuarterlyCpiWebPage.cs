using System.Net;
using DustInTheWind.Ins.Toolkit.Web.QuarterlyCpi;

namespace DustInTheWind.Ins.Toolkit;

public sealed class QuarterlyCpiWebPage : IDisposable
{
	private readonly Uri uri;
	private readonly HttpClient httpClient;

	public QuarterlyCpiWebPage()
		: this(new Uri("https://insse.ro/cms/ro/content/ipc-serie-de-date-trimestriala"))
	{
	}

	public QuarterlyCpiWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));

		ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

		httpClient = new HttpClient(new HttpClientHandler
		{
			AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
		});
	}

	public async Task<IEnumerable<QuarterlyCpiRecord>> EnumerateRecords()
	{
		QuarterlyCpiHttpRequest quarterlyCpiHttpRequest = new(uri);
		HttpRequestMessage httpRequestMessage = quarterlyCpiHttpRequest.ToHttpRequestMessage();

		HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

		QuarterlyCpiHttpResponse quarterlyCpiHttpResponse = new(httpResponseMessage);
		QuarterlyCpiHtmlDocument quarterlyCpiHtmlDocument = await quarterlyCpiHttpResponse.GetHtmlDocument();

		return quarterlyCpiHtmlDocument.EnumerateRecords();
	}

	public void Dispose()
	{
		httpClient?.Dispose();
	}
}