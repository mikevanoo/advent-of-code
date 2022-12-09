namespace RopeBridge;

public class Rope
{
    private readonly Dictionary<Coordinate, int> _tailPositionsVisited = new();
    
    public Coordinate HeadPosition { get; private set; }
    public Coordinate TailPosition { get; private set; }
    public int TailPositionsVisited => _tailPositionsVisited.Count;

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
        RecordTailPosition();
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
        var direction = parsedMove[0];
        var steps = Convert.ToInt32(parsedMove[1]);

        for (var step = 0; step < steps; step++)
        {
            HeadPosition = direction switch
            {
                "R" => new Coordinate(HeadPosition.X + 1, HeadPosition.Y),
                "L" => new Coordinate(HeadPosition.X - 1, HeadPosition.Y),
                "U" => new Coordinate(HeadPosition.X, HeadPosition.Y + 1),
                "D" => new Coordinate(HeadPosition.X, HeadPosition.Y - 1),
                _ => throw new InvalidOperationException()
            };

            var deltaX = HeadPosition.X - TailPosition.X;
            var deltaY = HeadPosition.Y - TailPosition.Y;
            if (Math.Abs(deltaX) > 1 || Math.Abs(deltaY) > 1)
            {
                var newX = TailPosition.X + Math.Sign(deltaX);
                var newY = TailPosition.Y + Math.Sign(deltaY);
                TailPosition = new Coordinate(newX, newY);
                RecordTailPosition();
            }
        }
    }
    
    private void RecordTailPosition()
    {
        if (_tailPositionsVisited.ContainsKey(TailPosition))
        {
            _tailPositionsVisited[TailPosition]++;
        }
        else
        {
            _tailPositionsVisited.Add(TailPosition, 1);
        }
    }
}