namespace RockPaperScissors;

public class Game
{
    public int GetPlayer2TotalScore(IEnumerable<string> playLines)
    {
        var parsedPlays = ParsePlays(playLines);
        var scoredPlays = ScorePlays(parsedPlays);
        
        return scoredPlays.Sum(x => x.Player2);
    }

    private IEnumerable<Play> ParsePlays(IEnumerable<string> playLines)
    {
        return playLines
            .Select(playLine => playLine.Split(" "))
            .Select(plays => new Play(Convert.ToChar(plays[0]), Convert.ToChar(plays[1])))
            .ToList();
    }

    private IEnumerable<Score> ScorePlays(IEnumerable<Play> plays)
    {
        List<Score> scores = new();
        
        foreach (var play in plays)
        {
            var player1Score = 0;
            var player2Score = 0;

            player1Score = play.Player1 switch
            {
                'A' => 1,
                'B' => 2,
                'C' => 3,
                _ => throw new ArgumentException($"Unknown play '{play.Player1}' for player 1.")
            };

            player2Score = play.Player2 switch
            {
                'X' => 1,
                'Y' => 2,
                'Z' => 3,
                _ => throw new ArgumentException($"Unknown play '{play.Player2}' for player 2.")
            };

            if (player1Score > player2Score)
            {
                player1Score += 6;
            }
            else if (player2Score > player1Score)
            {
                player2Score += 6;
            }
            else
            {
                player1Score += 3;
                player2Score += 3;
            }
            
            scores.Add(new Score(player1Score, player2Score));
        }
        
        return scores;
    }
    
    
}