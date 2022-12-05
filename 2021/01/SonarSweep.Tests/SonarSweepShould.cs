using FluentAssertions;

namespace SonarSweep.Tests;

public class SonarSweepShould
{
    [Theory]
    [InlineData("SampleInput.txt", 7)]
    [InlineData("MyInput.txt", 1676)]
    public void Get_The_Number_Of_Depth_Measurements_That_Increased(
        string inputFile, 
        int expectedNumber)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new SonarSweep().GetNumberOfIncreasedDepthMeasurements(inputLines).Should().Be(expectedNumber);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 5)]
    [InlineData("MyInput.txt", 1706)]
    public void Get_The_Number_Of_Depth_Measurements_That_Increased_In_Sliding_Window(
        string inputFile, 
        int expectedNumber)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new SonarSweep().GetNumberOfIncreasedDepthMeasurementsInSlidingWindow(inputLines).Should().Be(expectedNumber);
    }
}