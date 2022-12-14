using System.Text;

namespace Cave;

public class Cave2
{
    public const int GridPaddingX = 500;
    public const int GridPaddingY = 3;
    
    public CellContents[,] Grid { get; private set; } = new CellContents[1, 1];

    private int _rowSize;
    private int _columnSize;
    
    public void CreateGrid(string[] inputLines)
    {
        // size the grid
        var maxX = 0;
        var maxY = 0;
        foreach (var inputLine in inputLines)
        {
            var traces = inputLine.Split(" -> ");
            foreach (var trace in traces)
            {
                var coordinates = trace.Split(',');
                maxX = Math.Max(maxX, Convert.ToInt32(coordinates[0]));
                maxY = Math.Max(maxY, Convert.ToInt32(coordinates[1]));
            }
        }

        _rowSize = maxX + GridPaddingX;
        _columnSize = maxY + GridPaddingY;
        Grid = new CellContents[_rowSize, _columnSize];

        // init everything to air and the floor to rocks
        for (var x = 0; x < _rowSize; x++)
        {
            for (var y = 0; y < _columnSize; y++)
            {
                Grid[x, y] = CellContents.Air;
            }
            
            Grid[x, _columnSize - 1] = CellContents.Rock;
        }
        
        // draw rocks
        foreach (var inputLine in inputLines)
        {
            var startX = -1;
            var startY = -1;
            var traces = inputLine.Split(" -> ");
            foreach (var trace in traces)
            {
                var coordinates = trace.Split(',');
                var endX = Convert.ToInt32(coordinates[0]);
                var endY = Convert.ToInt32(coordinates[1]);

                if (startX != -1 && startY != -1)
                {
                    var rangeX = Enumerable.Range(
                        Math.Min(startX, endX), 
                        Math.Max(startX, endX) - Math.Min(startX, endX) + 1);
                    var rangeY = Enumerable.Range(
                        Math.Min(startY, endY), 
                        Math.Max(startY, endY) - Math.Min(startY, endY) + 1);

                    foreach (var x in rangeX)
                    {
                        foreach (var y in rangeY)
                        {
                            Grid[x, y] = CellContents.Rock;
                        }
                    }
                }

                startX = endX;
                startY = endY;
            }
        }
    }

    public int DropSand(int? numberOfSandUnits = null)
    {
        var sandCount = 0;

        while (sandCount < (numberOfSandUnits ?? int.MaxValue))
        {
            var x = 500;
            var y = 0;
            var cell = Grid[x, y];

            if (cell == CellContents.Sand)
            {
                return sandCount;
            }
            
            while (cell == CellContents.Air)
            {
                // have we breached the bottom? 
                if (y + 1 > Grid.GetUpperBound(1))
                {
                    return sandCount;
                }
                
                // move down?
                if (IsAir(x, y + 1))
                {
                    y++;
                }
                else
                {
                    // move down and left?
                    if (IsAir(x - 1, y + 1))
                    {
                        x--;
                        y++;
                    }
                    // move down and right?
                    else if (IsAir(x + 1, y + 1))
                    {
                        x++;
                        y++;
                    }
                    // make the cell sand
                    else
                    {
                        Grid[x, y] = CellContents.Sand;
                        break;
                    }
                }
                
                cell = Grid[x, y];
            }
            
            sandCount++;
        }

        return sandCount;
    }

    public string PrintGrid()
    {
        StringBuilder result = new();
        
        for (var y = 0; y < _columnSize; y++)
        {
            for (var x = 480; x < 520; x++)
            {
                switch (Grid[x, y])
                {
                    case CellContents.Air:
                        result.Append(".");
                        break;
                    case CellContents.Rock:
                        result.Append("#");
                        break;
                    case CellContents.Sand:
                        result.Append("o");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            result.AppendLine();
        }

        return result.ToString();
    }
    
    public bool IsRock(int x, int y) => IsContents(x, y, CellContents.Rock);
    public bool IsSand(int x, int y) => IsContents(x, y, CellContents.Sand);
    public bool IsAir(int x, int y) => IsContents(x, y, CellContents.Air);
    private bool IsContents(int x, int y, CellContents contents)
    {
        return Grid[x, y] == contents;
    }
}