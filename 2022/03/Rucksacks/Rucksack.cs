namespace Rucksacks;

public class Rucksack
{
    public int GetTotalPrioritiesOfMisplacedItems(string[] rucksackLines)
    {
        var misplacedItems = GetMisplacedItems(rucksackLines);
        return misplacedItems.Sum(GetItemTypePriority);
    }
    
    public char[] GetMisplacedItems(string[] rucksackLines)
    {
        List<char> misplacedItems = new();

        foreach (var rucksackLine in rucksackLines)
        {
            var compartments = rucksackLine.Chunk(rucksackLine.Length / 2).ToList();
            var misplaced = compartments[0].Intersect(compartments[1]).ToList();
            misplacedItems.AddRange(misplaced);
        }
        
        return misplacedItems.ToArray();
    }

    public int GetItemTypePriority(char itemType)
    {
        int priority = itemType;
        return char.IsLower(itemType) ? priority - 96 : priority - 38;
    }
}