using System.Reflection.PortableExecutable;
using AoC.Common;

namespace Vents;

public class VentMap
{
    public Dictionary<Coordinate, int> Vents { get; private set; } = new();
    
    public void ParseInput(string[] inputLines, bool includeDiagonals = false)
    {
        foreach (var inputLine in inputLines)
        {
            var numbers = inputLine.ExtractIntegers().ToList();
            var startX = numbers[0];
            var startY = numbers[1];
            var endX = numbers[2];
            var endY = numbers[3];
            
            if (!includeDiagonals && startX != endX && startY != endY) { continue; }
            
            var start = new Coordinate(startX, startY);
            var end = new Coordinate(endX, endY);
            var path = start.DrawStraightPathTo(end);

            foreach (var coordinate in path)
            {
                if (Vents.TryGetValue(coordinate, out var currentCount))
                {
                    Vents[coordinate] = currentCount + 1;
                }
                else
                {
                    Vents.Add(coordinate, 1);
                }
            }
        }
    }

    public int GetDangerousPointsCount()
    {
        return Vents.Values.Count(x => x >= 2);
    }
}