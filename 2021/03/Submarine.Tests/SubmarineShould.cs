using FluentAssertions;

namespace Submarine.Tests;

public class SubmarineShould
{
    [Theory]
    [InlineData("SampleInput.txt", 22)]
    public void Get_The_Gamma_Rate(
        string inputFile, 
        int expectedRate)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetGammaRate(inputLines).Should().Be(expectedRate);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 9)]
    public void Get_The_Epsilon_Rate(
        string inputFile, 
        int expectedRate)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetEpsilonRate(inputLines).Should().Be(expectedRate);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 198)]
    public void Get_PowerConsumption(
        string inputFile, 
        int expectedPowerConsumption)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetPowerConsumption(inputLines).Should().Be(expectedPowerConsumption);
    }
}