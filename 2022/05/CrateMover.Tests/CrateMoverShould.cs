using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using FluentAssertions;

namespace CrateMover.Tests;

public class CrateMoverShould
{
    [Fact]
    public void Parse_The_Stacks_From_Sample_Input()
    {
        var inputLines = File.ReadAllLines($@"TestData\SampleInput.txt");
        var sut = new CrateMover();

        var expectedStacks = new List<CrateStack>
        {
            new CrateStack
            {
                Number = 1,
                Crates = new Stack<char>(new[] { 'Z', 'N' })
            },
            new CrateStack
            {
                Number = 2,
                Crates = new Stack<char>(new[] { 'M', 'C', 'D' })
            },
            new CrateStack
            {
                Number = 3,
                Crates = new Stack<char>(new[] { 'P' })
            }
        };

        var actual = sut.ParseStacks(inputLines);

        actual.Should().BeEquivalentTo(expectedStacks, options => options
            .Using<Stack<char>>(ctx => ctx.Subject.ToArray().Should().ContainInOrder(ctx.Expectation.ToArray()))
            .WhenTypeIs<Stack<char>>());
    }

    [Theory]
    [InlineData(" 1   2   3 ", 0, '1')]
    [InlineData(" 1   2   3 ", 1, '2')]
    [InlineData(" 1   2   3 ", 2, '3')]
    [InlineData(" 1   2   3   4   5   6   7   8", 7, '8')]
    public void Read_Chars_From_The_Correct_Position(
        string inputLine,
        int stackIndex,
        char expectedCharacter)
    {
        new CrateMover().GetStackCharacter(inputLine, stackIndex).Should().Be(expectedCharacter);
    }
}