namespace CalorieCounter;

public class CalorieCounter
{
    public int GetMaxCalorieCount(IEnumerable<string> list)
    {
        var groupedCalories = GetGroupedCalorieCounts(list);
        return groupedCalories.Values.Max();
    }

    public int GetSumOfTop3CalorieCounts(IEnumerable<string> list)
    {
        var groupedCalories = GetGroupedCalorieCounts(list);
        var orderedCalorieCounts = groupedCalories.Values.OrderDescending();
        return orderedCalorieCounts.Take(3).Sum();
    }

    private static Dictionary<int, int> GetGroupedCalorieCounts(IEnumerable<string> list)
    {
        int groupCounter = 0;
        int groupTotal = 0;
        Dictionary<int, int> groups = new();

        void AddGroup()
        {
            groups.Add(groupCounter, groupTotal);
            groupCounter++;
            groupTotal = 0;
        }

        foreach (var line in list)
        {
            if (int.TryParse(line, out int value))
            {
                groupTotal += value;
            }
            else
            {
                AddGroup();
            }
        }
        
        // add the last group
        AddGroup();

        return groups;
    }
}