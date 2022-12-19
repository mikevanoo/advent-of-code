using System.Numerics;

namespace AoC.Common;

public static class CoordinateExtensions
{
    public static int ManhattanDistance(this Coordinate a, Coordinate b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public static T ManhattanDistance<T>(this Coordinate<T> a, Coordinate<T> b)
        where T : INumber<T>
    {
        return T.Abs(a.X - b.X) + T.Abs(a.Y - b.Y);
    }

    public static Coordinate Add(this Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }
    
    public static Coordinate<T> Add<T>(this Coordinate<T> a, Coordinate<T> b)
        where T : INumber<T>
    {
        return new Coordinate<T>(a.X + b.X, a.Y + b.Y);
    }
    
    public static bool WithinBoundsOf<TMatrix>(this Coordinate position, TMatrix[,] matrix)
    {
        return position.X >= matrix.GetLowerBound(0) &&
               position.X <= matrix.GetUpperBound(0) &&
               position.Y >= matrix.GetLowerBound(1) &&
               position.Y <= matrix.GetUpperBound(1);
    }
    
    public static List<Coordinate> Neighbours(this Coordinate position, bool includeDiagonals = false)
    {
        var neighbours = new List<Coordinate>()
        {
            new(position.X - 1, position.Y),
            new(position.X + 1, position.Y),
            new(position.X, position.Y - 1),
            new(position.X, position.Y + 1)
        };
        
        if(includeDiagonals)
        {
            neighbours.AddRange(new List<Coordinate>()
            {
                new(position.X - 1, position.Y - 1),
                new(position.X + 1, position.Y - 1),
                new(position.X - 1, position.Y + 1),
                new(position.X + 1, position.Y + 1)
            });
        }
        
        return neighbours;
    }
    
    public static List<Coordinate<T>> Neighbours<T>(this Coordinate<T> position, bool includeDiagonals = false)
        where T : INumber<T>
    {
        var plus1 = T.Zero;
        plus1++;
        
        var minus1 = T.Zero;
        minus1--;
        
        var neighbours = new List<Coordinate<T>>()
        {
            new(position.X + minus1, position.Y),
            new(position.X + plus1, position.Y),
            new(position.X, position.Y + minus1),
            new(position.X, position.Y + plus1)
        };
        
        if(includeDiagonals)
        {
            neighbours.AddRange(new List<Coordinate<T>>()
            {
                new(position.X + minus1, position.Y + minus1),
                new(position.X + plus1, position.Y + minus1),
                new(position.X + minus1, position.Y + plus1),
                new(position.X + plus1, position.Y + plus1)
            });
        }
        
        return neighbours;
    }
}