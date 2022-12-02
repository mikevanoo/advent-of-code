namespace RockPaperScissors;

public class Game
{
    public int GetPlayer2TotalScoreForPart1(IEnumerable<string> playLines)
    {
        var parsedPlays = ParsePlays(playLines);

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