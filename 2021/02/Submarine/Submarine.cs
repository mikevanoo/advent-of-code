using System.Globalization;

namespace Submarine;

public class Submarine
{
    public int GetHorizontalPositionMultipliedByDepth(string[] inputLines)
    {
        var commands = ParseCommands(inputLines);
        Position position = new();

        foreach (var command in commands)
        {
            position.Move(command);
        }
        
        return position.Horizontal * position.Depth;
    }

    public int GetHorizontalPositionMultipliedByDepthUsingAim(string[] inputLines)
    {
        var commands = ParseCommands(inputLines);
        PositionUsingAim position = new();
        
        foreach (var command in commands)
        {
            position.Move(command);
        }
        
        return position.Horizontal * position.Depth;
    }

    private IEnumerable<Command> ParseCommands(string[] inputLines)
    {
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        
        return inputLines
            .Select(inputLine => inputLine.Split(' '))
            .Select(commandArgs => new Command(
                Enum.Parse<Movement>(textInfo.ToTitleCase(commandArgs[0])),
                Convert.ToInt32(commandArgs[1]))
            );
    }
}

