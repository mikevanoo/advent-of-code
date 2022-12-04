using FluentAssertions;

namespace CalorieCounter.Tests;

public class CalorieCounterShould
{
    [Fact]
    public void Given_SampleInput_Get_The_Max_Calorie_Count_Carried_By_A_Single_Elf()
    {
        var list = File.ReadAllLines(@"TestData\SampleInput.txt");
        new CalorieCounter().GetMaxCalorieCount(list).Should().Be(24_000);
    }
    
    [Fact]
    public void Given_MyInput_Get_The_Max_Calorie_Count_Carried_By_A_Single_Elf()
    {
        var list = File.ReadAllLines(@"TestData\MyInput.txt");
        new CalorieCounter().GetMaxCalorieCount(list).Should().Be(70613);
    }
    
    [Fact]
    public void Given_SampleInput_Get_The_Sum_Of_The_Top_Three_Calorie_Counts_Carried_By_The_Elves()
    {
        var list = File.ReadAllLines(@"TestData\SampleInput.txt");
        new CalorieCounter().GetSumOfTop3CalorieCounts(list).Should().Be(45_000);
    }
    
    [Fact]
    public void Given_MyInput_Get_The_Sum_Of_The_Top_Three_Calorie_Counts_Carried_By_The_Elves()
    {
        var list = File.ReadAllLines(@"TestData\MyInput.txt");
        new CalorieCounter().GetSumOfTop3CalorieCounts(list).Should().Be(205_805);
    }
}