namespace CrateMover;

public class CrateStack
{
    public int Number { get; set; }
    public Stack<char> Crates { get; set; } = new();
}