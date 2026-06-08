namespace DustInTheWind.Ins.Toolkit;

[Serializable]
internal class MissingInflationUrlException : Exception
{
    private const string DefaultMessage = "The URL for the inflation values was not provided.";

    public MissingInflationUrlException()
        : base(DefaultMessage)
    {
    }

    public MissingInflationUrlException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}