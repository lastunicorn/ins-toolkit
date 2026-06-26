namespace DustInTheWind.Ins.Toolkit;

public class InsException : Exception
{
	private const string DefaultMessage = "Unknown error in the INS Toolkit.";

	public InsException()
		: base(DefaultMessage)
	{
	}

	public InsException(string message)
		: base(message)
	{
	}

	public InsException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	public InsException(Exception innerException)
		: base(DefaultMessage, innerException)
	{
	}
}