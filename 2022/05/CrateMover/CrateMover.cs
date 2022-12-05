namespace CrateMover;

public class CrateMover
{
    public IEnumerable<CrateStack> ParseStacks(string[] inputLines)
    {
        var stacksLines = inputLines
            .TakeWhile(inputLine => !string.IsNullOrWhiteSpace(inputLine))
            .ToList();
        stacksLines.Reverse();

        List<CrateStack> stacks = CreateStacks(stacksLines[0]);
        for (var index = 1; index < stacksLines.Count; index++)
        {
            var line = stacksLines[index];
            for (var stackIndex = 0; stackIndex < stacks.Count; stackIndex++)
            {
                var lineChar = GetStackCharacter(line, stackIndex);
                stacks[stackIndex].Crates.Push(lineChar);
            }
        }

        return stacks;
    }

    public char GetStackCharacter(string inputLine, int stackIndex)
    {
        return inputLine[stackIndex * 4 + 1];
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
}