using FluentAssertions;

namespace AoC.Common.Tests;

public class CoordinateExtensionsShould
{
    [Theory]
    [InlineData(0, 0, 5, 5, 10)]
    [InlineData(5, 5, 0, 0, 10)]
    [InlineData(0, 0, -5, -5, 10)]
    [InlineData(-5, -5, 0, 0, 10)]
    public void Determine_The_Manhattan_Distance_Int(
        int startX, int startY, 
        int endX, int endY,
        int expectedDistance)
    {
        var a = new Coordinate<int>(startX, startY);
        var b = new Coordinate<int>(endX, endY);

        a.ManhattanDistance(b).Should().Be(expectedDistance);
    }
    
    [Theory]
    [InlineData(0, 0, 5, 5, 10)]
    [InlineData(5, 5, 0, 0, 10)]
    [InlineData(0, 0, -5, -5, 10)]
    [InlineData(-5, -5, 0, 0, 10)]
    public void Determine_The_Manhattan_Distance_Long(
        long startX, long startY, 
        long endX, long endY,
        long expectedDistance)
    {
        var a = new Coordinate<long>(startX, startY);
        var b = new Coordinate<long>(endX, endY);

        a.ManhattanDistance(b).Should().Be(expectedDistance);
    }
}