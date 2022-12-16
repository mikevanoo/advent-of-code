namespace Bingo;

public class Bingo
{
    public List<Board> Boards { get; private set; } = new();
    public int WinningBoardIndex { get; private set; } = -1;
    public int SumOfUnmarkedNumbersOnWinningBoard { get; private set; } = -1;
    public int LastNumberCalled { get; private set; } = -1;
    public int WinningScore => SumOfUnmarkedNumbersOnWinningBoard * LastNumberCalled;

    private int[] _calledNumbers = new int[1];


    public void ParseInput(string[] inputLines)
    {
        _calledNumbers = inputLines[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();

        // skip the called number line, then get chunks of 6
        var boardInputs = inputLines.Skip(1).Chunk(6);
        
        foreach (var boardInput in boardInputs)
        {
            var board = new Board();
            // the chunks of 6 lines start with an empty line, so we skip that
            board.ParseInput(boardInput.Skip(1).ToArray());
            Boards.Add(board);    
        }
    }

    public int Play()
    {
        for (var calledNumberIndex = 1; calledNumberIndex < _calledNumbers.Length; calledNumberIndex++)
        {
            var calledNumbers = _calledNumbers[..calledNumberIndex];
            LastNumberCalled = calledNumbers[^1];
            for (var boardIndex = 0; boardIndex < Boards.Count; boardIndex++)
            {
                if (Boards[boardIndex].HasWon(calledNumbers))
                {
                    WinningBoardIndex = boardIndex;
                    SumOfUnmarkedNumbersOnWinningBoard = Boards[boardIndex].SumOfUnmarkedNumbers;
                    return WinningBoardIndex;
                }
            }
        }

        return -1;
    }
}