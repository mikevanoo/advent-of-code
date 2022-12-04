using FluentAssertions;

namespace Rucksacks.Tests;

public class RucksackShould
{
    [Fact]
    public void Get_Misplaced_Items_In_Sample_Input()
    {
        var rucksackLines = File.ReadAllLines(@"TestData\SampleInput.txt"); 
        var expectedMisplacedItems = "pLPvts".ToCharArray(); 
        new Rucksack().GetMisplacedItems(rucksackLines).Should().ContainInOrder(expectedMisplacedItems);
    }
    
    [Theory]
    [InlineData('a', 1)]
    [InlineData('z', 26)]
    [InlineData('A', 27)]
    [InlineData('Z', 52)]
    public void Get_Item_Type_Priorities(char itemType, int expectedPriority)
    {
        new Rucksack().GetItemTypePriority(itemType).Should().Be(expectedPriority);
    }
    
    [Fact]
    public void Get_Total_Priorities_Of_Misplaced_Items_In_Sample_Input()
    {
        var rucksackLines = File.ReadAllLines(@"TestData\SampleInput.txt"); 
        new Rucksack().GetTotalPrioritiesOfMisplacedItems(rucksackLines).Should().Be(157);
    }
    
    [Fact]
    public void Get_Total_Priorities_Of_Misplaced_Items_In_My_Input()
    {
        var rucksackLines = File.ReadAllLines(@"TestData\MyInput.txt"); 
        new Rucksack().GetTotalPrioritiesOfMisplacedItems(rucksackLines).Should().Be(8085);
    }
}