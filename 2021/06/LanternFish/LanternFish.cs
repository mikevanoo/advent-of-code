using AoC.Common;

namespace LanternFish;

public class LanternFish
{
    public List<int> Fish { get; private set; } = new();
    
    public void ParseInput(string inputLine)
    {
        Fish = inputLine.ExtractIntegers().ToList();
    }

    public void Reproduce(int reproduceForDays)
    {
        List<int> newFish = new();
        
        for (var day = 0; day < reproduceForDays; day++)
        {
            newFish.Clear();
            
            for (var fishIndex = 0; fishIndex < Fish.Count; fishIndex++)
            {
                var age = Fish[fishIndex] - 1;
                if (age < 0)
                {
                    age = 6;
                    newFish.Add(8);
                }

                Fish[fishIndex] = age;
            }
            
            Fish.AddRange(newFish);
        }
    }
}