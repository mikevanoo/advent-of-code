namespace CpuRegister;

public class Crt
{
    private const int RowSize = 40;
    private const int ColumnSize = 6;

    public char[,] Screen { get; private set; } = new char[RowSize, ColumnSize];
    
    public void Draw(int cycleNumber, int registerValue)
    {
        var drawPositionY = Math.DivRem(cycleNumber - 1, RowSize, out var drawPositionX);
        var minSpritePosition = registerValue - 1;
        var maxSpritePosition = registerValue + 1;

        if (drawPositionX >= minSpritePosition && drawPositionX <= maxSpritePosition)
        {
            Screen[drawPositionX, drawPositionY] = '#';
        }
        else
        {
            Screen[drawPositionX, drawPositionY] = '.';
        }
    }
    
    public string[] PrintScreen()
    {
        List<string> rows = new();
        
        for (var rowIndex = 0; rowIndex < ColumnSize; rowIndex++)
        {
            var row = string.Empty;
            for (var columnIndex = 0; columnIndex < RowSize; columnIndex++)
            {
                row += Screen[columnIndex, rowIndex];
            }    
            rows.Add(row);
        }

        return rows.ToArray();
    }
}