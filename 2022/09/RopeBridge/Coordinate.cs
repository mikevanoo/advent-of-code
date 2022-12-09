namespace RopeBridge;

public record Coordinate
{
    private readonly Dictionary<string, int> _positionsVisited = new();

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
        RecordPosition();
    }

    public int X { get; private set; }
    public int Y { get; private set; }
    public int PositionsVisited => _positionsVisited.Count;
    
    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.U:
                Y++;
                break;
            case Direction.D:
                Y--;
                break;
            case Direction.R:
                X++;
                break;
            case Direction.L:
                X--;
                break;
            case Direction.UL:
                Y++;
                X--;
                break;
            case Direction.UR:
                Y++;
                X++;
                break;
            case Direction.DL:
                Y--;
                X--;
                break;
            case Direction.DR:
                Y--;
                X++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        RecordPosition();
    }

    public virtual bool Equals(Coordinate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    
    private void RecordPosition()
    {
        var key = X.ToString() + Y.ToString();
        if (_positionsVisited.ContainsKey(key))
        {
            _positionsVisited[key]++;
        }
        else
        {
            _positionsVisited.Add(key, 1);
        }
    }
}