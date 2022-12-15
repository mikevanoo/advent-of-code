using System.Text;
using System.Text.RegularExpressions;

namespace Beacons;

public partial class Beacons
{
    [GeneratedRegex(@"[-\d]+")]
    private static partial Regex GetNumbersRegex();
    
    private int _minX, _maxX, _minY, _maxY;
    
    public Dictionary<Coordinate, Coordinate> SensorsAndBeacons { get; } = new();
    public Dictionary<Coordinate, Deadzone> SensorsAndDeadzones { get; } = new();

    public void ParseInput(string[] inputLines)
    {
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
            
            var sensor = new Coordinate(sensorX, sensorY);
            var beacon = new Coordinate(beaconX, beaconY);
            SensorsAndBeacons.Add(sensor, beacon);
            SensorsAndDeadzones.Add(sensor, new Deadzone(sensor, GetManhattanDistance(sensor, beacon)));
        }
    }

    public int GetCannotBeBeaconCountOnRow(int rowIndex)
    {
        var cannotBeBeaconCount = 0;
        
        for (var x = _minX; x <= _maxX; x++)
        {
            var cell = new Coordinate(x, rowIndex);
            foreach (var deadzone in SensorsAndDeadzones.Values)
            {
                var isSensor = SensorsAndBeacons.Keys.Contains(cell);
                var isBeacon = SensorsAndBeacons.Values.Contains(cell);
                
                if (isSensor || isBeacon || deadzone.Contains(cell))
                {
                    cannotBeBeaconCount++;
                    break;
                }
            }
        }

        return cannotBeBeaconCount;
    }
    
    private int GetManhattanDistance(Coordinate a, Coordinate b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
}