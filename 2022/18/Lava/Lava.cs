using AoC.Common;

namespace Lava;

public class Lava
{
    private int _maxX;
    private int _maxY;
    private int _maxZ;

    public HashSet<Coordinate3D> LavaDroplets { get; private set; } = new();
    
    public void ParseInput(string[] inputLines)
    {
        foreach (var inputLine in inputLines)
        {
            var numbers = inputLine.ExtractIntegers().ToList();
            var x = Convert.ToInt32(numbers[0]);
            var y = Convert.ToInt32(numbers[1]);
            var z = Convert.ToInt32(numbers[2]);

            LavaDroplets.Add(new Coordinate3D(x, y, z));
        }
    }

    public int GetTotalSurfaceArea()
    {
        var totalSurfaceArea = 0;
        
        foreach (var lavaDroplet in LavaDroplets)
        {
            totalSurfaceArea += 6;
            var neighbourLocations = lavaDroplet.Neighbours();
            foreach (var neighbour in neighbourLocations)
            {
                if (LavaDroplets.Contains(neighbour))
                {
                    totalSurfaceArea--;
                }
            }
        }

        return totalSurfaceArea;
    }
}