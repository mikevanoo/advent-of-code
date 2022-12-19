using FluentAssertions;

namespace AoC.Common.Tests;

public class Coordinate3DExtensionsShould
{
    [Theory]
    [InlineData(0, 0, 0, 5, 5, 5, 15)]
    [InlineData(5, 5, 5, 0, 0, 0, 15)]
    [InlineData(0, 0, 0, -5, -5, -5, 15)]
    [InlineData(-5, -5, -5, 0, 0, 0, 15)]
    public void Determine_The_Manhattan_Distance_Int(
        int startX, int startY, int startZ,
        int endX, int endY, int endZ,
        int expectedDistance)
    {
        var a = new Coordinate3D(startX, startY, startZ);
        var b = new Coordinate3D(endX, endY, endZ);

        a.ManhattanDistance(b).Should().Be(expectedDistance);
    }
    
    [Theory]
    [InlineData(0, 0, 0, 5, 5, 5, 15)]
    [InlineData(5, 5, 5, 0, 0, 0, 15)]
    [InlineData(0, 0, 0, -5, -5, -5, 15)]
    [InlineData(-5, -5, -5, 0, 0, 0, 15)]
    public void Determine_The_Manhattan_Distance_Long(
        long startX, long startY, long startZ,
        long endX, long endY, long endZ,
        long expectedDistance)
    {
        var a = new Coordinate3D<long>(startX, startY, startZ);
        var b = new Coordinate3D<long>(endX, endY, endZ);

        a.ManhattanDistance(b).Should().Be(expectedDistance);
    }
    
    [Theory]
    [InlineData(0, 0, 0, 5, 5, 5, 5, 5, 5)]
    [InlineData(0, 0, 0, -5, -5, -5, -5, -5, -5)]
    [InlineData(5, 5, 5, -5, -5, -5, 0, 0, 0)]
    [InlineData(5, 5, 5, -1, -2, -3, 4, 3, 2)]
    public void Add_To_Make_A_New_Coordinate3D_Int(
        int aX, int aY, int aZ,
        int bX, int bY, int bZ,
        int expectedX, int expectedY, int expectedZ)
    {
        var a = new Coordinate3D(aX, aY, aZ);
        var b = new Coordinate3D(bX, bY, bZ);
    
        a.Add(b).Should().Be(new Coordinate3D(expectedX, expectedY, expectedZ));
    }
    
    [Theory]
    [InlineData(0, 0, 0, 5, 5, 5, 5, 5, 5)]
    [InlineData(0, 0, 0, -5, -5, -5, -5, -5, -5)]
    [InlineData(5, 5, 5, -5, -5, -5, 0, 0, 0)]
    [InlineData(5, 5, 5, -1, -2, -3, 4, 3, 2)]
    public void Add_To_Make_A_New_Coordinate3D_Long(
        long aX, long aY, long aZ,
        long bX, long bY, long bZ,
        long expectedX, long expectedY, long expectedZ)
    {
        var a = new Coordinate3D<long>(aX, aY, aZ);
        var b = new Coordinate3D<long>(bX, bY, bZ);
    
        a.Add(b).Should().Be(new Coordinate3D<long>(expectedX, expectedY, expectedZ));
    }
    
    [Theory]
    [InlineData(0, 0, 0, 5, true)]
    [InlineData(4, 4, 4, 5, true)]
    [InlineData(5, 5, 5, 5, false)]
    [InlineData(-1, -1, -1, 5, false)]
    [InlineData(2, -1, 2, 5, false)]
    [InlineData(-1, 2, 2, 5, false)]
    [InlineData(2, 2, -1, 5, false)]
    [InlineData(5, 2, 2, 5, false)]
    [InlineData(2, 5, 2, 5, false)]
    [InlineData(2, 2, 5, 5, false)]
    public void Determine_If_A_Coordinate3D_Is_Within_The_Bounds_Of_A_Matrix(
        int x, int y, int z,
        int matrixSize,
        bool expectedResult)
    {
        var position = new Coordinate3D(x, y, z);
        var matrix = new string[matrixSize, matrixSize, matrixSize];
    
        position.WithinBoundsOf(matrix).Should().Be(expectedResult);
    }
    
    [Theory]
    [InlineData(2, 2, 2)]
    [InlineData(0, 0, 0)]
    public void Get_Neighbours_Of_Coordinate3D_Int(int x, int y, int z)
    {
        var position = new Coordinate3D(x, y, z);
        
        var actual = position.Neighbours();
        actual.Should().HaveCount(6);
        actual.Should().BeEquivalentTo(new List<Coordinate3D>
        {
            new(x - 1, y, z),
            new(x + 1, y, z),
            new(x, y - 1, z),
            new(x, y + 1, z),
            new(x, y, z - 1),
            new(x, y, z + 1)
        });
    }
    
    [Theory]
    [InlineData(2, 2, 2)]
    [InlineData(0, 0, 0)]
    public void Get_Neighbours_Of_Coordinate3D_Long(long x, long y, long z)
    {
        var position = new Coordinate3D<long>(x, y, z);
        
        var actual = position.Neighbours();
        actual.Should().HaveCount(6);
        actual.Should().BeEquivalentTo(new List<Coordinate3D<long>>
        {
            new(x - 1, y, z),
            new(x + 1, y, z),
            new(x, y - 1, z),
            new(x, y + 1, z),
            new(x, y, z - 1),
            new(x, y, z + 1)
        });
    }
}