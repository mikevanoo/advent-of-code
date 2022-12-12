using System.Text.RegularExpressions;

namespace Monkey;

public partial class Monkey
{
    [GeneratedRegex("\\d+")]
    private static partial Regex GetNumbersRegex();
    
    public List<int> Items { get; } = new();
    public string OperationLeftOperand { get; private set; } = string.Empty;
    public string OperationOperator { get; private set; } = string.Empty;
    public string OperationRightOperand { get; private set; } = string.Empty;
    public int TestDivisor { get; private set; }
    public int TrueMonkey { get; private set; }
    public int FalseMonkey { get; private set; }
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
                    .Select(match => Convert.ToInt32(match.Value))
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
                TrueMonkey = Convert.ToInt32(GetNumbersRegex().Matches(inputLine)[0].Value);
            }
            
            // test (false)
            if (inputLine.Contains("If false:"))
            {
                FalseMonkey = Convert.ToInt32(GetNumbersRegex().Matches(inputLine)[0].Value);
            }
        }
    }
    
    public (int ThrowToMonkeyIndex, int Item) InspectItem(int index)
    {
        InspectedItemsCount++;
        
        var worryLevel = Items[index];

        var leftOperand = OperationLeftOperand == "old" ? worryLevel : Convert.ToInt32(OperationLeftOperand);
        var rightOperand = OperationRightOperand == "old" ? worryLevel : Convert.ToInt32(OperationRightOperand);
        worryLevel = OperationOperator == "*" ? leftOperand * rightOperand : leftOperand + rightOperand; 
        
        worryLevel = Math.DivRem(worryLevel, 3).Quotient;

        var throwToMonkeyIndex = Math.DivRem(worryLevel, TestDivisor).Remainder == 0
            ? TrueMonkey
            : FalseMonkey;

        return (throwToMonkeyIndex, worryLevel);
    }
}