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
    
    [Theory]
    [InlineData("SampleInput.txt", 31)]
    [InlineData("MyInput.txt", 339)]
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