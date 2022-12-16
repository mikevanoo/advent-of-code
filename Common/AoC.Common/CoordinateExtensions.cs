using System.Numerics;

namespace AoC.Common;

public static class CoordinateExtensions
{
    public static T ManhattanDistance<T>(this Coordinate<T> a, Coordinate<T> b)
        where T : INumber<T>
    {
        return T.Abs(a.X - b.X) + T.Abs(a.Y - b.Y);
    }
}