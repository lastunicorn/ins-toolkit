using System.Net;
using DustInTheWind.Ins.Toolkit.Web.YearlyAverageWage;

namespace DustInTheWind.Ins.Toolkit;

public sealed class YearlyAverageWageWebPage : IDisposable
{
	private readonly Uri uri;
	private readonly HttpClient httpClient;

	public YearlyAverageWageWebPage()
		: this(new Uri("https://insse.ro/cms/ro/content/c%C3%A2%C8%99tiguri-salariale-din-1938-serie-anual%C4%83-0"))
	{
	}

	public YearlyAverageWageWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));

		ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

		httpClient = new HttpClient(new HttpClientHandler
		{
			AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
		});
	}

	public async Task<IEnumerable<YearlyAverageWageRecord>> EnumerateRecords()
	{
		YearlyAverageWageHttpRequest httpRequest = new(uri);
		HttpRequestMessage httpRequestMessage = httpRequest.ToHttpRequestMessage();

		HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

		YearlyAverageWageHttpResponse httpResponse = new(httpResponseMessage);
		YearlyAverageWageHtmlDocument htmlDocument = await httpResponse.GetHtmlDocument();

		return htmlDocument.EnumerateRecords();
	}

	public void Dispose()
	{
		httpClient?.Dispose();
	}
}