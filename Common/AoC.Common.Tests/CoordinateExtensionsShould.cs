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
        var a = new Coordinate(startX, startY);
        var b = new Coordinate(endX, endY);

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
    
    [Theory]
    [InlineData(0, 0, 5, 5, 5, 5)]
    [InlineData(0, 0, -5, -5, -5, -5)]
    [InlineData(5, 5, -5, -5, 0, 0)]
    [InlineData(5, 5, -1, -2, 4, 3)]
    public void Add_To_Make_A_New_Coordinate_Int(
        int aX, int aY, 
        int bX, int bY,
        int expectedX, int expectedY)
    {
        var a = new Coordinate(aX, aY);
        var b = new Coordinate(bX, bY);

        a.Add(b).Should().Be(new Coordinate(expectedX, expectedY));
    }
    
    [Theory]
    [InlineData(0, 0, 5, 5, 5, 5)]
    [InlineData(0, 0, -5, -5, -5, -5)]
    [InlineData(5, 5, -5, -5, 0, 0)]
    [InlineData(5, 5, -1, -2, 4, 3)]
    public void Add_To_Make_A_New_Coordinate_Long(
        long aX, long aY, 
        long bX, long bY,
        long expectedX, long expectedY)
    {
        var a = new Coordinate<long>(aX, aY);
        var b = new Coordinate<long>(bX, bY);

        a.Add(b).Should().Be(new Coordinate<long>(expectedX, expectedY));
    }
    
    [Theory]
    [InlineData(0, 0, 5, true)]
    [InlineData(4, 4, 5, true)]
    [InlineData(5, 5, 5, false)]
    [InlineData(-1, -1, 5, false)]
    [InlineData(2, -1, 5, false)]
    [InlineData(-1, 2, 5, false)]
    [InlineData(5, 2, 5, false)]
    [InlineData(2, 5, 5, false)]
    public void Determine_If_A_Coordinate_Is_Within_The_Bounds_Of_A_Matrix(
        int x, int y, 
        int matrixSize,
        bool expectedResult)
    {
        var position = new Coordinate(x, y);
        var matrix = new string[matrixSize, matrixSize];

        position.WithinBoundsOf(matrix).Should().Be(expectedResult);
    }
    
    [Theory]
    [InlineData(2, 2)]
    [InlineData(0, 0)]
    public void Get_Neighbours_Of_Coordinate_Int(int x, int y)
    {
        var position = new Coordinate(x, y);
        
        var actual = position.Neighbours();
        actual.Should().HaveCount(4);
        actual.Should().BeEquivalentTo(new List<Coordinate>
        {
            new(x - 1, y),
            new(x + 1, y),
            new(x, y - 1),
            new(x, y + 1),
        });
    }
    
    [Theory]
    [InlineData(2, 2)]
    [InlineData(0, 0)]
    public void Get_Neighbours_With_Diagonals_Of_Coordinate_Int(int x, int y)
    {
        var position = new Coordinate(x, y);
        
        var actual = position.Neighbours(includeDiagonals: true);
        actual.Should().HaveCount(8);
        actual.Should().BeEquivalentTo(new List<Coordinate>
        {
            new(x - 1, y),
            new(x + 1, y),
            new(x, y - 1),
            new(x, y + 1),
            
            new(x - 1, y - 1),
            new(x + 1, y - 1),
            new(x - 1, y + 1),
            new(x + 1, y + 1)
        });
    }
    
    [Theory]
    [InlineData(2, 2)]
    [InlineData(0, 0)]
    public void Get_Neighbours_Of_Coordinate_Long(long x, long y)
    {
        var position = new Coordinate<long>(x, y);
        
        var actual = position.Neighbours();
        actual.Should().HaveCount(4);
        actual.Should().BeEquivalentTo(new List<Coordinate<long>>
        {
            new(x - 1, y),
            new(x + 1, y),
            new(x, y - 1),
            new(x, y + 1),
        });
    }
    
    [Theory]
    [InlineData(2, 2)]
    [InlineData(0, 0)]
    public void Get_Neighbours_With_Diagonals_Of_Coordinate_Long(long x, long y)
    {
        var position = new Coordinate<long>(x, y);
        
        var actual = position.Neighbours(includeDiagonals: true);
        actual.Should().HaveCount(8);
        actual.Should().BeEquivalentTo(new List<Coordinate<long>>
        {
            new(x - 1, y),
            new(x + 1, y),
            new(x, y - 1),
            new(x, y + 1),
            
            new(x - 1, y - 1),
            new(x + 1, y - 1),
            new(x - 1, y + 1),
            new(x + 1, y + 1)
        });
    }
}