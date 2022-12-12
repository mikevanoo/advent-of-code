namespace Monkey;

public class WorriedMonkeys
{
    private readonly int _reliefFactor;
    public List<Monkey> Monkeys { get; private set; } = new();

    public WorriedMonkeys(int reliefFactor)
    {
        _reliefFactor = reliefFactor;
    }
    
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

    public long GetMonkeyBusinessLevel()
    {
        var inspectionCounts = Monkeys
            .Select(x => x.InspectedItemsCount)
            .OrderDescending()
            .ToList();

        return (long)inspectionCounts[0] * (long)inspectionCounts[1];
    }

    private void PlayTurn(Monkey monkey)
    {
        // using the product of all test divisors ensures that:
        // i) we keep the worry level numbers small
        // ii) when we transfer an item to another monkey then the worry level will continue to work with their test divisor
        var testFactor = Monkeys.Aggregate(1L, (f, m) => f * m.TestDivisor);
        
        for (var index = 0; index < monkey.Items.Count; index++)
        {
            var result = monkey.InspectItem(index, _reliefFactor, testFactor);
            Monkeys[result.ThrowToMonkeyIndex].Items.Add(result.Item);
        }

        monkey.Items.Clear();
    }
}