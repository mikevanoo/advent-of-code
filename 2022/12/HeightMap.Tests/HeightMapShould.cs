using FluentAssertions;

namespace HeightMap.Tests;

public class HeightMapShould
{
    [Fact]
    public void Parse_The_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new HeightMap();
        sut.ParseInput(inputLines);

        sut.Grid.GetUpperBound(0).Should().Be(4);
        sut.Grid.GetUpperBound(1).Should().Be(7);

        sut.Grid[0, 0].Should().Be('S');
        sut.Grid[2, 5].Should().Be('E');
    }

    // [Theory]
    // [InlineData(0, 0, 0, 1)]
    // [InlineData(0, 1, 1, 1)]
    // [InlineData(1, 1, 1, 2)]
    // [InlineData(1, 2, 1, 3)]
    // public void Find_Next_Move_In_Sample_Input(
    //     int startX,
    //     int startY,
    //     int expectedX,
    //     int expectedY)
    // {
    //     var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
    //     var sut = new HeightMap();
    //     sut.ParseInput(inputLines);
    //
    //     sut.CurrentPosition = new Coordinate(startX, startY);
    //     sut.FindNextMove();
    //
    //     sut.CurrentPosition.Should().Be(new Coordinate(expectedX, expectedY));
    // }
    
    [Theory]
    [InlineData("SampleInput.txt", 31)]
    public void Get_Fewest_Steps_To_Best_Signal(
        string inputFile,
        int expectedStepCount)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        var sut = new HeightMap();
        sut.ParseInput(inputLines);

        sut.GetFewestStepsToBestSignal().Should().Be(expectedStepCount);
    }
    
}