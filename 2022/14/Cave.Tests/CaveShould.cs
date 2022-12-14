using FluentAssertions;
using Xunit.Abstractions;

namespace Cave.Tests;

public class CaveShould
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CaveShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Create_Grid_From_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");

        var sut = new Cave();
        sut.CreateGrid(inputLines);

        // max Y or Y value + padding - 1 for zero-based indexes
        sut.Grid.GetUpperBound(0).Should().Be(503 + Cave.GridPaddingX - 1);
        sut.Grid.GetUpperBound(1).Should().Be(9 + Cave.GridPaddingY - 1);
    }
    
    [Fact]
    public void Draw_Rocks_From_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");

        var sut = new Cave();
        sut.CreateGrid(inputLines);

        var rockCount = 0;
        for (var x = 0; x <= sut.Grid.GetUpperBound(0); x++)
        {
            for (var y = 0; y <= sut.Grid.GetUpperBound(1); y++)
            {
                if (sut.Grid[x, y] == CellContents.Rock)
                {
                    rockCount++;
                }
            }
        }
        rockCount.Should().Be(20);

        sut.IsRock(498, 4).Should().BeTrue();
        sut.IsRock(498, 5).Should().BeTrue();
        sut.IsRock(498, 6).Should().BeTrue();
        sut.IsRock(497, 6).Should().BeTrue();
        sut.IsRock(496, 6).Should().BeTrue();
        sut.IsRock(503, 4).Should().BeTrue();
        sut.IsRock(502, 4).Should().BeTrue();
        sut.IsRock(502, 4).Should().BeTrue();
        sut.IsRock(502, 5).Should().BeTrue();
        sut.IsRock(502, 6).Should().BeTrue();
        sut.IsRock(502, 7).Should().BeTrue();
        sut.IsRock(502, 8).Should().BeTrue();
        sut.IsRock(501, 9).Should().BeTrue();
        sut.IsRock(500, 9).Should().BeTrue();
        sut.IsRock(499, 9).Should().BeTrue();
        sut.IsRock(498, 9).Should().BeTrue();
        sut.IsRock(497, 9).Should().BeTrue();
        sut.IsRock(496, 9).Should().BeTrue();
        sut.IsRock(495, 9).Should().BeTrue();
        sut.IsRock(494, 9).Should().BeTrue();
    }

    [Theory]
    [InlineData(1, 500, 8)]
    [InlineData(2, 499, 8)]
    [InlineData(3, 501, 8)]
    [InlineData(4, 500, 7)]
    [InlineData(5, 498, 8)]
    [InlineData(22, 500, 2)]
    [InlineData(23, 497, 5)]
    [InlineData(24, 495, 8)]
    public void Drop_Sand(
        int numberOfSandUnits,
        int expectedSandX,
        int expectedSandY)
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");

        var sut = new Cave();
        sut.CreateGrid(inputLines);
        
        sut.DropSand(numberOfSandUnits).Should().Be(numberOfSandUnits);
        sut.IsSand(expectedSandX, expectedSandY).Should().BeTrue();
    }

    [Theory]
    [InlineData("SampleInput.txt", 24)]
    [InlineData("MyInput.txt", 825)]
    public void Drop_Sand_Continuously(
        string inputFile,
        int expectedSandCount)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");

        var sut = new Cave();
        sut.CreateGrid(inputLines);
        
        sut.DropSand().Should().Be(expectedSandCount);
    }

    [Fact]
    public void Draw_Rocks_With_Floor_From_Sample_Input()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");

        var sut = new Cave();
        sut.CreateGrid(inputLines, true);

        var rockCount = 0;
        for (var x = 0; x <= sut.Grid.GetUpperBound(0); x++)
        {
            for (var y = 0; y <= sut.Grid.GetUpperBound(1); y++)
            {
                if (sut.Grid[x, y] == CellContents.Rock)
                {
                    rockCount++;
                }
            }
        }
        
        // 20 + the size of the floor
        rockCount.Should().Be(20 + 503 + Cave.GridPaddingX);

        sut.IsRock(498, 4).Should().BeTrue();
        sut.IsRock(498, 5).Should().BeTrue();
        sut.IsRock(498, 6).Should().BeTrue();
        sut.IsRock(497, 6).Should().BeTrue();
        sut.IsRock(496, 6).Should().BeTrue();
        sut.IsRock(503, 4).Should().BeTrue();
        sut.IsRock(502, 4).Should().BeTrue();
        sut.IsRock(502, 4).Should().BeTrue();
        sut.IsRock(502, 5).Should().BeTrue();
        sut.IsRock(502, 6).Should().BeTrue();
        sut.IsRock(502, 7).Should().BeTrue();
        sut.IsRock(502, 8).Should().BeTrue();
        sut.IsRock(501, 9).Should().BeTrue();
        sut.IsRock(500, 9).Should().BeTrue();
        sut.IsRock(499, 9).Should().BeTrue();
        sut.IsRock(498, 9).Should().BeTrue();
        sut.IsRock(497, 9).Should().BeTrue();
        sut.IsRock(496, 9).Should().BeTrue();
        sut.IsRock(495, 9).Should().BeTrue();
        sut.IsRock(494, 9).Should().BeTrue();

        for (var x = 0; x <= sut.Grid.GetUpperBound(0); x++)
        {
            sut.IsRock(x, sut.Grid.GetUpperBound(1)).Should().BeTrue();
        }
    }

    [Theory]
    [InlineData("SampleInput.txt", 93)]
    [InlineData("MyInput.txt", 26729)]
    public void Drop_Sand_Continuously_With_Floor(
        string inputFile,
        int expectedSandCount)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
    
        var sut = new Cave();
        sut.CreateGrid(inputLines, true);
        
        sut.DropSand().Should().Be(expectedSandCount);
    }
}