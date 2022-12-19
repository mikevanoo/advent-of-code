using FluentAssertions;

namespace LanternFish.Tests;

public class LanternFishShould
{
    [Fact]
    public void Parse_Sample_Input()
    {
        var inputLine = File.ReadAllText(@"TestData\SampleInput.txt");
        var sut = new LanternFish();
        sut.ParseInput(inputLine);
        
        sut.Fish.Should().BeEquivalentTo(new List<long> { 0,1,1,2,1,0,0,0,0 });
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 18, 26)]
    [InlineData("SampleInput.txt", 80, 5934)]
    [InlineData("MyInput.txt", 80, 360610)]
    [InlineData("SampleInput.txt", 256, 26984457539)]
    [InlineData("MyInput.txt", 256, 1631629590423)]
    public void Count_Total_Fish_After_Reproducing(
        string inputFile,
        int reproduceForDays, 
        long expectedFishCount)
    {
        var inputLine = File.ReadAllText(@$"TestData\{inputFile}");
        var sut = new LanternFish();
        sut.ParseInput(inputLine);

        sut.Reproduce(reproduceForDays).Should().Be(expectedFishCount);
    }
}