using System.Runtime.InteropServices;
using FluentAssertions;

namespace Monkey.Tests;

public class WorriedMonkeysShould
{
    [Fact]
    public void Parse_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys(reliefFactor: 3);
        
        sut.ParseInput(inputLines);

        sut.Monkeys.Count.Should().Be(4);
    }
    
    [Theory]
    [InlineData(1, new[] { 20L, 23, 27, 26 }, new[] { 2080L, 25, 167, 207, 401, 1046 })]
    [InlineData(2, new[] { 695L, 10, 71, 135, 350 }, new[] { 43L, 49, 58, 55, 362 })]
    [InlineData(3, new[] { 16L, 18, 21, 20, 122 }, new[] { 1468L, 22, 150, 286, 739 })]
    [InlineData(4, new[] { 491L, 9, 52, 97, 248, 34 }, new[] { 39L, 45, 43, 258 })]
    [InlineData(5, new[] { 15L, 17, 16, 88, 1037 }, new[] { 20L, 110, 205, 524, 72 })]
    [InlineData(6, new[] { 8L, 70, 176, 26, 34 }, new[] { 481L, 32, 36, 186, 2190 })]
    [InlineData(7, new[] { 162L, 12, 14, 64, 732, 17 }, new[] { 148L, 372, 55, 72 })]
    [InlineData(8, new[] { 51L, 126, 20, 26, 136 }, new[] { 343L, 26, 30, 1546, 36 })]
    [InlineData(9, new[] { 116L, 10, 12, 517, 14 }, new[] { 108L, 267, 43, 55, 288 })]
    [InlineData(10, new[] { 91L, 16, 20, 98 }, new[] { 481L, 245, 22, 26, 1092, 30 })]
    [InlineData(15, new[] { 83L, 44, 8, 184, 9, 20, 26, 102 }, new[] { 110L, 36 })]
    [InlineData(20, new[] {10L, 12, 14, 26, 34 }, new[] { 245L, 93, 53, 199, 115 })]
    public void Play_Rounds_On_Sample_Input(
        int roundCount,
        long[] monkey0Items,
        long[] monkey1Items)
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys(reliefFactor: 3);
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < roundCount; i++)
        {
            sut.PlayRound();    
        }
        
        sut.Monkeys[0].Items.Should().ContainInOrder(monkey0Items);
        sut.Monkeys[1].Items.Should().ContainInOrder(monkey1Items);
        sut.Monkeys[2].Items.Should().ContainInOrder(Array.Empty<long>());
        sut.Monkeys[3].Items.Should().ContainInOrder(Array.Empty<long>());
    }
    
    [Fact]
    public void Calculate_Inspection_Counts_After_20_Rounds_With_Relief_Factor_3_On_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys(reliefFactor: 3);
        
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
        var sut = new WorriedMonkeys(reliefFactor: 3);
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < 20; i++)
        {
            sut.PlayRound();    
        }

        sut.GetMonkeyBusinessLevel().Should().Be(expectedMonkeyBusiness);
    }
    
    [Fact]
    public void Calculate_Inspection_Counts_After_20_Rounds_With_Relief_Factor_0_On_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys(reliefFactor: 0);
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < 20; i++)
        {
            sut.PlayRound();    
        }

        sut.Monkeys[0].InspectedItemsCount.Should().Be(99);
        sut.Monkeys[1].InspectedItemsCount.Should().Be(97);
        sut.Monkeys[2].InspectedItemsCount.Should().Be(8);
        sut.Monkeys[3].InspectedItemsCount.Should().Be(103);
    }
    
    [Fact]
    public void Calculate_Inspection_Counts_After_10_000_Rounds_With_Relief_Factor_0_On_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var sut = new WorriedMonkeys(reliefFactor: 0);
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < 10_000; i++)
        {
            sut.PlayRound();    
        }

        sut.Monkeys[0].InspectedItemsCount.Should().Be(52166);
        sut.Monkeys[1].InspectedItemsCount.Should().Be(47830);
        sut.Monkeys[2].InspectedItemsCount.Should().Be(1938);
        sut.Monkeys[3].InspectedItemsCount.Should().Be(52013);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 2713310158L)]
    [InlineData("MyInput.txt", 15310845153L)]
    public void Calculate_Monkey_Business_Level_After_10_000_Rounds(
        string inputFile,
        long expectedMonkeyBusiness)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        var sut = new WorriedMonkeys(reliefFactor: 0);
        
        sut.ParseInput(inputLines);

        for (var i = 0; i < 10_000; i++)
        {
            sut.PlayRound();    
        }

        sut.GetMonkeyBusinessLevel().Should().Be(expectedMonkeyBusiness);
    }
}