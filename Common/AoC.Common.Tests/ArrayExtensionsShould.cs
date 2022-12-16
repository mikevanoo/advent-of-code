using System.Reflection;
using FluentAssertions;
using Xunit.Sdk;

namespace AoC.Common.Tests;

public class ArrayExtensionsShould
{
    [Theory]
    [InlineData(0, new[] { 1,6,11,16,21 })]
    [InlineData(2, new[] { 3,8,13,18,23 })]
    [InlineData(4, new[] { 5,10,15,20,25 })]
    public void Get_The_Column_From_A_Matrix(int columnIndex, int[] expectedColumn)
    {
        var matrix = new int[5, 5]
        {
            { 1, 2, 3, 4, 5 },
            { 6, 7, 8, 9, 10 },
            { 11, 12, 13, 14, 15 },
            { 16, 17, 18, 19, 20 },
            { 21, 22, 23, 24, 25 }
        };

        matrix.GetColumn(columnIndex).Should().ContainInOrder(expectedColumn);
    }
    
    [Theory]
    [InlineData(0, new[] { 1,2,3,4,5 })]
    [InlineData(2, new[] { 11,12,13,14,15 })]
    [InlineData(4, new[] { 21,22,23,24,25 })]
    public void Get_The_Row_From_A_Matrix(int rowIndex, int[] expectedRow)
    {
        var matrix = new int[5, 5]
        {
            { 1, 2, 3, 4, 5 },
            { 6, 7, 8, 9, 10 },
            { 11, 12, 13, 14, 15 },
            { 16, 17, 18, 19, 20 },
            { 21, 22, 23, 24, 25 }
        };

        matrix.GetRow(rowIndex).Should().ContainInOrder(expectedRow);
    }
    
    [Fact]
    public void Flatten_A_Matrix()
    {
        var matrix = new int[5, 5]
        {
            { 1, 2, 3, 4, 5 },
            { 6, 7, 8, 9, 10 },
            { 11, 12, 13, 14, 15 },
            { 16, 17, 18, 19, 20 },
            { 21, 22, 23, 24, 25 }
        };

        var expectedNumbers = Enumerable.Range(1, 25).ToList();
        var actual = matrix.Flatten().ToList();

        actual.Count.Should().Be(expectedNumbers.Count);
        for (var index = 0; index < expectedNumbers.Count; index++)
        {
            actual[index].Should().Be(expectedNumbers[index]);
        }
    }
}