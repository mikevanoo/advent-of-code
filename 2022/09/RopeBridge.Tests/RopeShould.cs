using FluentAssertions;

namespace RopeBridge.Tests;

public class RopeShould
{
    [Theory]
    [InlineData(0, 0, "R 4", 4, 0)]
    [InlineData(5, 1, "U 2", 5, 3)]
    public void Move_Head_Correctly(
        int initialX, int initialY,
        string move,
        int expectedX, int expectedY)
    {
        var sut = new Rope(new Coordinate(initialX, initialY));
        sut.Move(move);
        sut.HeadPosition.Should().Be(new Coordinate(expectedX, expectedY));
    }
    
    [Theory]
    [InlineData(0, 0, 0, 0, "R 1", 0, 0)]
    [InlineData(1, 0, 0, 0, "R 1", 1, 0)]
    [InlineData(4, 0, 3, 0, "U 1", 3, 0)]
    [InlineData(4, 1, 3, 0, "U 1", 4, 1)]
    [InlineData(4, 4, 4, 3, "L 1", 4, 3)]
    [InlineData(3, 4, 4, 3, "L 1", 3, 4)]
    [InlineData(1, 4, 2, 4, "D 1", 2, 4)]
    [InlineData(1, 3, 2, 4, "D 1", 1, 3)]
    [InlineData(2, 3, 2, 4, "R 1", 2, 4)]
    [InlineData(3, 3, 2, 4, "R 1", 3, 3)]
    [InlineData(4, 2, 4, 3, "L 1", 4, 3)]
    [InlineData(3, 2, 4, 3, "L 1", 3, 2)]
    public void Move_Tail_To_Touch_Head(
        int initialHeadX, int initialHeadY,
        int initialTailX, int initialTailY,
        string move,
        int expectedTailX, int expectedTailY)
    {
        var sut = new Rope(
            new Coordinate(initialHeadX, initialHeadY),
            new Coordinate(initialTailX, initialTailY));
        
        sut.Move(move);
        
        sut.TailPosition.Should().Be(new Coordinate(expectedTailX, expectedTailY));
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 13)]
    // [InlineData("MyInput.txt", 6077)]
    public void Count_Positions_Tail_Visited(
        string inputFile, 
        int expectedCount)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        var sut = new Rope();
        
        sut.Move(inputLines);

        sut.TailPosition.PositionsVisited.Should().Be(expectedCount);
    }
}