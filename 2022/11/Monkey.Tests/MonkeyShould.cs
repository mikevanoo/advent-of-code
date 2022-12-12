using FluentAssertions;

namespace Monkey.Tests;

public class MonkeyShould
{
    [Theory]
    [InlineData(0, new[] { 79L, 98 }, "old", "*", "19", 23, 2, 3)]
    [InlineData(1, new[] { 54L, 65, 75, 74 }, "old", "+", "6", 19, 2, 0)]
    [InlineData(2, new[] { 79L, 60, 97 }, "old", "*", "old", 13, 1, 3)]
    [InlineData(3, new[] { 74L }, "old", "+", "3", 17, 0, 1)]
    public void Parse_Sample_Input(
        int monkeyIndex,
        long[] startingItems,
        string operationLeftOperand,
        string operationOperator,
        string operationRightOperand,
        int testDivisor,
        int trueMonkey,
        int falseMonkey)
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var monkeyLines = inputLines
            .Skip(monkeyIndex * 7)
            .Take(6)
            .ToArray();
        var sut = new Monkey();
        
        sut.ParseInput(monkeyLines);

        sut.Items.Should().ContainInOrder(startingItems);
        sut.OperationLeftOperand.Should().Be(operationLeftOperand);
        sut.OperationOperator.Should().Be(operationOperator);
        sut.OperationRightOperand.Should().Be(operationRightOperand);
        sut.TestDivisor.Should().Be(testDivisor);
        sut.TrueMonkeyIndex.Should().Be(trueMonkey);
        sut.FalseMonkeyIndex.Should().Be(falseMonkey);
    }
}