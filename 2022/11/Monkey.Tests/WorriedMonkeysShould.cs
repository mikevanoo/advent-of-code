using System.Runtime.InteropServices;
using FluentAssertions;

namespace Monkey.Tests;

public class WorriedMonkeysShould
{
    [Fact]
    public void Parse_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys();
        
        sut.ParseInput(inputLines);

        sut.Monkeys.Count.Should().Be(4);
    }
    
    [Theory]
    [InlineData(1, new[] { 20, 23, 27, 26 }, new[] { 2080, 25, 167, 207, 401, 1046 })]
    [InlineData(2, new[] { 695, 10, 71, 135, 350 }, new[] { 43, 49, 58, 55, 362 })]
    [InlineData(3, new[] { 16, 18, 21, 20, 122 }, new[] { 1468, 22, 150, 286, 739 })]
    [InlineData(4, new[] { 491, 9, 52, 97, 248, 34 }, new[] { 39, 45, 43, 258 })]
    [InlineData(5, new[] { 15, 17, 16, 88, 1037 }, new[] { 20, 110, 205, 524, 72 })]
    [InlineData(6, new[] { 8, 70, 176, 26, 34 }, new[] { 481, 32, 36, 186, 2190 })]
    [InlineData(7, new[] { 162, 12, 14, 64, 732, 17 }, new[] { 148, 372, 55, 72 })]
    [InlineData(8, new[] { 51, 126, 20, 26, 136 }, new[] { 343, 26, 30, 1546, 36 })]
    [InlineData(9, new[] { 116, 10, 12, 517, 14 }, new[] { 108, 267, 43, 55, 288 })]
    [InlineData(10, new[] { 91, 16, 20, 98 }, new[] { 481, 245, 22, 26, 1092, 30 })]
    [InlineData(15, new[] { 83, 44, 8, 184, 9, 20, 26, 102 }, new[] { 110, 36 })]
    [InlineData(20, new[] {10, 12, 14, 26, 34 }, new[] { 245, 93, 53, 199, 115 })]
    public void Play_Rounds_On_Sample_Input(
        int roundCount,
        int[] monkey0Items,
        int[] monkey1Items)
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys();
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < roundCount; i++)
        {
            sut.PlayRound();    
        }
        
        sut.Monkeys[0].Items.Should().ContainInOrder(monkey0Items);
        sut.Monkeys[1].Items.Should().ContainInOrder(monkey1Items);
        sut.Monkeys[2].Items.Should().ContainInOrder(Array.Empty<int>());
        sut.Monkeys[3].Items.Should().ContainInOrder(Array.Empty<int>());
    }
    
    [Fact]
    public void Calculate_Inspection_Counts_After_20_Rounds_On_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys();
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < 20; i++)
        {
            sut.PlayRound();    
        }

        sut.Monkeys[0].InspectedItemsCount.Should().Be(101);
        sut.Monkeys[1].InspectedItemsCount.Should().Be(95);
        sut.Monkeys[2].InspectedItemsCount.Should().Be(7);
        sut.Monkeys[3].InspectedItemsCount.Should().Be(105);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 10605)]
    [InlineData("MyInput.txt", 72884)]
    public void Calculate_Monkey_Business_Level_After_20_Rounds(
        string inputFile,
        int expectedMonkeyBusiness)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        var sut = new WorriedMonkeys();
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < 20; i++)
        {
            sut.PlayRound();    
        }

        sut.GetMonkeyBusinessLevel().Should().Be(expectedMonkeyBusiness);
    }
}