using FluentAssertions;

namespace DistressSignal.Tests;

public class PacketPairShould
{
    [Theory]
    [InlineData("SampleInput.txt")]
    [InlineData("MyInput.txt")]
    public void Parse_Input_Without_Error(string inputFile)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        var chunkedLines = inputLines
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Chunk(2);

        foreach (var inputLineChunk in chunkedLines)
        {
            var sut = new PacketPair();
            sut.ParseInput(inputLineChunk[0], inputLineChunk[1]);
            sut.ToString().Should().Be(inputLineChunk[0] + Environment.NewLine + inputLineChunk[1]);   
        }
    }
    
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    public void Determine_If_Pair_Is_In_Correct_Order_In_Sample_Input(
        int pairNumber,
        bool expectedIsInCorrectOrder)
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        var chunkedLines = inputLines
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Chunk(2)
            .Skip(pairNumber - 1)
            .First();

        var sut = new PacketPair();
        sut.ParseInput(chunkedLines[0], chunkedLines[1]);
        sut.IsInCorrectOrder().Should().Be(expectedIsInCorrectOrder);
    }
    
}