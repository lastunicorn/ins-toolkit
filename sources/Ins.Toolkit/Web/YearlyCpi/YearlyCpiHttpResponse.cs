namespace DustInTheWind.Ins.Toolkit.Web.YearlyCpi;

internal class YearlyCpiHttpResponse
{
	private readonly HttpResponseMessage httpResponseMessage;

	public YearlyCpiHttpResponse(HttpResponseMessage httpResponseMessage)
	{
		this.httpResponseMessage = httpResponseMessage ?? throw new ArgumentNullException(nameof(httpResponseMessage));
	}

	public async Task<YearlyCpiHtmlDocument> GetHtmlDocument()
	{
		if (!httpResponseMessage.IsSuccessStatusCode)
			throw new InsException($"Failed to retrieve the CPI from the web. Status code: {httpResponseMessage.StatusCode}");

		Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
		return new YearlyCpiHtmlDocument(stream);
	}
}