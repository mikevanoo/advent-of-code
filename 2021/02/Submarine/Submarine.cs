using System.Globalization;

namespace Submarine;

public class Submarine
{
    public int GetHorizontalPositionMultipliedByDepth(string[] inputLines)
    {
        var commands = ParseCommands(inputLines);
        var position = ProcessCommands(commands);
        return position.Horizontal * position.Depth;
    }

    public int GetHorizontalPositionMultipliedByDepthUsingAim(string[] inputLines)
    {
        var commands = ParseCommands(inputLines);
        var position = ProcessCommandsUsingAim(commands);
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

    private Position ProcessCommands(IEnumerable<Command> commands)
    {
        Position position = new();

        foreach (var command in commands)
        {
            switch (command.Movement)
            {
                case Movement.Down:
                    position.Depth += command.Amount;
                    break;
                case Movement.Forward:
                    position.Horizontal += command.Amount;
                    break;
                case Movement.Up:
                    position.Depth -= command.Amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return position;
    }
    
    private Position ProcessCommandsUsingAim(IEnumerable<Command> commands)
    {
        Position position = new();

        foreach (var command in commands)
        {
            switch (command.Movement)
            {
                case Movement.Down:
                    position.Aim += command.Amount;
                    break;
                case Movement.Forward:
                    position.Horizontal += command.Amount;
                    position.Depth += position.Aim * command.Amount;
                    break;
                case Movement.Up:
                    position.Aim -= command.Amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return position;
    }
}

