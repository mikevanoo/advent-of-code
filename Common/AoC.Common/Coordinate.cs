using System.Numerics;

namespace AoC.Common;

public record struct Coordinate(int X, int Y); 

public record struct Coordinate<T>(T X, T Y) where T : INumber<T>;
