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
        // 4th row 2
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
    [InlineData("SampleInput.txt", 5)]
    [InlineData("MyInput.txt", 6687)]
    public void Get_Count_Of_Dangerous_Points(string inputFile, int expectedCount)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        var sut = new Vents.VentMap();
        sut.ParseInput(inputLines);

        sut.GetDangerousPointsCount().Should().Be(expectedCount);
    }
}