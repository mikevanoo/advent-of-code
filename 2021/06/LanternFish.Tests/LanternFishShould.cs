using System.Runtime.InteropServices;
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
        
        sut.Fish.Should().BeEquivalentTo(new List<int> { 3,4,3,1,2 });
    }
    
    [Theory]
    [InlineData(1, new[] { 2,3,2,0,1 })]
    [InlineData(2, new[] { 1,2,1,6,0,8 })]
    [InlineData(3, new[] { 0,1,0,5,6,7,8 })]
    [InlineData(4, new[] { 6,0,6,4,5,6,7,8,8 })]
    [InlineData(18, new[] { 6,0,6,4,5,6,0,1,1,2,6,0,1,1,1,2,2,3,3,4,6,7,8,8,8,8 })]
    public void Reproduce_The_Correct_Aged_Fish_For_The_Sample_Input(
        int reproduceForDays, 
        int[] expectedFish)
    {
        var inputLine = File.ReadAllText(@"TestData\SampleInput.txt");
        var sut = new LanternFish();
        sut.ParseInput(inputLine);

        sut.Reproduce(reproduceForDays);

        sut.Fish.Should().HaveCount(expectedFish.Length);
        sut.Fish.Should().ContainInOrder(expectedFish);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 18, 26)]
    [InlineData("SampleInput.txt", 80, 5934)]
    [InlineData("MyInput.txt", 80, 360610)]
    public void Count_Total_Fish_After_Reproducing(
        string inputFile,
        int reproduceForDays, 
        int expectedFishCount)
    {
        var inputLine = File.ReadAllText(@$"TestData\{inputFile}");
        var sut = new LanternFish();
        sut.ParseInput(inputLine);

        sut.Reproduce(reproduceForDays);

        sut.Fish.Should().HaveCount(expectedFishCount);
    }
}