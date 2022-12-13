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
    [InlineData(4, true)]
    [InlineData(5, false)]
    [InlineData(6, true)]
    [InlineData(7, false)]
    [InlineData(8, false)]
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
    
    [Theory]
    [InlineData("SampleInput.txt", 13)]
    [InlineData("MyInput.txt", 13)]
    public void Sum_Packet_Pair_Indexes_That_Are_In_The_Correct_Order(
        string inputFile,
        int expectedSum)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        var chunkedLines = inputLines
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Chunk(2)
            .ToList();

        var sum = 0;
        for (var index = 0; index < chunkedLines.Count(); index++)
        {
            var inputLineChunk = chunkedLines[index];
            
            var sut = new PacketPair();
            sut.ParseInput(inputLineChunk[0], inputLineChunk[1]);
            if (sut.IsInCorrectOrder())
            {
                sum += index + 1;
            }
        }

        sum.Should().Be(expectedSum);
    }
}