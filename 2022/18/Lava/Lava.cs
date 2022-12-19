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
        var totalSurfaceArea = GetTotalSurfaceArea();

        // check every position in the grid
        for (var x = 0; x <= _maxX; x++)
        {
            for (var y = 0; y <= _maxY; y++)
            {
                for (var z = 0; z <= _maxZ; z++)
                {
                    var position = new Coordinate3D(x, y, z);
                    if (!LavaDropletCubes.Contains(position))
                    {
                        // if the position does not contain lava
                        var neighbourLocations = position.Neighbours();
                        var positionIsSurrounded = true;
                        
                        // check it's neighbours
                        foreach (var neighbour in neighbourLocations)
                        {
                            // if all of the neighbours contain lava then the position is an air pocket
                            if (!LavaDropletCubes.Contains(neighbour))
                            {
                                positionIsSurrounded = false;
                                break;
                            }
                        }

                        if (positionIsSurrounded)
                        {
                            totalSurfaceArea -= 6;
                        }
                    }
                }
            }   
        }
       
        return totalSurfaceArea;
    }
}