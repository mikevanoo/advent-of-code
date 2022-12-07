using FluentAssertions;

namespace FileSystem.Tests;

public class ItemShould
{
    [Theory]
    [InlineData(ItemType.Directory, "/", 0, "- / (dir)")]
    [InlineData(ItemType.Directory, "/", 123, "- / (dir)")]
    [InlineData(ItemType.File, "h.lst", 123, "- h.lst (file, size=123)")]
    public void ToString_Itself_Correctly(
        ItemType type,
        string name,
        int size, 
        string expectedToString)
    {
        new Item(type, name, size).ToString().Should().Be(expectedToString);
    }
    
    [Theory]
    [InlineData(1, ItemType.Directory, "a", 0, "  - a (dir)")]
    [InlineData(2, ItemType.Directory, "e", 0, "    - e (dir)")]
    [InlineData(3, ItemType.File, "h.lst", 123, "      - h.lst (file, size=123)")]
    public void ToString_Itself_With_Indent_Correctly(
        int identLevel,
        ItemType type,
        string name,
        int size, 
        string expectedToString)
    {
        new Item(type, name, size).ToString(identLevel).Should().Be(expectedToString);
    }
}