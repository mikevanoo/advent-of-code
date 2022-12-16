using FluentAssertions;

namespace AoC.Common.Tests;

public class NumberExtensionsShould
{
    [Theory]
    [InlineData(new[] { 1,2,3,55,0,66,-99,47 }, -99)]
    public void Find_The_Minimum_Of_Many_Integers(int[] values, int expectedMinimum)
    {
        values.Minimum().Should().Be(expectedMinimum);
    }
    
    [Theory]
    [InlineData(new long[] { 1,2,3,55,0,66,-99,47 }, -99)]
    public void Find_The_Minimum_Of_Many_Longs(long[] values, long expectedMinimum)
    {
        values.Minimum().Should().Be(expectedMinimum);
    }
    
    [Theory]
    [InlineData(new[] { 1,2,3,55,0,66,-99,47 }, 66)]
    public void Find_The_Maximum_Of_Many_Integers(int[] values, int expectedMaximum)
    {
        values.Maximum().Should().Be(expectedMaximum);
    }
    
    [Theory]
    [InlineData(new long[] { 1,2,3,55,0,66,-99,47 }, 66)]
    public void Find_The_Maximum_Of_Many_Longs(long[] values, long expectedMaximum)
    {
        values.Maximum().Should().Be(expectedMaximum);
    }
    
    [Theory]
    [InlineData(8, 12, 4)]
    [InlineData(5, 25, 5)]
    [InlineData(-8, 12, 4)]
    [InlineData(8, -12, -4)]
    [InlineData(-8, -12, -4)]
    [InlineData(8, 0, 8)]
    [InlineData(0, 8, 8)]
    [InlineData(-8, 0, -8)]
    [InlineData(0, -8, -8)]
    public void Find_The_Greatest_Common_Divisor_Of_Two_Integers(int a, int b, int expectedGcd)
    {
        NumberExtensions.GreatestCommonDivisor(a, b).Should().Be(expectedGcd);
    }
    
    [Theory]
    [InlineData(new[] { 5, 10, 15, 20, 25, 30 }, 5)]
    [InlineData(new[] { -5, 10, 15, 20, 25, 30 }, -5)]
    public void Find_The_Greatest_Common_Divisor_Of_Many_Integers(int[] values, int expectedGcd)
    {
        values.GreatestCommonDivisor().Should().Be(expectedGcd);
    }
    
    [Theory]
    [InlineData(8, 12, 4)]
    [InlineData(5, 25, 5)]
    [InlineData(-8, 12, 4)]
    [InlineData(8, -12, -4)]
    [InlineData(-8, -12, -4)]
    [InlineData(8, 0, 8)]
    [InlineData(0, 8, 8)]
    [InlineData(-8, 0, -8)]
    [InlineData(0, -8, -8)]
    public void Find_The_Greatest_Common_Divisor_Of_Two_Longs(long a, long b, long expectedGcd)
    {
        NumberExtensions.GreatestCommonDivisor(a, b).Should().Be(expectedGcd);
    }
    
    [Theory]
    [InlineData(new long[] { 5, 10, 15, 20, 25, 30 }, 5)]
    [InlineData(new long[] { -5, 10, 15, 20, 25, 30 }, -5)]
    public void Find_The_Greatest_Common_Divisor_Of_Many_Longs(long[] values, long expectedGcd)
    {
        values.GreatestCommonDivisor().Should().Be(expectedGcd);
    }
    
    [Theory]
    [InlineData(8, 12, 24)]
    [InlineData(12, 8, 24)]
    [InlineData(-12, 8, 24)]
    [InlineData(12, -8, -24)]
    [InlineData(0, 8, 0)]
    [InlineData(0, -8, 0)]
    [InlineData(30, 36, 180)]
    public void Find_The_Lowest_Common_Multiple_Of_Two_Integers(int a, int b, int expectedLcm)
    {
        NumberExtensions.LowestCommonMultiple(a, b).Should().Be(expectedLcm);
    }
    
    [Theory]
    [InlineData(new[] { 2, 4, 6, 8 }, 24)]
    [InlineData(new[] { -2, 4, 6, 8 }, 24)]
    public void Find_The_Lowest_Common_Multiple_Of_Many_Integers(int[] values, int expectedLcm)
    {
        values.LowestCommonMultiple().Should().Be(expectedLcm);
    }
    
    [Theory]
    [InlineData(8, 12, 24)]
    [InlineData(12, 8, 24)]
    [InlineData(-12, 8, 24)]
    [InlineData(12, -8, -24)]
    [InlineData(0, 8, 0)]
    [InlineData(0, -8, 0)]
    [InlineData(30, 36, 180)]
    public void Find_The_Lowest_Common_Multiple_Of_Two_Longs(long a, long b, long expectedLcm)
    {
        NumberExtensions.LowestCommonMultiple(a, b).Should().Be(expectedLcm);
    }
    
    [Theory]
    [InlineData(new long[] { 2, 4, 6, 8 }, 24)]
    [InlineData(new long[] { -2, 4, 6, 8 }, 24)]
    public void Find_The_Lowest_Common_Multiple_Of_Many_Longs(long[] values, long expectedLcm)
    {
        values.LowestCommonMultiple().Should().Be(expectedLcm);
    }
}