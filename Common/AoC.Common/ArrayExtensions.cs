namespace AoC.Common;

public static class ArrayExtensions
{
    public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
    
    public static IEnumerable<T> Flatten<T>(this T[,] matrix)
    {
        for (var rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < matrix.GetLength(1); columnIndex++)
            {
                yield return matrix[rowIndex, columnIndex];
            }
        }
    }
}