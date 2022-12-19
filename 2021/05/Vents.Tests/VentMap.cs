using AoC.Common;
using FluentAssertions;

namespace Vents.Tests;

public class VentMap
{
    [Fact]
    public void Parse_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new Vents.VentMap();
        sut.ParseInput(inputLines);

        sut.Vents.Values.Count(x => x == 1).Should().Be(16);
        sut.Vents.Values.Count(x => x == 2).Should().Be(5);
        
        // top row 1
        sut.Vents[new Coordinate(7, 0)].Should().Be(1);
        // 5th row 2
        sut.Vents[new Coordinate(3, 4)].Should().Be(2);
        // bottom row
        sut.Vents[new Coordinate(0, 9)].Should().Be(2);
        sut.Vents[new Coordinate(1, 9)].Should().Be(2);
        sut.Vents[new Coordinate(2, 9)].Should().Be(2);
        sut.Vents[new Coordinate(3, 9)].Should().Be(1);
        sut.Vents[new Coordinate(4, 9)].Should().Be(1);
        sut.Vents[new Coordinate(5, 9)].Should().Be(1);
    }

    [Theory]
    [InlineData("SampleInput.txt", false, 5)]
    [InlineData("MyInput.txt", false, 6687)]
    [InlineData("SampleInput.txt", true, 12)]
    [InlineData("MyInput.txt", true, 19851)]
    public void Get_Count_Of_Dangerous_Points(
        string inputFile,
        bool includeDiagonals,
        int expectedCount)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        var sut = new Vents.VentMap();
        sut.ParseInput(inputLines, includeDiagonals);

        sut.GetDangerousPointsCount().Should().Be(expectedCount);
    }
    
    [Fact]
    public void Parse_Sample_Input_With_Diagonals()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new Vents.VentMap();
        sut.ParseInput(inputLines, includeDiagonals: true);

        sut.Vents.Values.Count(x => x == 1).Should().Be(27);
        sut.Vents.Values.Count(x => x == 2).Should().Be(10);
        sut.Vents.Values.Count(x => x == 3).Should().Be(2);
        
        // top row 1
        sut.Vents[new Coordinate(7, 0)].Should().Be(1);
        // 5th row 2
        sut.Vents[new Coordinate(3, 4)].Should().Be(2);
        // 5th row 3's
        sut.Vents[new Coordinate(4, 4)].Should().Be(3);
        sut.Vents[new Coordinate(6, 4)].Should().Be(3);
        // bottom row
        sut.Vents[new Coordinate(0, 9)].Should().Be(2);
        sut.Vents[new Coordinate(1, 9)].Should().Be(2);
        sut.Vents[new Coordinate(2, 9)].Should().Be(2);
        sut.Vents[new Coordinate(3, 9)].Should().Be(1);
        sut.Vents[new Coordinate(4, 9)].Should().Be(1);
        sut.Vents[new Coordinate(5, 9)].Should().Be(1);
    }
}