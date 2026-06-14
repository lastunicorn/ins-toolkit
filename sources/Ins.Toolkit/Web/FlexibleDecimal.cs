using System.Globalization;

namespace DustInTheWind.Ins.Toolkit.Web;

internal readonly record struct FlexibleDecimal
{
	private readonly decimal value;

	private FlexibleDecimal(decimal value)
	{
		this.value = value;
	}

	/// <summary>
	/// Parses a decimal string that may use either '.' or ',' as the
	/// fractional separator, and optionally the other character as a
	/// thousands separator.
	/// </summary>
	/// <exception cref="FormatException">
	/// Thrown when the string cannot be interpreted as a number.
	/// </exception>
	public static FlexibleDecimal Parse(string value)
	{
		if (TryParse(value, out FlexibleDecimal result))
			return result;

		throw new FormatException(
			$"'{value}' is not a valid decimal number. " +
			"Both '.' and ',' are accepted as decimal separators.");
	}

	/// <summary>
	/// Tries to parse a decimal string that may use either '.' or ','
	/// as the fractional separator.
	/// </summary>
	public static bool TryParse(string value, out FlexibleDecimal result)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			result = default;
			return false;
		}

		string normalized = Normalize(value.Trim());
		if (normalized is null)
		{
			result = default;
			return false;
		}

		bool success = decimal.TryParse(
				normalized,
				NumberStyles.Number,
				CultureInfo.InvariantCulture,
				out decimal d);

		result = new FlexibleDecimal(d);
		return success;
	}
	
 
	// -----------------------------------------------------------------------
	// Private: normalize to invariant format (dot as decimal separator)
	// -----------------------------------------------------------------------
 
	/// <summary>
	/// Returns the string with a single '.' as the decimal separator, or
	/// null if the input is not parseable.
	///
	/// Heuristic rules (mirrors common EU/US banking conventions):
	///   - Both '.' and ',' present  → the rightmost one is the decimal sep.
	///       "1.234,56"  → "1234.56"
	///       "1,234.56"  → "1234.56"
	///   - Only ',' present:
	///       single comma, 3 digits after → thousands separator  "1,500" → "1500"
	///       single comma, 1-2 digits after → decimal separator  "1,5"   → "1.5"
	///       multiple commas               → all are thousands   "1,234,567" → "1234567"
	///   - Only '.' present → standard invariant (pass through)
	/// </summary>
	private static string Normalize(string value)
	{
		int lastDot   = value.LastIndexOf('.');
		int lastComma = value.LastIndexOf(',');
 
		if (lastDot >= 0 && lastComma >= 0)
		{
			// Both present: rightmost is the decimal separator.
			return lastComma > lastDot
				? value.Replace(".", "").Replace(',', '.')   // "1.234,56"
				: value.Replace(",", "");                    // "1,234.56"
		}
 
		if (lastComma >= 0)
		{
			int commaCount      = CountChar(value, ',');
			int digitsAfterComma = value.Length - lastComma - 1;
 
			if (commaCount > 1 || digitsAfterComma == 3)
				return value.Replace(",", "");   // thousands separator
 
			return value.Replace(',', '.');      // decimal separator
		}
 
		// Only dots or no separator — invariant already.
		return value;
	}

	private static int CountChar(string s, char c)
	{
		return s.Count(x => x == c);
	}

	public override string ToString()
	{
		return value.ToString("0.00");
	}

	public static implicit operator decimal(FlexibleDecimal flexibleDecimal)
	{
		return flexibleDecimal.value;
	}

	public static implicit operator FlexibleDecimal(decimal value)
	{
		return new FlexibleDecimal(value);
	}

	public static implicit operator FlexibleDecimal(string text)
	{
		return Parse(text);
	}

	public static implicit operator string(FlexibleDecimal flexibleDecimal)
	{
		return flexibleDecimal.ToString();
	}
}