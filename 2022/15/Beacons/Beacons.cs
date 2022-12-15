using System.Text;
using System.Text.RegularExpressions;

namespace Beacons;

public partial class Beacons
{
    [GeneratedRegex(@"[-\d]+")]
    private static partial Regex GetNumbersRegex();
    
    private int _minX, _maxX, _minY, _maxY;
    
    public Dictionary<Coordinate, Coordinate> SensorAndBeacons { get; } = new();
    public Dictionary<Coordinate, CellContents> Cells { get; } = new();

    public void CreateCells(string[] inputLines)
    {
        // determine grid size
        foreach (var inputLine in inputLines)
        {
            var numbers = GetNumbersRegex().Matches(inputLine);
            var sensorX = Convert.ToInt32(numbers[0].Value);
            var sensorY = Convert.ToInt32(numbers[1].Value);
            var beaconX = Convert.ToInt32(numbers[2].Value);
            var beaconY = Convert.ToInt32(numbers[3].Value);

            _minX = Math.Min(_minX, sensorX);
            _minX = Math.Min(_minX, beaconX);
            _maxX = Math.Max(_maxX, sensorX);
            _maxX = Math.Max(_maxX, beaconX);

            _minY = Math.Min(_minY, sensorY);
            _minY = Math.Min(_minY, beaconY);
            _maxY = Math.Max(_maxY, sensorY);
            _maxY = Math.Max(_maxY, beaconY);

            // accumulate sensor and closest beacon positions
            SensorAndBeacons.Add(new Coordinate(sensorX, sensorY), new Coordinate(beaconX, beaconY));
        }

        var rangeX = Enumerable.Range(_minX, _maxX - _minX + 1).ToList();
        var rangeY = Enumerable.Range(_minY, _maxY - _minY + 1).ToList();

        // create cells for all of empty/sensors/beacons
        foreach (var x in rangeX)
        {
            foreach (var y in rangeY)
            {
                var position = new Coordinate(x, y);
                var contents = CellContents.Unknown;
                
                if (SensorAndBeacons.Keys.Contains(position))
                {
                    contents = CellContents.Sensor;
                }

                if (SensorAndBeacons.Values.Contains(position))
                {
                    contents = CellContents.Beacon;
                }

                Cells.Add(position, contents);
            }
        }
    }

    public void DetectBeacons()
    {
        foreach (var cell in Cells)
        {
            if (cell.Value == CellContents.Unknown)
            {
                foreach (var sensorAndBeacon in SensorAndBeacons)
                {
                    var distanceToClosestBeacon = GetManhattanDistance(sensorAndBeacon.Key, sensorAndBeacon.Value);
                    var distanceToCell = GetManhattanDistance(sensorAndBeacon.Key, cell.Key);
                    if (distanceToCell <= distanceToClosestBeacon)
                    {
                        Cells[cell.Key] = CellContents.CannotBeBeacon;
                    }
                }
            }
        }
    }

    public int GetCannotBeBeaconCountOnRow(int rowIndex)
    {
        return Cells.Keys
            .Where(k => k.Y == rowIndex)
            .Count(key => Cells[key] == CellContents.CannotBeBeacon);
    }
    
    public string PrintGrid()
    {
        StringBuilder result = new();

        var keys = Cells.Keys
            .OrderBy(k => k.Y)
            .ThenBy(k => k.X)
            .ToList();

        int currentY = keys.First().Y;
        result.Append(currentY.ToString().PadRight(3));
        foreach (var key in keys)
        {
            if (key.Y > currentY)
            {
                result.AppendLine();
                result.Append(key.Y.ToString().PadRight(3));
            }
            
            switch (Cells[key])
            {
                case CellContents.Unknown:
                    result.Append('.');
                    break;
                case CellContents.Sensor:
                    result.Append('S');
                    break;
                case CellContents.Beacon:
                    result.Append('B');
                    break;
                case CellContents.CannotBeBeacon:
                    result.Append('#');
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            currentY = key.Y;
        }
        
        return result.ToString();
    }

    private int GetManhattanDistance(Coordinate a, Coordinate b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
}