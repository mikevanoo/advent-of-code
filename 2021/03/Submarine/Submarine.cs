namespace Submarine;

public class Submarine
{
    public int GetPowerConsumption(string[] inputLines)
    {
        var mostCommonValues = GetMostCommonValuesByColumn(inputLines);
        var gammaRate = GetGammaRate(mostCommonValues);
        var epsilonRate = GetEpsilonRate(mostCommonValues);

        return gammaRate * epsilonRate;
    }

    public int GetGammaRate(string[] inputLines)
    {
        var mostCommonValues = GetMostCommonValuesByColumn(inputLines);

        return GetGammaRate(mostCommonValues);
    }
    
    public int GetEpsilonRate(string[] inputLines)
    {
        var mostCommonValues = GetMostCommonValuesByColumn(inputLines);

        return GetEpsilonRate(mostCommonValues);
    }

    public int GetLifeSupportRating(string[] inputLines)
    {
        return GetOxygenGeneratorRating(inputLines) * GetCo2ScrubberRating(inputLines);
    }

    public int GetOxygenGeneratorRating(string[] inputLines)
    {
        for (var columnIndex = 0; columnIndex < inputLines[0].Length; columnIndex++)
        {
            var moreOrLessZeros = MoreOrLessZerosAtPosition(inputLines, columnIndex);
            var mostCommonValue = moreOrLessZeros == MoreOrLessZeros.MoreZero ? '0' : '1';
            inputLines = inputLines
                .Where(line => line[columnIndex] == mostCommonValue)
                .ToArray();

            if (inputLines.Length == 1)
            {
                break;
            }
        }

        return ConvertBinaryToInt32(string.Join(string.Empty, inputLines[0]));
    }

    public int GetCo2ScrubberRating(string[] inputLines)
    {
        for (var columnIndex = 0; columnIndex < inputLines[0].Length; columnIndex++)
        {
            var moreOrLessZeros = MoreOrLessZerosAtPosition(inputLines, columnIndex);
            var leastCommonValue = moreOrLessZeros == MoreOrLessZeros.MoreZero ? '1' : '0';
            inputLines = inputLines
                .Where(line => line[columnIndex] == leastCommonValue)
                .ToArray();

            if (inputLines.Length == 1)
            {
                break;
            }
        }

        return ConvertBinaryToInt32(string.Join(string.Empty, inputLines[0]));
    }

    private List<int> GetMostCommonValuesByColumn(string[] inputLines)
    {
        List<MoreOrLessZeros> results = new();
        for (var index = 0; index < inputLines[0].Length; index++)
        {
            results.Add(MoreOrLessZerosAtPosition(inputLines, index));
        }

        return results
            .Select(x => x == MoreOrLessZeros.MoreZero ? 0 : 1)
            .ToList();
    }

    private MoreOrLessZeros MoreOrLessZerosAtPosition(string[] inputLines, int index)
    {
        var zeroCount = 0;
        var oneCount = 0;
        foreach (var input in inputLines)
        {
            if (input[index] == '0')
            {
                zeroCount++;
            }
            else
            {
                oneCount++;
            }
        }

        if (zeroCount == oneCount)
        {
            return MoreOrLessZeros.Equal;
        }

        if (zeroCount > oneCount)
        {
            return MoreOrLessZeros.MoreZero;
        }

        return MoreOrLessZeros.LessZero;
    }

    private enum MoreOrLessZeros
    {
        MoreZero,
        Equal,
        LessZero
    }

    private int GetGammaRate(List<int> mostCommonValues)
    {
        return ConvertBinaryToInt32(string.Join(string.Empty, mostCommonValues));
    }

    private int GetEpsilonRate(List<int> mostCommonValues)
    {
        var leastCommonValues = mostCommonValues
            .Select(x => Math.Abs(1 - x))
            .ToList();

        return ConvertBinaryToInt32(string.Join(string.Empty, leastCommonValues));
    }

    private int ConvertBinaryToInt32(string binary)
    {
        return Convert.ToInt32(binary, 2);
    }
}

