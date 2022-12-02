namespace RockPaperScissors;

public record Play(char Player1, char Player2)
{
    public char Player1 { get; set; } = Player1;
    public char Player2 { get; set; } = Player2;
}