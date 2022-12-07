namespace FileSystem;

public record struct Command(CommandType Type, string? Argument);