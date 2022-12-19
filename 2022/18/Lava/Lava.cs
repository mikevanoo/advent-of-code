using AoC.Common;

namespace Lava;

public class Lava
{
    private int _maxX;
    private int _maxY;
    private int _maxZ;

    public HashSet<Coordinate3D> LavaDropletCubes { get; private set; } = new();
    
    public void ParseInput(string[] inputLines)
    {
        foreach (var inputLine in inputLines)
        {
            var numbers = inputLine.ExtractIntegers().ToList();
            var x = Convert.ToInt32(numbers[0]);
            var y = Convert.ToInt32(numbers[1]);
            var z = Convert.ToInt32(numbers[2]);

            _maxX = Math.Max(_maxX, x);
            _maxY = Math.Max(_maxY, y);
            _maxZ = Math.Max(_maxZ, z);

            LavaDropletCubes.Add(new Coordinate3D(x, y, z));
        }
    }

    public int GetTotalSurfaceArea()
    {
        var totalSurfaceArea = 0;
        
        foreach (var lavaDroplet in LavaDropletCubes)
        {
            totalSurfaceArea += 6;
            var neighbourLocations = lavaDroplet.Neighbours();
            
            foreach (var neighbour in neighbourLocations)
            {
                if (LavaDropletCubes.Contains(neighbour))
                {
                    totalSurfaceArea--;
                }
            }
        }

        return totalSurfaceArea;
    }
    
    public int GetTotalSurfaceAreaExcludingAirPockets()
    {
        var start = new Coordinate3D(0, 0, 0);
        var q = new Queue<Coordinate3D>();
        q.Enqueue(start);
        
        var closedSet = new HashSet<Coordinate3D>();
        closedSet.Add(start);

        var virtualGrid = new int[_maxX, _maxY, _maxZ];
        
        while (q.Count > 0)
        {
            var current = q.Dequeue();
            foreach (var neighbour in current.Neighbours())
            {
                if (!neighbour.WithinBoundsOf(virtualGrid)) { continue; }
                if (closedSet.Contains(neighbour)) { continue; }
                if (LavaDropletCubes.Contains(neighbour)) { continue; }
                
                q.Enqueue(neighbour);
                closedSet.Add(neighbour);
            }
        }

        var result = 0;
        foreach (var cube in LavaDropletCubes)
        {
            foreach (var neighbour in cube.Neighbours())
            {
                if (closedSet.Contains(neighbour))
                {
                    result++;
                }
            }
        }

        return result;
    }
}