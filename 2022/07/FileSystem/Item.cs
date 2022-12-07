namespace FileSystem;

public record struct Item(ItemType Type, string Name, int Size)
{
    public override string ToString()
    {
        return ToString(0);
    }
    
    public string ToString(int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        return Type == ItemType.Directory 
            ? $"{indent}- {Name} (dir)" 
            : $"{indent}- {Name} (file, size={Size})";
    }
}