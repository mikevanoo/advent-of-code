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
    
    public static IList<T> Shift<T>(this IList<T> list, T item, int number)
        where T : class // shifting reference types allows the caller to loop over the source items
    {
        var itemIndex = list.IndexOf(item);
        var moveCount = Math.Abs(number);
        var delta = Math.Sign(number);
        
        for (var move = 0; move < moveCount; move++)
        {
            var destinationIndex = itemIndex + delta;
            // wrap around the start/end
            if (destinationIndex > list.Count - 1)
            {
                destinationIndex = 0;
            } 
            else if (destinationIndex < 0)
            {
                destinationIndex = list.Count - 1;
            }

            // swap
            (list[destinationIndex], list[itemIndex]) = (list[itemIndex], list[destinationIndex]);

            itemIndex = destinationIndex;
        }

        return list;
    }
}