using FluentAssertions;

namespace FileSystem.Tests;

public class TerminalShould
{
    [Theory]
    [InlineData("cd x")]
    [InlineData("1234 bob.txt")]
    [InlineData("dir e")]
    public void Parse_Command_Input_With_A_Non_Command_Returning_Null(string inputLine)
    {
        new Terminal().ParseCommandInputLine(inputLine).Should().BeNull();
    }

    [Theory]
    [InlineData("$ cd x", CommandType.ChangeDirectory, "x")]
    [InlineData("$ cd ..", CommandType.ChangeToParentDirectory, null)]
    [InlineData("$ cd /", CommandType.ChangeToRootDirectory, null)]
    [InlineData("$ ls", CommandType.List, null)]
    public void Parse_Command_Input_Correctly(
        string inputLine, 
        CommandType expectedCommand, 
        string? expectedDirectoryName)
    {
        var actual = new Terminal().ParseCommandInputLine(inputLine);
        actual.Should().NotBeNull();
        actual!.Value.Type.Should().Be(expectedCommand);
        actual!.Value.Argument.Should().Be(expectedDirectoryName);
    }
    
    [Theory]
    [InlineData("$ cd x")]
    [InlineData("$ ls")]
    [InlineData("$ cd /")]
    public void Parse_List_Command_Output_With_A_Non_Command_Returning_Null(string inputLine)
    {
        new Terminal().ParseListCommandOutputLine(inputLine).Should().BeNull();
    }
    
    [Theory]
    [InlineData("dir a", ItemType.Directory, "a", 0)]
    [InlineData("14848514 b.txt", ItemType.File, "b.txt", 14848514)]
    public void Parse_List_Command_Output_Correctly(
        string inputLine, 
        ItemType expectedType, 
        string expectedName,
        int expectedSize)
    {
        var actual = new Terminal().ParseListCommandOutputLine(inputLine);
        actual.Should().NotBeNull();
        actual!.Type.Should().Be(expectedType);
        actual!.Name.Should().Be(expectedName);
        actual!.Size.Should().Be(expectedSize);
    }
    
    [Theory]
    [InlineData("$ cd /", "/")]
    [InlineData("$ cd a", "a")]
    [InlineData("$ cd ..", "/")]
    [InlineData("$ cd b", "b")]
    [InlineData("$ cd xxx", "/")]
    [InlineData("$ ls", "/")]
    public void Process_Change_Directory_Commands_Correctly(
        string inputLine, 
        string? expectedDirectoryName)
    {
        var sut = new Terminal();
        sut.CurrentDirectory.AddChildren(
            new Item(ItemType.Directory, "a", 0),
            new Item(ItemType.Directory, "b", 0),
            new Item(ItemType.Directory, "c", 0));
        var command = sut.ParseCommandInputLine(inputLine);

        sut.ProcessChangeDirectoryCommand(command!.Value);
        sut.CurrentDirectory.Value.Name.Should().Be(expectedDirectoryName);
    }
    
    [Fact]
    public void Build_FileSystem_From_SampleInput()
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        
        var sut = new Terminal();
        sut.ParseInput(inputLines);

        var expectedOutput = File.ReadAllText(@"TestData\SampleInput_FileSystemOutput.txt");
        var actual = sut.PrintFileSystem();
        actual.Should().BeEquivalentTo(expectedOutput);
    }
    
    [Theory]
    [InlineData("e", 584)]
    [InlineData("a", 94853)]
    [InlineData("d", 24933642)]
    [InlineData("/", 48381165)]
    public void Get_Directory_Sizes_From_SampleInput(string name, int expectedSize)
    {
        var inputLines = File.ReadAllLines(@"TestData\SampleInput.txt");
        
        var sut = new Terminal();
        sut.ParseInput(inputLines);

        var allItems = sut.FileSystem.Flatten();
        allItems.First(x => x.Type == ItemType.Directory && x.Name == name)
            .Size.Should().Be(expectedSize);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 95437)]
    [InlineData("MyInput.txt", 1334506)]
    public void Get_Total_Directory_Sizes_With_Max_Size_Of_100_000(string inputFile, int expectedSize)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        
        var sut = new Terminal();
        sut.ParseInput(inputLines);

        sut.GetTotalDirectorySizesWithMaxSizeOf(100_000).Should().Be(expectedSize);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 48_381_165)]
    [InlineData("MyInput.txt", 46_975_962)]
    public void Get_Total_Directory_Size(string inputFile, int expectedSize)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        
        var sut = new Terminal();
        sut.ParseInput(inputLines);

        sut.GetTotalDirectorySize().Should().Be(expectedSize);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", 21_618_835)]
    [InlineData("MyInput.txt", 23_024_038)]
    public void Get_Free_Space_Size(string inputFile, int expectedSize)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        
        var sut = new Terminal();
        sut.ParseInput(inputLines);

        sut.GetFreeSpaceSize().Should().Be(expectedSize);
    }
    
    [Theory]
    [InlineData("SampleInput.txt", "d", 24_933_642)]
    [InlineData("MyInput.txt", "ptzptl", 7_421_137)]
    public void Get_The_Smallest_Directory_To_Delete_To_Achieve_Desired_Free_Space(
        string inputFile,
        string expectedDirectoryName,
        int expectedDirectorySize)
    {
        var inputLines = File.ReadAllLines($@"TestData\{inputFile}");
        
        var sut = new Terminal();
        sut.ParseInput(inputLines);

        var actual = sut.GetSmallestDirectoryToDeleteToAchieveFreeSpaceOf(30_000_000);
        actual.Name.Should().Be(expectedDirectoryName);
        actual.Size.Should().Be(expectedDirectorySize);
    }
}