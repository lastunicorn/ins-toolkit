namespace DustInTheWind.Ins.Toolkit.Web.QuarterlyCpi;

internal class QuarterlyCpiHttpResponse
{
	private readonly HttpResponseMessage httpResponseMessage;

	public QuarterlyCpiHttpResponse(HttpResponseMessage httpResponseMessage)
	{
		this.httpResponseMessage = httpResponseMessage ?? throw new ArgumentNullException(nameof(httpResponseMessage));
	}

	public async Task<QuarterlyCpiHtmlDocument> GetHtmlDocument()
	{
		if (!httpResponseMessage.IsSuccessStatusCode)
			throw new InsException($"Failed to retrieve the quarterly CPI values from the web. Status code: {httpResponseMessage.StatusCode}");

		Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
		return new QuarterlyCpiHtmlDocument(stream);
	}
}