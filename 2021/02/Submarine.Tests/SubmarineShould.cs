using FluentAssertions;

namespace Submarine.Tests;

public class SubmarineShould
{
    [Theory]
    [InlineData("SampleInput.txt", 150)]
    [InlineData("MyInput.txt", 1882980)]
    public void Get_Horizontal_Position_Multiplied_By_Depth(
        string inputFile, 
        int expectedNumber)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetHorizontalPositionMultipliedByDepth(inputLines).Should().Be(expectedNumber);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 900)]
    [InlineData("MyInput.txt", 1971232560)]
    public void Get_Horizontal_Position_Multiplied_By_Depth_Using_Aim(
        string inputFile, 
        int expectedNumber)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetHorizontalPositionMultipliedByDepthUsingAim(inputLines).Should().Be(expectedNumber);
    }
}