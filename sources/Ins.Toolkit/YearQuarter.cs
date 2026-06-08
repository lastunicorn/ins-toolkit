namespace DustInTheWind.Ins.Toolkit;

public sealed record class YearQuarter
{
	public int Year { get; }

	public int Quarter { get; }

	public YearQuarter(int year, int quarter)
	{
		if (quarter < 1 || quarter > 4)
			throw new ArgumentOutOfRangeException(nameof(quarter), "Quarter must be between 1 and 4.");

		Year = year;
		Quarter = quarter;
	}

	public static YearQuarter Parse(string value)
	{
		if (value == null) throw new ArgumentNullException(nameof(value));

		string[] parts = value.Split(' ');

		if (parts.Length != 2)
			throw new FormatException("The input string must be in the format 'T{Quarter} {Year}'.");

		if (!parts[0].StartsWith('T') || !int.TryParse(parts[0].AsSpan(1), out int quarter))
			throw new FormatException("The quarter part must start with 'T' followed by a number between 1 and 4.");

		if (!int.TryParse(parts[1], out int year))
			throw new FormatException("The year part must be a valid integer.");

		return new YearQuarter(year, quarter);
	}

	public override string ToString()
	{
		return $"T{Quarter} {Year}";
	}

	public static implicit operator YearQuarter(string value)
	{
		return Parse(value);
	}

	public static implicit operator string(YearQuarter paymentType)
	{
		return paymentType?.ToString();
	}
}