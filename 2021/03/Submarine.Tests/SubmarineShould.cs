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
    [InlineData("MyInput.txt", 3309596)]
    public void Get_PowerConsumption(
        string inputFile, 
        int expectedPowerConsumption)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetPowerConsumption(inputLines).Should().Be(expectedPowerConsumption);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 23)]
    public void Get_The_Oxygen_Generator_Rating(
        string inputFile, 
        int expectedRate)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetOxygenGeneratorRating(inputLines).Should().Be(expectedRate);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 10)]
    public void Get_The_Co2_Scrubber_Rating(
        string inputFile, 
        int expectedRate)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetCo2ScrubberRating(inputLines).Should().Be(expectedRate);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 230)]
    [InlineData("MyInput.txt", 2981085)]
    public void Get_Life_Support_Rating(
        string inputFile, 
        int expectedRate)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new Submarine().GetLifeSupportRating(inputLines).Should().Be(expectedRate);
    }
}