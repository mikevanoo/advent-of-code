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
    
    [Theory]
    [InlineData(0, 1, new object[] { 2, 1, -3, 3, -2, 0, 4 })]
    [InlineData(0, 2, new object[] { 2, -3, 1, 3, -2, 0, 4 })]
    [InlineData(0, 3, new object[] { 2, -3, 3, 1, -2, 0, 4 })]
    [InlineData(0, 4, new object[] { 2, -3, 3, -2, 1, 0, 4 })]
    [InlineData(0, 5, new object[] { 2, -3, 3, -2, 0, 1, 4 })]
    [InlineData(0, 6, new object[] { 2, -3, 3, -2, 0, 4, 1 })]
    [InlineData(0, 7, new object[] { 1, -3, 3, -2, 0, 4, 2 })]
    [InlineData(0, -1, new object[] { 4, 2, -3, 3, -2, 0, 1 })]
    [InlineData(1, -1, new object[] { 2, 1, -3, 3, -2, 0, 4 })]
    public void Shift_Items_In_A_List_By_A_Number_Of_Positions_Wrapping_At_The_Start_And_End(
        int itemIndex,
        int number, 
        object[] expectedResult)
    {
        var initialState = new object[] { 1, 2, -3, 3, -2, 0, 4 };
        
        var actual = initialState.Shift(initialState[itemIndex], number);
        actual.Should().HaveCount(initialState.Length);
        actual.Should().ContainInOrder(expectedResult);
    }
}