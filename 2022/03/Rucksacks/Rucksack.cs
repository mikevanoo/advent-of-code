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

    public int GetTotalPrioritiesOfGroupBadges(string[] rucksackLines)
    {
        var groupBadges = GetCommonItemTypeInEachGroup(rucksackLines);
        return groupBadges.Sum(GetItemTypePriority);
    }

    public char[] GetCommonItemTypeInEachGroup(string[] rucksackLines)
    {
        var groups = rucksackLines.Chunk(3).ToList();

        List<char> commonItemTypes = new();
        
        foreach (var group in groups)
        {
            var inCommon = group[0].Intersect(group[1].Intersect(group[2])).ToList();
            commonItemTypes.AddRange(inCommon);
        }
        
        return commonItemTypes.ToArray();
    }
}