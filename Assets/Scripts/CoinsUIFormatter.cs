public static class CoinsUIFormatter
{
    public static string Format(int value, FormatType format = FormatType.DEFAULT)
    {
        if (format == FormatType.SHORT)
        {
            if (value < 1000)
            {
                return value.ToString();
            }
            else if (value < 1000000)
            {
                return $"{(value / 1000f):F1}K";
            }
            else if (value < 1000000000)
            {
                return $"{(value / 1000000f):F1}M";
            }
            else
            {
                return $"{(value / 1000000000f):F1}B";
            }
        }
        return value.ToString();

    }
}
public enum FormatType
{
    DEFAULT,
    SHORT
}

