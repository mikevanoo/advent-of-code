namespace Monkey;

public class WorriedMonkeys
{
    public List<Monkey> Monkeys { get; private set; } = new();
    
    public void ParseInput(string[] inputLines)
    {
        var monkeyCount = (inputLines.Length + 1) / 7;

        for (var index = 0; index < monkeyCount; index++)
        {
            var monkey = new Monkey();
            monkey.ParseInput(inputLines.Skip(index * 7).Take(6).ToArray());
            Monkeys.Add(monkey);
        }
    }

    public void PlayRound()
    {
        foreach (var monkey in Monkeys)
        {
            PlayTurn(monkey);
        }
    }

    public int GetMonkeyBusinessLevel()
    {
        var inspectionCounts = Monkeys
            .Select(x => x.InspectedItemsCount)
            .OrderDescending()
            .ToList();

        return inspectionCounts[0] * inspectionCounts[1];
    }

    private void PlayTurn(Monkey monkey)
    {
        for (var index = 0; index < monkey.Items.Count; index++)
        {
            var result = monkey.InspectItem(index);
            Monkeys[result.ThrowToMonkeyIndex].Items.Add(result.Item);
        }

        monkey.Items.Clear();
    }
}