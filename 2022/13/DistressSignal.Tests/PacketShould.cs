using FluentAssertions;

namespace DistressSignal.Tests;

public class PacketShould
{
    [Theory]
    [InlineData("[1,1,3,1,1]")]
    [InlineData("[1,1,5,1,1]")]
    [InlineData("[[1],[2,3,4]]")]
    public void Parse_Sample_Input(string packetString)
    {
        var sut = new Packet();
        sut.ParseInput(packetString);
        sut.ToString().Should().Be(packetString);
    }
    
    [Theory]
    [InlineData("SampleInput.txt")]
    [InlineData("MyInput.txt")]
    public void Parse_Input_Without_Error(string inputFile)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");

        foreach (var inputLine in inputLines.Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            var sut = new Packet();
            sut.ParseInput(inputLine);
            sut.ToString().Should().Be(inputLine);   
        }
    }
    
}