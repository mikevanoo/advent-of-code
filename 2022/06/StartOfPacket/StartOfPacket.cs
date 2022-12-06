namespace StartOfPacket;

public class StartOfPacket
{
    public int CharactersReadAtEndOfStartOfPacketMarker(string inputLine)
    {
        const int markerLength = 4;
        return CharactersReadAtEndOfMarker(inputLine, markerLength);
    }

    public int CharactersReadAtEndOfStartOfMessageMarker(string inputLine)
    {
        const int markerLength = 14;
        return CharactersReadAtEndOfMarker(inputLine, markerLength);
    }

    private int CharactersReadAtEndOfMarker(string inputLine, int markerLength)
    {
        for (var index = 0; index <= inputLine.Length - markerLength; index++)
        {
            if (SampleContainsDistinctCharacters(inputLine, index, markerLength))
            {
                return index + markerLength;
            }
        }

        return -1;
    }

    private bool SampleContainsDistinctCharacters(
        string inputLine,
        int startIndex,
        int length)
    {
        var sample = inputLine[startIndex..(startIndex + length)];
        if (sample.Length == length)
        {
            if (sample.Distinct().Count() == length)
            {
                return true;
            }
        }

        return false;
    }
}