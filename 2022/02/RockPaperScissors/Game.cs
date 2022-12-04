namespace RockPaperScissors;

public class Game
{
    public int GetPlayer2TotalScoreForPart1(IEnumerable<string> playLines)
    {
        var parsedPlays = ParsePlays(playLines);
        return TotalScoreForPlayer2(parsedPlays);
    }

    public int GetPlayer2TotalScoreForPart2(IEnumerable<string> playLines)
    {
        var parsedPlays = ParsePlays(playLines).ToList();

        // adjust what player 2 plays such that they lose/draw/win using the following rules:
        // X means player 2 loses
        // Y means a draw
        // Z means player 2 wins
        foreach (var play in parsedPlays)
        {
            play.Player2 = play.Player1 switch
            {
                'A' when play.Player2 == 'X' => 'Z',
                'A' when play.Player2 == 'Y' => 'X',
                'A' when play.Player2 == 'Z' => 'Y',
                'B' when play.Player2 == 'X' => 'X',
                'B' when play.Player2 == 'Y' => 'Y',
                'B' when play.Player2 == 'Z' => 'Z',
                'C' when play.Player2 == 'X' => 'Y',
                'C' when play.Player2 == 'Y' => 'Z',
                'C' when play.Player2 == 'Z' => 'X',
                _ => throw new InvalidOperationException($"Invalid plays.")
            };
        }
        
        return TotalScoreForPlayer2(parsedPlays);
    }

    private int TotalScoreForPlayer2(IEnumerable<Play> parsedPlays)
    {
        return parsedPlays.Sum(play => play.Player1 switch
        {
            'A' when play.Player2 == 'X' => 4,
            'A' when play.Player2 == 'Y' => 8,
            'A' when play.Player2 == 'Z' => 3,
            'B' when play.Player2 == 'X' => 1,
            'B' when play.Player2 == 'Y' => 5,
            'B' when play.Player2 == 'Z' => 9,
            'C' when play.Player2 == 'X' => 7,
            'C' when play.Player2 == 'Y' => 2,
            'C' when play.Player2 == 'Z' => 6,
            _ => throw new InvalidOperationException($"Invalid plays.")
        });
    }

    private IEnumerable<Play> ParsePlays(IEnumerable<string> playLines)
    {
        return playLines
            .Select(playLine => playLine.Split(" "))
            .Select(plays => new Play(Convert.ToChar(plays[0]), Convert.ToChar(plays[1])))
            .ToList();
    }
}