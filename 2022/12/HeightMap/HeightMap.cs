namespace HeightMap;

public class HeightMap
{
    private int _rowCount;
    private int _columnCount;
    private readonly List<Coordinate> _visited = new();
    private Coordinate _currentPosition;

    public char[,] Grid { get; private set; } = new char[1,1];

    public HeightMap()
    {
        CurrentPosition = new Coordinate(0, 0);
    }

    public Coordinate CurrentPosition
    {
        get => _currentPosition;
        set
        {
            _visited.Add(value);
            _currentPosition = value;
        }
    }

    public void ParseInput(string[] inputLines)
    {
        _rowCount = inputLines.Length;
        _columnCount = inputLines[0].Length;
        Grid = new char[_rowCount, _columnCount];

        for (var rowIndex = 0; rowIndex < _rowCount; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < _columnCount; columnIndex++)
            {
                Grid[rowIndex, columnIndex] = inputLines[rowIndex][columnIndex];
            }
        }
    }

    public void FindNextMove()
    {
        var currentHeight = GetHeightAt(CurrentPosition);
        
        var up = GetPossibleMove(0, -1);
        var down = GetPossibleMove(0, 1);
        var left = GetPossibleMove(-1, 0);
        var right = GetPossibleMove(1, 0);
        
        List<PossibleMove> possibleMoves = new()
        {
            up,
            down,
            left,
            right
        };

        var moveToMake = possibleMoves
            .Where(move => move.Height.HasValue && 
                        move.Height.Value - currentHeight >= 0 &&
                        !_visited.Contains(move.Position))
            .OrderByDescending(x => x.Height)
            .First();

        CurrentPosition = moveToMake.Position;
    }

    private PossibleMove GetPossibleMove(int xDelta, int yDelta)
    {
        var x = CurrentPosition.X + xDelta;
        var y = CurrentPosition.Y + yDelta;
        var position = new Coordinate(x, y);

        if (x < 0 ||
            x >= _columnCount ||
            y < 0 ||
            y >= _rowCount)
        {
            return new(position, null);
        }
        
        return new PossibleMove(position, GetHeightAt(position));
    }
    
    private char GetHeightAt(Coordinate position)
    {
        var value = Grid[position.Y, position.X];
        return value switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => value
        };
    }
}