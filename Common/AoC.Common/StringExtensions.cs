using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC.Common;

public static partial class StringExtensions
{
    [GeneratedRegex(@"[-\d]+")]
    private static partial Regex GetNumbersRegex();
    
    public static IEnumerable<int> ExtractIntegers(this string value)
    {
        return ExtractNumbers<int>(value);
    }
    
    public static IEnumerable<long> ExtractLongs(this string value)
    {
        return ExtractNumbers<long>(value);
    }
    
    public static IEnumerable<T> ExtractNumbers<T>(this string value)
        where T : INumber<T>
    {
        foreach (Match match in GetNumbersRegex().Matches(value))
        {
            yield return T.Parse(match.Value, null);
        }
    }
}