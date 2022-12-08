using FluentAssertions;

namespace TreeHouse.Tests;

public class TreeHouseShould
{
    [Fact]
    public void Determine_Tree_Visibility_Of_The_SampleInput()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        
        var sut = new TreeHouse();
        sut.ParseInput(inputLines);
        
        // edge rows/columns are always visible
        sut.Map[0].Should().AllSatisfy(tree => tree.Visible.Should().BeTrue(), "first row is all on the edge");
        sut.Map[^1].Should().AllSatisfy(tree => tree.Visible.Should().BeTrue(), "last row is all on the edge");
        for (var rowIndex = 0; rowIndex < sut.Map.Length; rowIndex++)
        {
            sut.Map[rowIndex][0].Visible.Should().BeTrue("first column is all on the edge");   
            sut.Map[rowIndex][^1].Visible.Should().BeTrue("last column is all on the edge");
        }

        // top-left 5
        sut.Map[1][1].Visible.Should().BeTrue();
        // top-middle 5
        sut.Map[1][2].Visible.Should().BeTrue();
        // top-right 1
        sut.Map[1][3].Visible.Should().BeFalse();
        
        // middle-left 5
        sut.Map[2][1].Visible.Should().BeTrue();
        // middle-middle 3
        sut.Map[2][2].Visible.Should().BeFalse();
        // middle-right 3
        sut.Map[2][3].Visible.Should().BeTrue();
        
        // bottom-left 3
        sut.Map[3][1].Visible.Should().BeFalse();
        // bottom-middle 5
        sut.Map[3][2].Visible.Should().BeTrue();
        // bottom-right 4
        sut.Map[3][3].Visible.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 21)]
    [InlineData("MyInput.txt", 1796)]
    public void Get_Number_Of_Visible_Trees(
        string inputFile,
        int expectedCount)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        
        var sut = new TreeHouse();
        sut.ParseInput(inputLines);

        sut.GetVisibleTreeCount().Should().Be(expectedCount);
    }
    
    [Fact]
    public void Determine_Tree_Scenic_Score_Of_The_SampleInput()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        
        var sut = new TreeHouse();
        sut.ParseInput(inputLines);
        
        // top-middle 5
        sut.Map[1][2].ScenicScore.Should().Be(4);
        
        // bottom-middle 5
        sut.Map[3][2].ScenicScore.Should().Be(8);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 8)]
    [InlineData("MyInput.txt", 288120)]
    public void Get_Highest_Scenic_Score(
        string inputFile,
        int expectedScore)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        
        var sut = new TreeHouse();
        sut.ParseInput(inputLines);

        sut.GetHighestScenicScore().Should().Be(expectedScore);
    }
}