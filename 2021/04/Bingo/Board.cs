using System.Text.RegularExpressions;
using AoC.Common;

namespace Bingo;

public partial class Board
{
    public int[,] Grid { get; private set; } = new int[GridSize, GridSize];
    public int SumOfUnmarkedNumbers { get; private set; }

    [GeneratedRegex(@"[\d]+")]
    private static partial Regex GetNumbersRegex();
    
    private const int GridSize = 5;

    public void ParseInput(string[] inputLines)
    {
        for (var rowIndex = 0; rowIndex < inputLines.Length; rowIndex++)
        {
            var inputLine = inputLines[rowIndex];
            var numbers = GetNumbersRegex().Matches(inputLine);
            for (var columnIndex = 0; columnIndex < numbers.Count; columnIndex++)
            {
                var number = Convert.ToInt32(numbers[columnIndex].Value);
                Grid[rowIndex, columnIndex] = number;
            }
        }
    }

    public bool HasWon(int[] calledNumbers)
    {
        // check rows
        for (var rowIndex = 0; rowIndex < GridSize; rowIndex++)
        {
            var row = Grid.GetRow(rowIndex);
            if (!row.Except(calledNumbers).Any())
            {
                SumOfUnmarkedNumbers = SumNumbersExcept(calledNumbers);
                return true;
            }         
        }
        
        // check columns
        for (var columnIndex = 0; columnIndex < GridSize; columnIndex++)
        {
            var column = Grid.GetColumn(columnIndex);
            if (!column.Except(calledNumbers).Any())
            {
                SumOfUnmarkedNumbers = SumNumbersExcept(calledNumbers);
                return true;
            }
        }
        
        return false;
    }

    private int SumNumbersExcept(int[] calledNumbers)
    {
        var sum = 0;
        for (var rowIndex = 0; rowIndex < GridSize; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < GridSize; columnIndex++)
            {
                var number = Grid[rowIndex, columnIndex];
                if (!calledNumbers.Contains(number))
                {
                    sum += number;   
                }
            }
        }

        return sum;
    }
}