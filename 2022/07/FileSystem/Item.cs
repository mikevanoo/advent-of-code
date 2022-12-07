namespace FileSystem;

public record Item(ItemType Type, string Name, int Size)
{
    public ItemType Type { get; private set; } = Type;
    public string Name { get; private set; } = Name;
    public int Size { get; private set; } = Size;

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

    public void IncreaseSize(int size)
    {
        Size += size;
    }
}