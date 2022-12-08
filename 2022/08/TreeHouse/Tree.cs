namespace TreeHouse;

public record Tree(int Height, bool Visible)
{
    public bool Visible { get; set; } = Visible;
    public int ScenicScore { get; set; } = 0;
}