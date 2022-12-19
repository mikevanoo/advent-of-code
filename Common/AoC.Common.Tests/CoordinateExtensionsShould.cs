using FluentAssertions;
using Xunit.Abstractions;

namespace AoC.Common.Tests;

public class CoordinateExtensionsShould
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CoordinateExtensionsShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
    
    [Theory]
    [InlineData(-1, -2, 5, 5)]
    public void Draw_A_Straight_Path_With_Invalid_Coordinates_Throws(
        int startX, int startY,
        int endX, int endY)
    {
        var start = new Coordinate(startX, startY);
        var end = new Coordinate(endX, endY);
        
        var actual = () => start.DrawStraightPathTo(end);

        actual.Should().ThrowExactly<ArgumentException>();
    }
    
    [Theory]
    [InlineData(1, 1, 1, 3, 3)]
    [InlineData(9, 7, 7, 7, 3)]
    [InlineData(0, 9, 5, 9, 6)]
    [InlineData(8, 0, 0, 8, 9)]
    [InlineData(9, 4, 3, 4, 7)]
    [InlineData(2, 2, 2, 1, 2)]
    [InlineData(7, 0, 7, 4, 5)]
    [InlineData(6, 4, 2, 0, 5)]
    [InlineData(0, 9, 2, 9, 3)]
    [InlineData(3, 4, 1, 4, 3)]
    [InlineData(0, 0, 8, 8, 9)]
    [InlineData(5, 5, 8, 2, 4)]
    public void Draw_A_Straight_Path_From_A_Coordinate_To_Another(
        int startX, int startY,
        int endX, int endY,
        int expectedStepCount)
    {
        var start = new Coordinate(startX, startY);
        var end = new Coordinate(endX, endY);
        
        var actual = start.DrawStraightPathTo(end);

        foreach (var coordinate in actual)
        {
            _testOutputHelper.WriteLine(coordinate.ToString());
        }
        
        actual.Should().HaveCount(expectedStepCount);
    }
}