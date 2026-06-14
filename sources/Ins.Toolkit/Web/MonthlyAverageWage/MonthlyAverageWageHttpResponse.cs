namespace DustInTheWind.Ins.Toolkit.Web.MonthlyAverageWage;

internal class MonthlyAverageWageHttpResponse
{
	private readonly HttpResponseMessage httpResponseMessage;

	public MonthlyAverageWageHttpResponse(HttpResponseMessage httpResponseMessage)
	{
		this.httpResponseMessage = httpResponseMessage ?? throw new ArgumentNullException(nameof(httpResponseMessage));
	}

	public async Task<MonthlyAverageWageHtmlDocument> GetHtmlDocument()
	{
		if (!httpResponseMessage.IsSuccessStatusCode)
			throw new InsException($"Failed to retrieve the monthly average wage values from the web. Status code: {httpResponseMessage.StatusCode}");

		Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
		return new MonthlyAverageWageHtmlDocument(stream);
	}
}