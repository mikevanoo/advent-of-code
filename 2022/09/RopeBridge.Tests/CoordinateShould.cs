using FluentAssertions;

namespace RopeBridge.Tests;

public class CoordinateShould
{
    [Theory]
    [InlineData(0, 0, Direction.U, 0, 1)]
    [InlineData(0, 0, Direction.D, 0, -1)]
    [InlineData(0, 0, Direction.R, 1, 0)]
    [InlineData(0, 0, Direction.L, -1, 0)]
    [InlineData(0, 0, Direction.UL, -1, 1)]
    [InlineData(0, 0, Direction.UR, 1, 1)]
    [InlineData(0, 0, Direction.DL, -1, -1)]
    [InlineData(0, 0, Direction.DR, 1, -1)]
    public void Move_Correctly(
        int initialX, int initialY,
        Direction move,
        int expectedX, int expectedY)
    {
        var sut = new Coordinate(initialX, initialY);
        sut.Move(move);
        sut.X.Should().Be(expectedX);
        sut.Y.Should().Be(expectedY);
    }
    
    [Fact]
    public void Record_Positions_Visited()
    {
        var sut = new Coordinate(0, 0);
        sut.Move(Direction.U);
        sut.PositionsVisited.Should().Be(2);
    }
    
    [Fact]
    public void Not_Record_Repeated_Positions_Visited()
    {
        var sut = new Coordinate(0, 0);
        sut.Move(Direction.U);
        sut.PositionsVisited.Should().Be(2);
        sut.Move(Direction.D);
        sut.PositionsVisited.Should().Be(2);
    }
}