using FluentAssertions;

namespace CalorieCounter.Tests;

public class CalorieCounterShould
{
    [Fact]
    public void Given_SampleInput_Find_The_Max_Calories_Carried_By_A_Single_Elf()
    {
        var list = TestUtils.ReadEmbeddedResourceLines(@"TestData\SampleInput.txt");
        new CalorieCounter().FindMaxCalories(list).Should().Be(24_000);
    }
    
    [Fact]
    public void Given_MyInput_Find_The_Max_Calories_Carried_By_A_Single_Elf()
    {
        var list = TestUtils.ReadEmbeddedResourceLines(@"TestData\MyInput.txt");
        new CalorieCounter().FindMaxCalories(list).Should().Be(70613);
    }
    
    [Fact]
    public void Given_SampleInput_Find_The_Sum_Of_The_Top_Three_Calories_Carried_By_The_Elves()
    {
        var list = TestUtils.ReadEmbeddedResourceLines(@"TestData\SampleInput.txt");
        new CalorieCounter().FindSumOfTop3Calories(list).Should().Be(45_000);
    }
    
    [Fact]
    public void Given_MyInput_Find_The_Sum_Of_The_Top_Three_Calories_Carried_By_The_Elves()
    {
        var list = TestUtils.ReadEmbeddedResourceLines(@"TestData\MyInput.txt");
        new CalorieCounter().FindSumOfTop3Calories(list).Should().Be(205_805);
    }
}