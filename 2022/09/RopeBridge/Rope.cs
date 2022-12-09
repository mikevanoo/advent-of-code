namespace RopeBridge;

public class Rope
{
    private const int HeadIndex = 0;
    
    private readonly List<Coordinate> _knots = new();
    private readonly Dictionary<Coordinate, int> _tailPositionsVisited = new();

    public Coordinate HeadPosition
    {
        get => _knots[HeadIndex];
        private set => _knots[HeadIndex] = value;
    }

    public Coordinate TailPosition
    {
        get => _knots[^1]; 
        private set => _knots[^1] = value;
    }
    
    public int TailPositionsVisited => _tailPositionsVisited.Count;

    public Rope(int knotCount)
        : this(knotCount, new Coordinate(0, 0), new Coordinate(0, 0))
    {
    }
    
    public Rope(int knotCount, Coordinate initialHeadPosition)
        : this(knotCount, initialHeadPosition, new Coordinate(0, 0))
    {
    }
    
    public Rope(int knotCount, Coordinate initialHeadPosition, Coordinate initialTailPosition)
    {
        for (var index = HeadIndex; index < knotCount; index++)
        {
            _knots.Add(new Coordinate(0, 0));
        }
        
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

            for (var knotIndex = HeadIndex + 1; knotIndex < _knots.Count; knotIndex++)
            {
                var currentKnot = _knots[knotIndex];
                var previousKnot = _knots[knotIndex - 1];
                var deltaX = previousKnot.X - currentKnot.X;
                var deltaY = previousKnot.Y - currentKnot.Y;
                
                if (Math.Abs(deltaX) > 1 || Math.Abs(deltaY) > 1)
                {
                    var newX = currentKnot.X + Math.Sign(deltaX);
                    var newY = currentKnot.Y + Math.Sign(deltaY);
                    _knots[knotIndex] = new Coordinate(newX, newY);
                }

                if (knotIndex == _knots.Count - 1)
                {
                    RecordTailPosition();
                }
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