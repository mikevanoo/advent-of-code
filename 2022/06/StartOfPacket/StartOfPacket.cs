namespace StartOfPacket;

public class StartOfPacket
{
    public int CharactersReadAtEndOfStartOfPacketMarker(string inputLine)
    {
        const int markerLength = 4;
        
        for (var index = 0; index <= inputLine.Length - markerLength; index++)
        {
            var sample = inputLine[index..(index + markerLength)];
            if (sample.Length == markerLength)
            {
                if (sample.Distinct().Count() == markerLength)
                {
                    return index + markerLength;
                }
            }
        }
        
        return -1;
    }
}