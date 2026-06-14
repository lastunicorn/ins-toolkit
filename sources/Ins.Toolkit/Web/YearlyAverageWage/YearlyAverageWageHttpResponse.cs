namespace DustInTheWind.Ins.Toolkit.Web.YearlyAverageWage;

internal class YearlyAverageWageHttpResponse
{
	private readonly HttpResponseMessage httpResponseMessage;

	public YearlyAverageWageHttpResponse(HttpResponseMessage httpResponseMessage)
	{
		this.httpResponseMessage = httpResponseMessage ?? throw new ArgumentNullException(nameof(httpResponseMessage));
	}

	public async Task<YearlyAverageWageHtmlDocument> GetHtmlDocument()
	{
		if (!httpResponseMessage.IsSuccessStatusCode)
			throw new InsException($"Failed to retrieve the yearly average wage from the web. Status code: {httpResponseMessage.StatusCode}");

		Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
		return new YearlyAverageWageHtmlDocument(stream);
	}
}