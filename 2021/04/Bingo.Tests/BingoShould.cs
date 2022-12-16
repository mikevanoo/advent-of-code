using FluentAssertions;

namespace Bingo.Tests;

public class BingoShould
{
    [Fact]
    public void Parse_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new Bingo();

        sut.ParseInput(inputLines);

        sut.Boards.Should().HaveCount(3);
    }
    
    [Fact]
    public void Determine_3rd_Board_Wins_In_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new Bingo();

        sut.ParseInput(inputLines);
        sut.Play().Should().Be(2);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 4512)]
    [InlineData("MyInput.txt", 10680)]
    public void Determine_Score_Of_Winning_Board(
        string inputFile,
        int expectedScore)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        var sut = new Bingo();

        sut.ParseInput(inputLines);
        sut.Play();
        sut.WinningScore.Should().Be(expectedScore);
    }
}