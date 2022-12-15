using FluentAssertions;

namespace Beacons.Tests;

public class BeaconsShould
{
    [Fact]
    public void Parse_Input_From_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
    
        var sut = new Beacons();
        sut.ParseInput(inputLines);

        sut.SensorsAndBeacons.Keys.Should().HaveCount(14);
        sut.SensorsAndBeacons.Keys.Contains(new Coordinate(2, 18)).Should().BeTrue();
        sut.SensorsAndBeacons.Keys.Contains(new Coordinate(20, 1)).Should().BeTrue();
        
        sut.SensorsAndBeacons.Values.Distinct().Should().HaveCount(6);
        sut.SensorsAndBeacons.Values.Contains(new Coordinate(-2, 15)).Should().BeTrue();
        sut.SensorsAndBeacons.Values.Contains(new Coordinate(15, 3)).Should().BeTrue();
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
        sut.ParseInput(inputLines);
     
        sut.GetCannotBeBeaconCountOnRow(rowIndex).Should().Be(expectedCount);
    }
}