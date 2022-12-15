using FluentAssertions;
using Xunit.Abstractions;

namespace Beacons.Tests;

public class BeaconsShould
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BeaconsShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Create_Cells_From_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
    
        var sut = new Beacons();
        sut.CreateCells(inputLines);

        sut.SensorAndBeacons.Keys.Should().HaveCount(14);
        sut.SensorAndBeacons.Keys.Contains(new Coordinate(2, 18)).Should().BeTrue();
        sut.SensorAndBeacons.Keys.Contains(new Coordinate(20, 1)).Should().BeTrue();
        
        sut.SensorAndBeacons.Values.Distinct().Should().HaveCount(6);
        sut.SensorAndBeacons.Values.Contains(new Coordinate(-2, 15)).Should().BeTrue();
        sut.SensorAndBeacons.Values.Contains(new Coordinate(15, 3)).Should().BeTrue();

        sut.Cells.Values.Count(v => v == CellContents.Sensor).Should().Be(14);
        sut.Cells.Values.Count(v => v == CellContents.Beacon).Should().Be(6);
    }

    [Theory]
    [InlineData("SampleInput.txt", 10, 26)]
    [InlineData("MyInput.txt", 2000000, 26)]
    public void Determine_Count_Where_Beacons_Cannot_Be_On_Row(
        string inputFile,
        int rowIndex,
        int expectedCount)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
    
        var sut = new Beacons();
        sut.CreateCells(inputLines);
        sut.DetectBeacons();
        
        // _testOutputHelper.WriteLine(sut.PrintGrid());

        sut.GetCannotBeBeaconCountOnRow(rowIndex).Should().Be(expectedCount);
    }
}