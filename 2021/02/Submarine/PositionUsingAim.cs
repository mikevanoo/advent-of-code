namespace Submarine;

public class PositionUsingAim
{
    public int Horizontal { get; private set; }
    public int Depth { get; private set; }
    public int Aim { get; private set; }
    
    public void Move(Command command)
    {
        switch (command.Movement)
        {
            case Movement.Down:
                Aim += command.Amount;
                break;
            case Movement.Forward:
                Horizontal += command.Amount;
                Depth += Aim * command.Amount;
                break;
            case Movement.Up:
                Aim -= command.Amount;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}