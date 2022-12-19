using FluentAssertions;

namespace Lava.Tests;

public class LavaShould
{
    [Fact]
    public void Parse_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");

        var sut = new Lava();
        sut.ParseInput(inputLines);

        sut.LavaDroplets.Count.Should().Be(13);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 64)]
    [InlineData("MyInput.txt", 3466)]
    public void Get_Total_Surface_Area(string inputFile, int expectedResult)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");

        var sut = new Lava();
        sut.ParseInput(inputLines);

        sut.GetTotalSurfaceArea().Should().Be(expectedResult);
    }
}