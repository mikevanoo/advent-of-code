using FluentAssertions;

namespace CalorieCounter.Tests;

public class CalorieCounterShould
{
    [Fact]
    public void Given_The_SampleInput_File_Return_24_000()
    {
        var list = TestUtils.ReadEmbeddedResourceLines(@"TestData\SampleInput.txt");
        new CalorieCounter().FindMaxCalories(list).Should().Be(24_000);
    }
    
    [Fact]
    public void Given_The_MyInput_File_Return_70_613()
    {
        var list = TestUtils.ReadEmbeddedResourceLines(@"TestData\MyInput.txt");
        new CalorieCounter().FindMaxCalories(list).Should().Be(70613);
    }
}