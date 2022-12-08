namespace TreeHouse;

public class TreeHouse
{
    private int _rowSize = 1;
    private int _columnSize = 0;

    public Tree[][] Map { get; private set; } = new Tree[1][];
    
    public void ParseInput(string[] inputLines)
    {
        _rowSize = inputLines[0].Length;
        _columnSize = inputLines.Length;
        Map = new Tree[_rowSize][];

        for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
        {
            Map[rowIndex] = new Tree[_columnSize];
            
            for (var columnIndex = 0; columnIndex < _columnSize; columnIndex++)
            {
                var height = (int)char.GetNumericValue(inputLines[rowIndex][columnIndex]);
                Map[rowIndex][columnIndex] = new Tree(
                    Height: height,
                    Visible: false
                );
            }
        }

        SetTreeVisibility();
    }

    public int GetVisibleTreeCount()
    {
        var visibleCount = 0;
        
        for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < _columnSize; columnIndex++)
            {
                if (Map[rowIndex][columnIndex].Visible)
                {
                    visibleCount++;
                }
            }
        }

        return visibleCount;
    }

    private void SetTreeVisibility()
    {
        for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < _columnSize; columnIndex++)
            {
                var visible = IsEdgeTree(rowIndex, columnIndex);
                visible = IsVisibleUp(rowIndex, columnIndex, visible);
                visible = IsVisibleLeft(rowIndex, columnIndex, visible);
                visible = IsVisibleDown(rowIndex, columnIndex, visible);
                visible = IsVisibleRight(rowIndex, columnIndex, visible);

                Map[rowIndex][columnIndex].Visible = visible;
            }
        }
    }

    private bool IsEdgeTree(int rowIndex, int columnIndex)
    {
        return rowIndex == 0 ||
               rowIndex == _rowSize - 1 ||
               columnIndex == 0 ||
               columnIndex == _columnSize - 1;
    }

    private bool IsVisibleUp(int rowIndex, int columnIndex, bool isAlreadyVisible)
    {
        if (isAlreadyVisible)
        {
            return isAlreadyVisible;
        }
        
        var height = Map[rowIndex][columnIndex].Height;
        var indexToCheck = rowIndex - 1;

        while (indexToCheck >= 0)
        {
            if (Map[indexToCheck][columnIndex].Height >= height)
            {
                return false;
            }
            indexToCheck--;
        }

        return true;
    }

    private bool IsVisibleLeft(int rowIndex, int columnIndex, bool isAlreadyVisible)
    {
        if (isAlreadyVisible)
        {
            return isAlreadyVisible;
        }
        
        var height = Map[rowIndex][columnIndex].Height;
        var indexToCheck = columnIndex - 1;

        while (indexToCheck >= 0)
        {
            if (Map[rowIndex][indexToCheck].Height >= height)
            {
                return false;
            }
            indexToCheck--;
        }

        return true;
    }

    private bool IsVisibleDown(int rowIndex, int columnIndex, bool isAlreadyVisible)
    {
        if (isAlreadyVisible)
        {
            return isAlreadyVisible;
        }
        
        var height = Map[rowIndex][columnIndex].Height;
        var indexToCheck = rowIndex + 1;

        while (indexToCheck < _rowSize)
        {
            if (Map[indexToCheck][columnIndex].Height >= height)
            {
                return false;
            }
            indexToCheck++;
        }

        return true;
    }

    private bool IsVisibleRight(int rowIndex, int columnIndex, bool isAlreadyVisible)
    {
        if (isAlreadyVisible)
        {
            return isAlreadyVisible;
        }
        
        var height = Map[rowIndex][columnIndex].Height;
        var indexToCheck = columnIndex + 1;

        while (indexToCheck < _columnSize)
        {
            if (Map[rowIndex][indexToCheck].Height >= height)
            {
                return false;
            }
            indexToCheck++;
        }

        return true;
    }
}