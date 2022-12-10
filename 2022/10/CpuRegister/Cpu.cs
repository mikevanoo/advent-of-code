namespace CpuRegister;

public class Cpu
{
    private readonly List<Register> _cycles = new();

    public int RegisterX { get; private set; } = 1;
    public int CycleCount => _cycles.Count;

    public void Execute(string[] instructions)
    {
        foreach (var instruction in instructions)
        {
            Execute(instruction);
        }
    }
    
    public void Execute(string instruction)
    {
        var parsedInstruction = instruction.Split(' ');
        var operation = parsedInstruction[0];
        var value = parsedInstruction.Length > 1 ? Convert.ToInt32(parsedInstruction[1]) : 0;

        switch (operation)
        {
            case "noop":
                _cycles.Add(new Register(RegisterX, RegisterX));
                break;
            case "addx":
                _cycles.Add(new Register(RegisterX, RegisterX));
                
                var newRegisterX = RegisterX + value;
                _cycles.Add(new Register(RegisterX, newRegisterX));
                RegisterX = newRegisterX;
                
                break;
        }
    }

    public int GetSignalStrengthAtCycle(int cycleNumber)
    {
        return cycleNumber * _cycles[cycleNumber - 1].During;
    }

    public int GetTotalSignalStrengthAtCycles(params int[] cycleNumbers)
    {
        return cycleNumbers.Sum(GetSignalStrengthAtCycle);
    }
}