using FluentAssertions;

namespace Bingo.Tests;

public class BoardShould
{
    [Fact]
    public void Parse_Sample_Input()
    {
        var inputLines = new[]
        {
            "22 13 17 11  0",
            " 8  2 23  4 24",
            "21  9 14 16  7",
            " 6 10  3 18  5",
            " 1 12 20 15 19"
        };

        var sut = new Board();
        sut.ParseInput(inputLines);

        var expectedGrid = new int[5, 5]
        {
            { 22, 13, 17, 11, 0 },
            { 8, 2, 23, 4, 24 },
            { 21, 9, 14, 16, 7 },
            { 6, 10, 3, 18, 5 },
            { 1, 12, 20, 15, 19 }
        };
        
        sut.Grid.Should().BeEquivalentTo(expectedGrid);
    }
    
    [Theory]
    [InlineData(new[] { 1,2,3,4 }, false)]
    [InlineData(new[] { 5,6,7,8,9 }, false)]
    [InlineData(new[] { 22,13,17,11,0 }, true)]
    [InlineData(new[] { 17,23,14,3,20 }, true)]
    [InlineData(new[] { 7,4,9,5,11,17,23,2,0 }, false)]
    public void Determine_If_The_Board_Has_Won(
        int[] calledNumbers,
        bool expectedResult)
    {
        var inputLines = new[]
        {
            "22 13 17 11  0",
            " 8  2 23  4 24",
            "21  9 14 16  7",
            " 6 10  3 18  5",
            " 1 12 20 15 19"
        };

        var sut = new Board();
        sut.ParseInput(inputLines);
        
        sut.HasWon(calledNumbers).Should().Be(expectedResult);
    }
    
    [Theory]
    [InlineData(new[] { 50,51,52 }, 0)]
    [InlineData(new[] { 7,4,9,5,11,17,23,2,0,14,21,24 }, 188)]
    public void Determine_Sum_Of_Unmarked_Numbers(
        int[] calledNumbers,
        int expectedSum)
    {
        var inputLines = new[]
        {
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };

        var sut = new Board();
        sut.ParseInput(inputLines);
        sut.HasWon(calledNumbers);
        
        sut.SumOfUnmarkedNumbers.Should().Be(expectedSum);
    }
}