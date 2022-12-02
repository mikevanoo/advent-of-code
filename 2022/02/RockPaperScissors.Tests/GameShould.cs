using CalorieCounter.Tests;
using FluentAssertions;

namespace RockPaperScissors.Tests;

public class GameShould
{
    [Fact]
    public void Given_SampleInput_Get_Player_2_Total_Score_For_Part1()
    {
        var playLines = TestUtils.ReadEmbeddedResourceLines(@"TestData\SampleInput.txt");
        new Game().GetPlayer2TotalScoreForPart1(playLines).Should().Be(15);
    }
    
    [Fact]
    public void Given_MyInput_Get_Player_2_Total_Score_For_Part1()
    {
        var playLines = TestUtils.ReadEmbeddedResourceLines(@"TestData\MyInput.txt");
        new Game().GetPlayer2TotalScoreForPart1(playLines).Should().Be(13484);
    }
    
    [Fact]
    public void Given_SampleInput_Get_Player_2_Total_Score_For_Part2()
    {
        var playLines = TestUtils.ReadEmbeddedResourceLines(@"TestData\SampleInput.txt");
        new Game().GetPlayer2TotalScoreForPart2(playLines).Should().Be(12);
    }
    
    [Fact]
    public void Given_MyInput_Get_Player_2_Total_Score_For_Part2()
    {
        var playLines = TestUtils.ReadEmbeddedResourceLines(@"TestData\MyInput.txt");
        new Game().GetPlayer2TotalScoreForPart2(playLines).Should().Be(13433);
    }
}