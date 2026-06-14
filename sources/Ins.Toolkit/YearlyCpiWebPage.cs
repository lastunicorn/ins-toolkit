using System.Net;
using DustInTheWind.Ins.Toolkit.Web.YearlyCpi;

namespace DustInTheWind.Ins.Toolkit;

public sealed class YearlyCpiWebPage : IDisposable
{
	private readonly Uri uri;
	private readonly HttpClient httpClient;

	public YearlyCpiWebPage()
		: this(new Uri("https://insse.ro/cms/ro/content/ipc%E2%80%93serie-de-date-anuala"))
	{
	}

	public YearlyCpiWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));

		ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

		httpClient = new HttpClient(new HttpClientHandler
		{
			AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
		});
	}

	public async Task<IEnumerable<YearlyCpiRecord>> EnumerateRecords(CancellationToken cancellationToken = default)
	{
		YearlyCpiHttpRequest httpRequest = new(uri);
		HttpRequestMessage httpRequestMessage = httpRequest.ToHttpRequestMessage();

		HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

		YearlyCpiHttpResponse httpResponse = new(httpResponseMessage);
		YearlyCpiHtmlDocument yearlyCpiHtmlDocument = await httpResponse.GetHtmlDocument();

		return yearlyCpiHtmlDocument.EnumerateRecords();
	}

	public void Dispose()
	{
		httpClient?.Dispose();
	}
}