using System.ComponentModel.Design;

namespace TreeHouse;

public class TreeHouse
{
    private int _rowSize = 1;
    private int _columnSize = 0;

    public Tree[][] Map { get; private set; } = new Tree[1][];
    
    public void ParseInput(string[] inputLines)
    {
        _rowSize = inputLines[0].Length;
        _columnSize = inputLines.Length;
        Map = new Tree[_rowSize][];

        for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
        {
            Map[rowIndex] = new Tree[_columnSize];
            
            for (var columnIndex = 0; columnIndex < _columnSize; columnIndex++)
            {
                var height = (int)char.GetNumericValue(inputLines[rowIndex][columnIndex]);
                Map[rowIndex][columnIndex] = new Tree(
                    Height: height,
                    Visible: false
                );
            }
        }

        SetTreeProperties();
    }

    public int GetVisibleTreeCount()
    {
        var visibleCount = 0;
        
        for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < _columnSize; columnIndex++)
            {
                if (Map[rowIndex][columnIndex].Visible)
                {
                    visibleCount++;
                }
            }
        }

        return visibleCount;
    }

    public int GetHighestScenicScore()
    {
        var highestScore = 0;
        
        for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < _columnSize; columnIndex++)
            {
                var currentScore = Map[rowIndex][columnIndex].ScenicScore;
                if (currentScore > highestScore)
                {
                    highestScore = currentScore;
                }
            }
        }

        return highestScore;
    }

    private void SetTreeProperties()
    {
        for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < _columnSize; columnIndex++)
            {
                var height = Map[rowIndex][columnIndex].Height;
                var treesUp = GetTreesFromTargetToEdge(0, columnIndex, rowIndex - 1, columnIndex, true);
                var treesLeft = GetTreesFromTargetToEdge(rowIndex, 0, rowIndex, columnIndex - 1, true);
                var treesDown = GetTreesFromTargetToEdge(rowIndex + 1, columnIndex, _rowSize - 1, columnIndex, false);
                var treesRight = GetTreesFromTargetToEdge(rowIndex, columnIndex + 1, rowIndex, _columnSize - 1, false);
                
                // visibility
                var visible = IsEdgeTree(rowIndex, columnIndex);
                if (!visible) visible = treesUp.All(x => x.Height < height);
                if (!visible) visible = treesLeft.All(x => x.Height < height);
                if (!visible) visible = treesDown.All(x => x.Height < height);
                if (!visible) visible = treesRight.All(x => x.Height < height);
                Map[rowIndex][columnIndex].Visible = visible;
                
                // scenic score
                var scenicScoreUp = GetScenicScoreInOneDirection(treesUp, height);
                var scenicScoreLeft = GetScenicScoreInOneDirection(treesLeft, height);
                var scenicScoreDown = GetScenicScoreInOneDirection(treesDown, height);
                var scenicScoreRight = GetScenicScoreInOneDirection(treesRight, height);
                Map[rowIndex][columnIndex].ScenicScore = scenicScoreUp * scenicScoreLeft * scenicScoreDown * scenicScoreRight;
            }
        }
    }

    private bool IsEdgeTree(int rowIndex, int columnIndex)
    {
        return rowIndex == 0 ||
               rowIndex == _rowSize - 1 ||
               columnIndex == 0 ||
               columnIndex == _columnSize - 1;
    }

    private List<Tree> GetTreesFromTargetToEdge(
        int startRowIndex, int startColumnIndex,
        int endRowIndex, int endColumnIndex,
        bool reverse)
    {
        List<Tree> trees = new();

        for (var rowIndex = startRowIndex; rowIndex <= endRowIndex; rowIndex++)
        {
            for (var columnIndex = startColumnIndex; columnIndex <= endColumnIndex; columnIndex++)
            {
                trees.Add(Map[rowIndex][columnIndex]);
            }
        }

        if (reverse)
        {
            trees.Reverse();
        }
        
        return trees;
    }
    
    private int GetScenicScoreInOneDirection(List<Tree> trees, int height)
    {
        var treeCount = 0;
        foreach (var tree in trees)
        {
            treeCount++;
            if (tree.Height >= height)
            {
                return treeCount;
            }
        }

        return treeCount;
    }
}