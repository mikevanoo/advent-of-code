using FluentAssertions;

namespace AoC.Common.Tests;

public class StringExtensionsShould
{
    [Theory]
    [InlineData("1 2 3 55 0 66 -99", new[] { 1,2,3,55,0,66,-99 })]
    [InlineData("bob1 2 al3ice dave55 0johndoh66 mike-99", new[] { 1,2,3,55,0,66,-99 })]
    [InlineData("1 2 - 4", new[] { 1,2,4 })]
    public void Extract_Integers_From_A_String(string input, int[] expectedIntegers)
    {
        input.ExtractIntegers().Should().ContainInOrder(expectedIntegers);
    }
    
    [Theory]
    [InlineData("1 2 3 55 0 66 -99", new long[] { 1,2,3,55,0,66,-99 })]
    [InlineData("bob1 2 al3ice dave55 0johndoh66 mike-99", new long[] { 1,2,3,55,0,66,-99 })]
    [InlineData("1 2 - 4", new long[] { 1,2,4 })]
    public void Extract_Longs_From_A_String(string input, long[] expectedLongs)
    {
        input.ExtractLongs().Should().ContainInOrder(expectedLongs);
    }
}