using System.Net;
using DustInTheWind.Ins.Toolkit.Web.MonthlyAverageWage;

namespace DustInTheWind.Ins.Toolkit;

public sealed class MonthlyAverageWageWebPage : IDisposable
{
	private readonly Uri uri;
	private readonly HttpClient httpClient;

	public MonthlyAverageWageWebPage()
		: this(new Uri("https://insse.ro/cms/ro/content/c%C3%A2%C8%99tiguri-salariale-din-1991-serie-lunar%C4%83"))
	{
	}

	public MonthlyAverageWageWebPage(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));

		ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

		httpClient = new HttpClient(new HttpClientHandler
		{
			AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
		});
	}

	public async Task<IEnumerable<MonthlyAverageWageRecord>> EnumerateRecords()
	{
		MonthlyAverageWageHttpRequest httpRequest = new(uri);
		HttpRequestMessage httpRequestMessage = httpRequest.ToHttpRequestMessage();

		HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

		MonthlyAverageWageHttpResponse httpResponse = new(httpResponseMessage);
		MonthlyAverageWageHtmlDocument htmlDocument = await httpResponse.GetHtmlDocument();

		return htmlDocument.EnumerateRecords();
	}

	public void Dispose()
	{
		httpClient?.Dispose();
	}
}