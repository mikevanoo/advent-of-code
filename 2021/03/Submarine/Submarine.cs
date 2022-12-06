namespace Submarine;

public class Submarine
{
    public int GetPowerConsumption(string[] inputLines)
    {
        var input = ParseInput(inputLines);
        var mostCommonValues = GetMostCommonValuesByColumn(input);
        var gammaRate = GetGammaRate(mostCommonValues);
        var epsilonRate = GetEpsilonRate(mostCommonValues);

        return gammaRate * epsilonRate;
    }

    public int GetGammaRate(string[] inputLines)
    {
        var input = ParseInput(inputLines);
        var mostCommonValues = GetMostCommonValuesByColumn(input);

        return GetGammaRate(mostCommonValues);
    }
    
    public int GetEpsilonRate(string[] inputLines)
    {
        var input = ParseInput(inputLines);
        var mostCommonValues = GetMostCommonValuesByColumn(input);

        return GetEpsilonRate(mostCommonValues);
    }
    
    private List<List<int>> ParseInput(string[] inputLines)
    {
        var rowCount = inputLines.Length;
        var columnCount = inputLines[0].Length;
        var binaries = new List<List<int>>(columnCount);
    
        for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            var column = new List<int>(rowCount);
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                column.Add(inputLines[rowIndex][columnIndex] == '1' ? 1 : 0);
            }
            binaries.Add(column);
        }

        return binaries;
    }
    
    private List<int> GetMostCommonValuesByColumn(List<List<int>> input)
    {
        return input
            .Select(column => FindMostCommonValue(column.ToList()))
            .ToList();
    }

    private int FindMostCommonValue(List<int> column)
    {
        var oneCount = column.Sum();
        var zeroCount = column.Count - oneCount;
        return oneCount > zeroCount ? 1 : 0;
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

