using FluentAssertions;
using Xunit.Abstractions;

namespace Beacons.Tests;

public class DeadzoneShould
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DeadzoneShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(8, -3, false, "above top")]
    [InlineData(7, -2, false, "left of top")]
    [InlineData(8, -2, true, "top point")]
    [InlineData(9, -2, false, "right of top")]
    [InlineData(7, -1, true, "too far left")]
    [InlineData(8, -1, true, "inside")]
    [InlineData(9, -1, true, "inside")]
    [InlineData(10, -1, false, "too far right")]
    [InlineData(-2, 7, false, "left of left")]
    [InlineData(-1, 7, true, "left point")]
    [InlineData(0, 7, true, "right of left")]
    [InlineData(8, 7, true, "centre point")]
    [InlineData(17, 7, true, "right point")]
    [InlineData(18, 7, false, "right of right")]
    [InlineData(8, 15, true, "above bottom")]
    [InlineData(8, 16, true, "bottom point")]
    [InlineData(7, 16, false, "left of bottom")]
    [InlineData(9, 16, false, "right of bottom")]
    public void Determine_If_The_Given_Point_Is_Contained_Within_The_Deadzone(
        int x,
        int y,
        bool expected,
        string because)
    {
        new Deadzone(new Coordinate(8, 7), 9).Contains(new Coordinate(x, y)).Should().Be(expected, because);
    }
    
    [Theory]
    [InlineData(8, -2, -1, 7)]
    public void Determine_If_The_Points_Along_Top_To_Left_Edge_Are_Contained_Within_The_Deadzone(
        int startX,
        int startY,
        int endX,
        int endY)
    {
        var sut = new Deadzone(new Coordinate(8, 7), 9);
        var currentX = startX;
        var currentY = startY;
        
        while (currentX >= endX && currentY <= endY)
        {
            _testOutputHelper.WriteLine($"Checking  {currentX}, {currentY}");
            sut.Contains(new Coordinate(currentX, currentY)).Should().Be(true);
            currentX--;
            currentY++;
        }
    }
    
    [Theory]
    [InlineData(8, -2, 17, 7)]
    public void Determine_If_The_Points_Along_Top_To_Right_Edge_Are_Contained_Within_The_Deadzone(
        int startX,
        int startY,
        int endX,
        int endY)
    {
        var sut = new Deadzone(new Coordinate(8, 7), 9);
        var currentX = startX;
        var currentY = startY;
        
        while (currentX <= endX && currentY <= endY)
        {
            _testOutputHelper.WriteLine($"Checking  {currentX}, {currentY}");
            sut.Contains(new Coordinate(currentX, currentY)).Should().Be(true);
            currentX++;
            currentY++;
        }
    }
    
    [Theory]
    [InlineData(8, 16, 17, 7)]
    public void Determine_If_The_Points_Along_Bottom_To_Right_Edge_Are_Contained_Within_The_Deadzone(
        int startX,
        int startY,
        int endX,
        int endY)
    {
        var sut = new Deadzone(new Coordinate(8, 7), 9);
        var currentX = startX;
        var currentY = startY;
        
        while (currentX <= endX && currentY >= endY)
        {
            _testOutputHelper.WriteLine($"Checking  {currentX}, {currentY}");
            
            // TODO many cases on this edge fail!!
            sut.Contains(new Coordinate(currentX, currentY)).Should().Be(true);
            
            currentX++;
            currentY--;
        }
    }
    
    [Theory]
    [InlineData(8, 16, -1, 7)]
    public void Determine_If_The_Points_Along_Bottom_To_Left_Edge_Are_Contained_Within_The_Deadzone(
        int startX,
        int startY,
        int endX,
        int endY)
    {
        var sut = new Deadzone(new Coordinate(8, 7), 9);
        var currentX = startX;
        var currentY = startY;
        
        while (currentX >= endX && currentY >= endY)
        {
            _testOutputHelper.WriteLine($"Checking  {currentX}, {currentY}");
            
            // TODO many cases on this edge fail!!
            sut.Contains(new Coordinate(currentX, currentY)).Should().Be(true);
            
            currentX--;
            currentY--;
        }
    }
}