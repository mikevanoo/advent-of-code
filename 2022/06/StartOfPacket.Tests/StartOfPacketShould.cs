using FluentAssertions;

namespace StartOfPacket.Tests;

public class StartOfPacketShould
{
    [Theory]
    [InlineData("SampleInput1.txt", 7)]
    [InlineData("SampleInput2.txt", 5)]
    [InlineData("SampleInput3.txt", 6)]
    [InlineData("SampleInput4.txt", 10)]
    [InlineData("SampleInput5.txt", 11)]
    [InlineData("MyInput.txt", 1542)]
    public void Get_The_Number_Of_Characters_Read_To_Complete_The_Start_Of_Packet_Marker(
        string inputFile,
        int expectedNumberOfCharacters)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new StartOfPacket().CharactersReadAtEndOfStartOfPacketMarker(inputLines[0]).Should().Be(expectedNumberOfCharacters);
    }
    
    [Theory]
    [InlineData("SampleInput1.txt", 19)]
    [InlineData("SampleInput2.txt", 23)]
    [InlineData("SampleInput3.txt", 23)]
    [InlineData("SampleInput4.txt", 29)]
    [InlineData("SampleInput5.txt", 26)]
    [InlineData("MyInput.txt", 3153)]
    public void Get_The_Number_Of_Characters_Read_To_Complete_The_Start_Of_Message_Marker(
        string inputFile,
        int expectedNumberOfCharacters)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        new StartOfPacket().CharactersReadAtEndOfStartOfMessageMarker(inputLines[0]).Should().Be(expectedNumberOfCharacters);
    }
}