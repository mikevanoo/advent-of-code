using System.Text.RegularExpressions;

namespace CrateMover;

public partial class CrateMover
{
    public string MoveCratesSingle(string[] inputLines)
    {
        return MoveCrates(
            inputLines,
            (quantity, source, destination) =>
            {
                while (quantity > 0)
                {
                    var crate = source.Crates.Pop();
                    destination.Crates.Push(crate);
                    quantity--;
                }
            });
    }
    
    public string MoveCratesMultiple(string[] inputLines)
    {
        return MoveCrates(
            inputLines, 
            (quantity, source, destination) =>
            {
                List<char> crates = new();
                while (quantity > 0)
                {
                    crates.Add(source.Crates.Pop());
                    quantity--;
                }
                crates.Reverse();

                foreach (var crate in crates)
                {
                    destination.Crates.Push(crate);
                }
            });
    }

    public IEnumerable<CrateStack> ParseStacks(string[] inputLines)
    {
        var stacksLines = inputLines
            .TakeWhile(inputLine => !string.IsNullOrWhiteSpace(inputLine))
            .ToList();
        stacksLines.Reverse();

        var stacks = CreateStacks(stacksLines[0]);
        for (var index = 1; index < stacksLines.Count; index++)
        {
            var line = stacksLines[index];
            for (var stackIndex = 0; stackIndex < stacks.Count; stackIndex++)
            {
                var lineChar = GetStackCharacter(line, stackIndex);
                if (char.IsLetter(lineChar))
                {
                    stacks[stackIndex].Crates.Push(lineChar);   
                }
            }
        }

        return stacks;
    }

    public char GetStackCharacter(string inputLine, int stackIndex)
    {
        return inputLine[stackIndex * 4 + 1];
    }

    [GeneratedRegex("\\d+")]
    private static partial Regex GetNumbersRegex();

    public IEnumerable<CrateMove> ParseMoves(string[] inputLines)
    {
        var movesLines = inputLines
            .SkipWhile(inputLine => !string.IsNullOrWhiteSpace(inputLine))
            .Skip(1)
            .ToList();
        
        return movesLines
            .Select(inputLine => GetNumbersRegex().Matches(inputLine))
            .Select(numbers => new CrateMove(
                Convert.ToInt32(numbers[0].Value), 
                Convert.ToInt32(numbers[1].Value), 
                Convert.ToInt32(numbers[2].Value)))
            .ToList();
    }

    private List<CrateStack> CreateStacks(string stackNumberLine)
    {
        return stackNumberLine
            .Replace(" ", string.Empty)
            .Select(x => new CrateStack
            {
                Number = (int)char.GetNumericValue(x)
            })
            .ToList();
    }

    private string MoveCrates(
        string[] inputLines, 
        Action<int, CrateStack, CrateStack> mover)
    {
        var stacks = ParseStacks(inputLines).ToList();
        var moves = ParseMoves(inputLines);

        foreach (var move in moves)
        {
            var source = stacks.First(x => x.Number == move.SourceStackNumber);
            var destination = stacks.First(x => x.Number == move.DestinationStackNumber);
            mover(move.Quantity, source, destination);
        }

        var tops = stacks
            .Select(x => x.Crates.Peek())
            .ToArray();

        return new string(tops);
    }
}