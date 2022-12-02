namespace RockPaperScissors;

public record Play
{
    public char Player1 { get; set; }
    public char Player2 { get; set; }
    
    public Play(char player1, char player2)
    {
        Player1 = player1;
        Player2 = player2;
    }
}