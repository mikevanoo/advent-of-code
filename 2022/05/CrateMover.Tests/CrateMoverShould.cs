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
    public void Read_Stack_Chars_From_The_Correct_Position(
        string inputLine,
        int stackIndex,
        char expectedCharacter)
    {
        new CrateMover().GetStackCharacter(inputLine, stackIndex).Should().Be(expectedCharacter);
    }
    
    [Fact]
    public void Parse_The_Moves_From_Sample_Input()
    {
        var inputLines = File.ReadAllLines($@"TestData\SampleInput.txt");
        var sut = new CrateMover();

        var expectedMoves = new List<CrateMove>
        {
            new CrateMove
            {
                Quantity = 1,
                SourceStackNumber = 2,
                DestinationStackNumber = 1
            },
            new CrateMove
            {
                Quantity = 3,
                SourceStackNumber = 1,
                DestinationStackNumber = 3
            },
            new CrateMove
            {
                Quantity = 2,
                SourceStackNumber = 2,
                DestinationStackNumber = 1
            },
            new CrateMove
            {
                Quantity = 1,
                SourceStackNumber = 1,
                DestinationStackNumber = 2
            }
        };

        var actual = sut.ParseMoves(inputLines);

        actual.Should().BeEquivalentTo(expectedMoves);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", "CMZ")]
    [InlineData("MyInput.txt", "DHBJQJCCW")]
    public void Get_Tops_Of_Stacks_After_Moves(
        string inputFile,
        string expectedTopsOfStacks)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new CrateMover().MoveStacks(inputLines).Should().BeEquivalentTo(expectedTopsOfStacks);
    }
}