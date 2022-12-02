using CalorieCounter.Tests;
using FluentAssertions;

namespace RockPaperScissors.Tests;

public class GameShould
{
    [Fact]
    public void Given_SampleInput_Get_Player_2_Total_Score()
    {
        var playLines = TestUtils.ReadEmbeddedResourceLines(@"TestData\SampleInput.txt");
        new Game().GetPlayer2TotalScore(playLines).Should().Be(15);
    }
    
    [Fact]
    public void Given_MyInput_Get_Player_2_Total_Score()
    {
        var playLines = TestUtils.ReadEmbeddedResourceLines(@"TestData\MyInput.txt");
        new Game().GetPlayer2TotalScore(playLines).Should().Be(13484);
    }
}