using System.Text;

namespace FileSystem;

public class Terminal
{
    private const int FileSystemCapacity = 70_000_000;
    private const string RootDirectoryName = "/";
    
    public TreeNode<Item> CurrentDirectory { get; private set; }
    public TreeNode<Item> FileSystem { get; private set; }

    public Terminal()
    {
        var rootItem = new Item(ItemType.Directory, RootDirectoryName, 0);
        FileSystem = new TreeNode<Item>(rootItem);
        CurrentDirectory = FileSystem;
    }

    public void ParseInput(string[] inputLines)
    {
        foreach (var inputLine in inputLines)
        {
            if (inputLine[0] == '$')
            {
                var command = ParseCommandInputLine(inputLine);
                if (command.HasValue)
                {
                    ProcessChangeDirectoryCommand(command.Value);   
                }
            }
            else
            {
               var item = ParseListCommandOutputLine(inputLine);
               if (item != null)
               {
                   CurrentDirectory.AddChild(item);
                   
                   // update directory sizes
                   var current = CurrentDirectory;
                   do
                   {
                       current.Value.IncreaseSize(item.Size);
                       current = current.Parent;
                   } while (current != null);
               }
            }
        }
    }

    public string PrintFileSystem()
    {
        StringBuilder output = new();
        
        FileSystem.Traverse(
            action: (item, level) => output.AppendLine(item.ToString(level)), 
            level: 0);
        
        return output.ToString().TrimEnd();
    }

    public Command? ParseCommandInputLine(string inputLine)
    {
        var parts = inputLine.Split(' ');
        var prompt = parts[0];
        var cmd = parts[1];
        var arg = parts.Length > 2 ? parts[2] : null;
        
        if (prompt != "$")
        {
            return null;
        }

        Command? command = cmd switch
        {
            "cd" when arg == ".." => new Command(CommandType.ChangeToParentDirectory, null),
            "cd" when arg == RootDirectoryName => new Command(CommandType.ChangeToRootDirectory, null),
            "cd" => new Command(CommandType.ChangeDirectory, arg),
            "ls" => new Command(CommandType.List, null),
            _ => null
        };

        return command;
    }

    public Item? ParseListCommandOutputLine(string inputLine)
    {
        var parts = inputLine.Split(' ');
        
        if (parts[0] == "$")
        {
            return null;
        }

        Item? item = null;
        if (parts[0] == "dir")
        {
            item = new Item(ItemType.Directory, parts[1], 0);
        }
        else if (int.TryParse(parts[0], out var size))
        {
            item = new Item(ItemType.File, parts[1], size);
        }

        return item;
    }

    public void ProcessChangeDirectoryCommand(Command command)
    {
        switch (command.Type)
        {
            case CommandType.ChangeToRootDirectory:
                CurrentDirectory = FileSystem;
                break;
            case CommandType.ChangeToParentDirectory:
                if (CurrentDirectory.Parent != null)
                {
                    CurrentDirectory = CurrentDirectory.Parent;
                }
                break;
            case CommandType.ChangeDirectory:
                var destination = CurrentDirectory.Children
                    .FirstOrDefault(x => x.Value.Name == command.Argument);
                if (destination != null)
                {
                    CurrentDirectory = destination;
                }
                break;
            default:
                // do nothing for any other commands
                break;
        }
    }

    public int GetTotalDirectorySizesWithMaxSizeOf(int size)
    {
        return FileSystem.Flatten()
            .Where(x => 
                x.Name != RootDirectoryName &&
                x.Type == ItemType.Directory && 
                x.Size <= size)
            .Sum(x => x.Size);
    }

    public int GetTotalDirectorySize()
    {
        return FileSystem.Value.Size;
    }
    
    public int GetFreeSpaceSize()
    {
        return FileSystemCapacity - FileSystem.Value.Size;
    }

    public Item GetSmallestDirectoryToDeleteToAchieveFreeSpaceOf(int desiredFreeSpace)
    {
        var requiredDirectorySize = desiredFreeSpace - GetFreeSpaceSize();
        
        return FileSystem.Flatten()
            .Where(x =>
                x.Type == ItemType.Directory &&
                x.Size >= requiredDirectorySize)
            .OrderBy(x => x.Size)
            .First();
    }
}