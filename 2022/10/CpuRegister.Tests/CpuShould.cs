using FluentAssertions;
using Xunit.Abstractions;

namespace CpuRegister.Tests;

public class CpuShould
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CpuShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("noop", 1)]
    [InlineData("addx 3", 2)]
    [InlineData("addx -5", 2)]
    public void Accumulate_Cycles_With_Operations(
        string instruction,
        int expectedCycleCount)
    {
        var sut = new Cpu();
        sut.Execute(instruction);
        sut.CycleCount.Should().Be(expectedCycleCount);
    }
    
    [Theory]
    [InlineData("noop", 1)]
    [InlineData("addx 3", 4)]
    [InlineData("addx -5", -4)]
    public void Records_The_Correct_RegisterX_Value(
        string instruction,
        int expectedRegisterValue)
    {
        var sut = new Cpu();
        sut.Execute(instruction);
        sut.RegisterX.Should().Be(expectedRegisterValue);
    }
    
    [Theory]
    [InlineData(20, 420)]
    [InlineData(60, 1140)]
    [InlineData(100, 1800)]
    [InlineData(140, 2940)]
    [InlineData(180, 2880)]
    [InlineData(220, 3960)]
    public void Get_Signal_Strength_At_A_Cycle_Number_In_Sample_Input(
        int cycleNumber,
        int expectedSignalStrength)
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        
        var sut = new Cpu();
        sut.Execute(inputLines);
        
        sut.GetSignalStrengthAtCycle(cycleNumber).Should().Be(expectedSignalStrength);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 13140)]
    [InlineData("MyInput.txt", 13820)]
    public void Get_Total_Signal_Strength_At_A_Cycle_Numbers(
        string inputFile,
        int expectedSignalStrength)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        
        var sut = new Cpu();
        sut.Execute(inputLines);
        
        sut.GetTotalSignalStrengthAtCycles(20, 60, 100, 140, 180, 220).Should().Be(expectedSignalStrength);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", "SampleInput_Output.txt")]
    [InlineData("MyInput.txt", "MyInput_Output.txt")]
    public void Print_Screen(
        string inputFile,
        string expectedOutputFile)
    {
        var inputLines = File.ReadAllLines(@$"TestData\{inputFile}");
        var expectedOutputLines = File.ReadAllLines(@$"TestData\{expectedOutputFile}");
        
        var sut = new Cpu();
        sut.Execute(inputLines);

        var actual = sut.PrintScreen();

        foreach (var line in actual)
        {
            _testOutputHelper.WriteLine(line);
        }
        actual.Should().BeEquivalentTo(expectedOutputLines);
        
        
    }
}