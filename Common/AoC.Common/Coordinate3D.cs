using System.Numerics;

namespace AoC.Common;

public record struct Coordinate3D(int X, int Y, int Z); 

public record struct Coordinate3D<T>(T X, T Y, T Z) where T : INumber<T>;