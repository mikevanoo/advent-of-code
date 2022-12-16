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
    [InlineData(new[] { 1,2,3,55,0,66,-99,47 }, 66)]
    public void Find_The_Maximum_Of_Many_Integers(int[] values, int expectedMaximum)
    {
        values.Maximum().Should().Be(expectedMaximum);
    }
}