namespace Submarine;

public class Position
{
    public int Horizontal { get; private set; }
    public int Depth { get; private set; }
    
    public void Move(Command command)
    {
        switch (command.Movement)
        {
            case Movement.Down:
                Depth += command.Amount;
                break;
            case Movement.Forward:
                Horizontal += command.Amount;
                break;
            case Movement.Up:
                Depth -= command.Amount;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}