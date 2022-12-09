namespace RopeBridge;

public class Rope
{
    public Coordinate HeadPosition { get; private set; }
    public Coordinate TailPosition { get; private set; }

    public Rope()
        : this(new Coordinate(0, 0), new Coordinate(0, 0))
    {
    }
    
    public Rope(Coordinate initialHeadPosition)
        : this(initialHeadPosition, new Coordinate(0, 0))
    {
    }
    
    public Rope(Coordinate initialHeadPosition, Coordinate initialTailPosition)
    {
        HeadPosition = initialHeadPosition;
        TailPosition = initialTailPosition;
    }

    public void Move(string[] moves)
    {
        foreach (var move in moves)
        {
            Move(move);
        }
    }

    public void Move(string move)
    {
        var parsedMove = move.Split(' ');
        var direction = Enum.Parse<Direction>(parsedMove[0]);
        var steps = Convert.ToInt32(parsedMove[1]);

        for (var step = 0; step < steps; step++)
        {
            HeadPosition.Move(direction);

            var xDelta = HeadPosition.X - TailPosition.X;
            var yDelta = HeadPosition.Y - TailPosition.Y;

            switch (xDelta)
            {
                case 0 when yDelta == 2:
                    TailPosition.Move(Direction.U);
                    break;
                case 0 when yDelta == -2:
                    TailPosition.Move(Direction.D);
                    break;
                case 2 when yDelta == 0:
                    TailPosition.Move(Direction.R);
                    break;
                case -2 when yDelta == 0:
                    TailPosition.Move(Direction.L);
                    break;
                case 1 when yDelta == 2:
                    TailPosition.Move(Direction.UR);
                    break;
                case 1 when yDelta == -2:
                    TailPosition.Move(Direction.DR);
                    break;
                case -1 when yDelta == 2:
                    TailPosition.Move(Direction.UL);
                    break;
                case -1 when yDelta == -2:
                    TailPosition.Move(Direction.DL);
                    break;
                case 2 when yDelta == 1:
                    TailPosition.Move(Direction.UR);
                    break;
                case 2 when yDelta == -1:
                    TailPosition.Move(Direction.DR);
                    break;
                case -2 when yDelta == 1:
                    TailPosition.Move(Direction.UL);
                    break;
                case -2 when yDelta == -1:
                    TailPosition.Move(Direction.DL);
                    break;
            }
        }
    }
}