namespace AssignmentPairs;

public class AssignmentPairs
{
    public int GetNumberOfPairsWhereOneRangeFullyCoversTheOther(string[] inputLines)
    {
        var rangePairs = ParseLines(inputLines);
        return rangePairs
            .Count(rangePair => 
                (rangePair.Range1.Start >= rangePair.Range2.Start && rangePair.Range1.End <= rangePair.Range2.End) || 
                (rangePair.Range2.Start >= rangePair.Range1.Start && rangePair.Range2.End <= rangePair.Range1.End));
    }

    public Range ParseRange(string range)
    {
        var rangeLimits = range.Split('-');
        var start = Convert.ToInt32(rangeLimits[0]);
        var end = Convert.ToInt32(rangeLimits[1]);
        
        return new Range(start, end);
    }

    private IEnumerable<(Range Range1, Range Range2)> ParseLines(string[] inputLines)
    {
        return inputLines
            .Select(line => line.Split(','))
            .Select(ranges => new ValueTuple<Range, Range>(
                ParseRange(ranges[0]), 
                ParseRange(ranges[1]))
            )
            .ToList();
    }
}