using System.Text.RegularExpressions;

namespace Monkey;

public partial class Monkey
{
    [GeneratedRegex("\\d+")]
    private static partial Regex GetNumbersRegex();
    
    public List<long> Items { get; } = new();
    public string OperationLeftOperand { get; private set; } = string.Empty;
    public string OperationOperator { get; private set; } = string.Empty;
    public string OperationRightOperand { get; private set; } = string.Empty;
    public int TestDivisor { get; private set; }
    public int TrueMonkeyIndex { get; private set; }
    public int FalseMonkeyIndex { get; private set; }
    public int InspectedItemsCount { get; private set; }

    public void ParseInput(string[] inputLines)
    {
        foreach (var inputLine in inputLines)
        {
            // starting items
            if (inputLine.Contains("Starting items:"))
            {
                Items.AddRange(
                    GetNumbersRegex()
                    .Matches(inputLine)
                    .Select(match => (long)Convert.ToDouble(match.Value))
                    .ToList());
            }
            
            // worry operation
            if (inputLine.Contains("Operation:"))
            {
                var calculation = inputLine.Split("= ")[1];
                var parsedCalculation = calculation.Split(' ');
                OperationLeftOperand = parsedCalculation[0];
                OperationOperator = parsedCalculation[1];
                OperationRightOperand = parsedCalculation[2];
            }
            
            // test
            if (inputLine.Contains("Test:"))
            {
                TestDivisor = Convert.ToInt32(GetNumbersRegex().Matches(inputLine)[0].Value);
            }
            
            // test (true)
            if (inputLine.Contains("If true:"))
            {
                TrueMonkeyIndex = Convert.ToInt32(GetNumbersRegex().Matches(inputLine)[0].Value);
            }
            
            // test (false)
            if (inputLine.Contains("If false:"))
            {
                FalseMonkeyIndex = Convert.ToInt32(GetNumbersRegex().Matches(inputLine)[0].Value);
            }
        }
    }
    
    public (int ThrowToMonkeyIndex, long Item) InspectItem(int index, int reliefFactor, long testFactor)
    {
        InspectedItemsCount++;
        
        var worryLevel = Items[index];

        var leftOperand = OperationLeftOperand == "old" ? worryLevel : Convert.ToInt32(OperationLeftOperand);
        var rightOperand = OperationRightOperand == "old" ? worryLevel : Convert.ToInt32(OperationRightOperand);
        worryLevel = OperationOperator == "*" ? leftOperand * rightOperand : leftOperand + rightOperand;

        if (reliefFactor > 0)
        {
            worryLevel = Math.DivRem(worryLevel, 3).Quotient;
        }
 
        // using the product of all test divisors ensures that:
        // i) we keep the worry level numbers small
        // ii) when we transfer an item to another monkey then the worry level will continue to work with their test divisor
        worryLevel %= testFactor;

        var throwToMonkeyIndex = worryLevel % TestDivisor == 0
            ? TrueMonkeyIndex
            : FalseMonkeyIndex;

        return (throwToMonkeyIndex, worryLevel);
    }
}