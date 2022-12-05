using FluentAssertions;

namespace AssignmentPairs.Tests;

public class AssignmentPairsShould
{
    [Theory]
    [InlineData("SampleInput.txt", 2)]
    [InlineData("MyInput.txt", 569)]
    public void Determine_How_Many_Pairs_Where_One_Range_Fully_Covers_The_Other(
        string inputFile, 
        int expectedNumberOfPairs)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        var sut = new AssignmentPairs();
        
        sut.GetNumberOfPairsWhereOneRangeFullyCoversTheOther(inputLines).Should()
            .Be(expectedNumberOfPairs);
    }
    
    [Theory]
    [InlineData("2-8", 2, 8)]
    [InlineData("6-6", 6, 6)]
    [InlineData("61-69", 61, 69)]
    public void Parse_A_Range_String(
        string range, 
        int expectedStart,
        int expectedEnd)
    {
        var actual = new AssignmentPairs().ParseRange(range);
        actual.Start.Should().Be(expectedStart);
        actual.End.Should().Be(expectedEnd);
    }
}