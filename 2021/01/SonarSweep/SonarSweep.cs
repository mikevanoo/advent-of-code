namespace SonarSweep;

public class SonarSweep
{
    public int GetNumberOfIncreasedDepthMeasurements(string[] inputLines)
    {
        List<Measurement> measurements = new();
        
        for (var index = 0; index < inputLines.Length; index++)
        {
            var measurement = Convert.ToInt32(inputLines[index]);
            var changeFromPrevious = GetChangeFromPrevious(measurements, index, measurement);
            measurements.Add(new Measurement(measurement, changeFromPrevious));
        }
        
        return measurements.Count(x => x.ChangeFromPrevious == MeasurementChange.Increased);
    }

    public int GetNumberOfIncreasedDepthMeasurementsInSlidingWindow(string[] inputLines)
    {
        List<Measurement> measurements = new();
        
        for (var index = 0; index < inputLines.Length; index++)
        {
            if (!CanBuildGroupOf3(inputLines, index))
            {
                break;
            }

            var measurement = GetGroupOf3Measurement(inputLines, index);
            var changeFromPrevious = GetChangeFromPrevious(measurements, index, measurement);
            measurements.Add(new Measurement(measurement, changeFromPrevious));
        }
        
        return measurements.Count(x => x.ChangeFromPrevious == MeasurementChange.Increased);
    }

    private bool CanBuildGroupOf3(string[] inputLines, int index)
    {
        return (index + 2 < inputLines.Length);
    }

    private int GetGroupOf3Measurement(string[] inputLines, int index)
    {
        return inputLines[index..(index + 3)].Sum(Convert.ToInt32);
    }

    private MeasurementChange GetChangeFromPrevious(
        IReadOnlyList<Measurement> measurements, 
        int currentIndex,
        int currentMeasurement)
    {
        var changeFromPrevious = MeasurementChange.NotApplicable;
        if (currentIndex <= 0)
        {
            return changeFromPrevious;
        }
        
        var previousMeasurement = measurements[currentIndex - 1].Value;
        if (currentMeasurement > previousMeasurement)
        {
            changeFromPrevious = MeasurementChange.Increased;
        }
        if (currentMeasurement < previousMeasurement)
        {
            changeFromPrevious = MeasurementChange.Decreased;
        }

        return changeFromPrevious;
    }
}