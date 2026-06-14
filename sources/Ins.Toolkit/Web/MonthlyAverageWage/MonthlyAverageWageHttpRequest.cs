namespace DustInTheWind.Ins.Toolkit.Web.MonthlyAverageWage;

internal class MonthlyAverageWageHttpRequest
{
	private readonly Uri uri;

	public MonthlyAverageWageHttpRequest(Uri uri)
	{
		this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
	}

	public HttpRequestMessage ToHttpRequestMessage()
	{
		HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, uri);

		httpRequestMessage.Headers.Add("Host", "insse.ro");
		httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:128.0) Gecko/20100101 Firefox/128.0");
		httpRequestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/png,image/svg+xml,*/*;q=0.8");
		httpRequestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.5");
		httpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br, zstd");
		httpRequestMessage.Headers.Add("Connection", "keep-alive");
		httpRequestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
		httpRequestMessage.Headers.Add("Sec-Fetch-Dest", "document");
		httpRequestMessage.Headers.Add("Sec-Fetch-Mode", "navigate");
		httpRequestMessage.Headers.Add("Sec-Fetch-Site", "none");
		httpRequestMessage.Headers.Add("Sec-Fetch-User", "?1");
		httpRequestMessage.Headers.Add("Sec-GPC", "1");
		httpRequestMessage.Headers.Add("Priority", "u=0, i");
		httpRequestMessage.Headers.Add("Pragma", "no-cache");
		httpRequestMessage.Headers.Add("Cache-Control", "no-cache");

		return httpRequestMessage;
	}
}