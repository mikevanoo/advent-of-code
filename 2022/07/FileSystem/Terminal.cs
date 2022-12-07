using System.Text;

namespace FileSystem;

public class Terminal
{
    private Item _rootItem;
    
    public TreeNode<Item> CurrentDirectory { get; private set; }
    public TreeNode<Item> FileSystem { get; private set; }

    public Terminal()
    {
        _rootItem = new Item(ItemType.Directory, "/", 0);
        FileSystem = new TreeNode<Item>(_rootItem);
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
               if (item.HasValue)
               {
                   CurrentDirectory.AddChild(item.Value);
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
            "cd" when arg == "/" => new Command(CommandType.ChangeToRootDirectory, null),
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
}