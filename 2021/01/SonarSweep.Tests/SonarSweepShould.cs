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
}