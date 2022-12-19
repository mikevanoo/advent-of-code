using AoC.Common;

namespace LanternFish;

public class LanternFish
{
    private const int AgeBucketsSize = 9;   // the number of age-buckets to hold the counts of fish at that age
    private const int ResetLifeAge = 6;     // the age expired fish are reset to 
    
    public List<long> Fish { get; private set; } = new();
    
    public void ParseInput(string inputLine)
    {
        Fish = inputLine.ExtractIntegers()
            .GroupBy(age => age)
            .Aggregate(
                new List<long>(new long[AgeBucketsSize]), 
                (list, kv) =>
                {
                    list[kv.Key] = kv.Count();
                    return list;
                });
    }

    public long Reproduce(int reproduceForDays)
    {
        for (var day = 0; day < reproduceForDays; day++)
        {
            var expired = Fish.First();     // count of zero-aged fish
            Fish.RemoveAt(0);               // remove the bucket of expired fish
            Fish[ResetLifeAge] += expired;  // the count of expired fish now become aged "ResetLifeAge"
            Fish.Add(expired);              // the count of expired fish now get added to the top of the lifecycle
        }

        return Fish.Sum();
    }
}