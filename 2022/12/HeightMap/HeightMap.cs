namespace HeightMap;

public class HeightMap
{
    private int _rowCount;
    private int _columnCount;
    private readonly List<Coordinate> _visited = new();
    private Coordinate _currentPosition;
    private Coordinate _start;
    private Coordinate _end;

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
                if (Grid[rowIndex, columnIndex] == 'S')
                {
                    _start = new Coordinate(columnIndex, rowIndex);
                }
                if (Grid[rowIndex, columnIndex] == 'E')
                {
                    _end = new Coordinate(columnIndex, rowIndex);
                }
            }
        }
    }

    public int GetFewestStepsToBestSignal()
    {
        var queue = new Queue<Coordinate>();
        var visited = new Dictionary<Coordinate, Coordinate>();

        visited[_start] = _start;
        queue.Enqueue(_start);

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();
            var positionHeight = GetHeightAt(position);
            var neighbours = GetNeighbours(position);
            foreach (var neighbour in neighbours)
            {
                if (neighbour.X < 0 ||
                    neighbour.X >= _columnCount ||
                    neighbour.Y < 0 ||
                    neighbour.Y >= _rowCount ||
                    visited.ContainsKey(neighbour))
                {
                    continue;
                }

                var neighbourHeight = GetHeightAt(neighbour);
                if (neighbourHeight - positionHeight <= 1)
                {
                    visited[neighbour] = position;
                    queue.Enqueue(neighbour);   
                }
            }
        }

        var endPositions = visited.Where(x => x.Key == _end);
        var stepCount = 0;
        foreach (var endPosition in endPositions)
        {
            var current = endPosition.Key;
            while (current != _start)
            {
                current = visited[current];
                stepCount++;
            }
        }
        
        return stepCount;
    }
    
    private List<Coordinate> GetNeighbours(Coordinate position)
    {
        return new List<Coordinate>
        {
            new Coordinate(position.X, position.Y - 1), // up
            new Coordinate(position.X, position.Y + 1), // down
            new Coordinate(position.X - 1, position.Y), // left
            new Coordinate(position.X + 1, position.Y)  // right
        };
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