namespace RockPaperScissors;

public class Game
{
    public int GetPlayer2TotalScore(IEnumerable<string> playLines)
    {
        var parsedPlays = ParsePlays(playLines);

        var player2Score = 0;
        foreach (var play in parsedPlays)
        {
            switch (play.Player1)
            {
                case 'A' when play.Player2 == 'X':
                    player2Score += 4;
                    break;
                case 'A' when play.Player2 == 'Y':
                    player2Score += 8;
                    break;
                case 'A' when play.Player2 == 'Z':
                    player2Score += 3;
                    break;
                
                case 'B' when play.Player2 == 'X':
                    player2Score += 1;
                    break;
                case 'B' when play.Player2 == 'Y':
                    player2Score += 5;
                    break;
                case 'B' when play.Player2 == 'Z':
                    player2Score += 9;
                    break;
                
                case 'C' when play.Player2 == 'X':
                    player2Score += 7;
                    break;
                case 'C' when play.Player2 == 'Y':
                    player2Score += 2;
                    break;
                case 'C' when play.Player2 == 'Z':
                    player2Score += 6;
                    break;
            }
        }

        return player2Score;
    }

    private IEnumerable<Play> ParsePlays(IEnumerable<string> playLines)
    {
        return playLines
            .Select(playLine => playLine.Split(" "))
            .Select(plays => new Play(Convert.ToChar(plays[0]), Convert.ToChar(plays[1])))
            .ToList();
    }
}