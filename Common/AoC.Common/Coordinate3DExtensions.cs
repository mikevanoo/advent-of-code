using System.Numerics;

namespace AoC.Common;

public static class Coordinate3DExtensions
{
    public static int ManhattanDistance(this Coordinate3D a, Coordinate3D b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
    }

    public static T ManhattanDistance<T>(this Coordinate3D<T> a, Coordinate3D<T> b)
        where T : INumber<T>
    {
        return T.Abs(a.X - b.X) + T.Abs(a.Y - b.Y) + T.Abs(a.Z - b.Z);
    }

    public static Coordinate3D Add(this Coordinate3D a, Coordinate3D b)
    {
        return new Coordinate3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    
    public static Coordinate3D<T> Add<T>(this Coordinate3D<T> a, Coordinate3D<T> b)
        where T : INumber<T>
    {
        return new Coordinate3D<T>(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    
    public static bool WithinBoundsOf<TMatrix>(this Coordinate3D position, TMatrix[,,] matrix)
    {
        return position.X >= matrix.GetLowerBound(0) &&
               position.X <= matrix.GetUpperBound(0) &&
               position.Y >= matrix.GetLowerBound(1) &&
               position.Y <= matrix.GetUpperBound(1) &&
               position.Z >= matrix.GetLowerBound(2) &&
               position.Z <= matrix.GetUpperBound(2);
    }
    
    public static List<Coordinate3D> Neighbours(this Coordinate3D position)
    {
        return new List<Coordinate3D>
        {
            new(position.X - 1, position.Y, position.Z),
            new(position.X + 1, position.Y, position.Z),
            new(position.X, position.Y - 1, position.Z),
            new(position.X, position.Y + 1, position.Z),
            new(position.X, position.Y, position.Z - 1),
            new(position.X, position.Y, position.Z + 1)
        };
    }
    
    public static List<Coordinate3D<T>> Neighbours<T>(this Coordinate3D<T> position)
        where T : INumber<T>
    {
        var plus1 = T.Zero;
        plus1++;
        
        var minus1 = T.Zero;
        minus1--;
        
        return new List<Coordinate3D<T>>
        {
            new(position.X + minus1, position.Y, position.Z),
            new(position.X + plus1, position.Y, position.Z),
            new(position.X, position.Y + minus1, position.Z),
            new(position.X, position.Y + plus1, position.Z),
            new(position.X, position.Y, position.Z + minus1),
            new(position.X, position.Y, position.Z + plus1)
        };
    }
}