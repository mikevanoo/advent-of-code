namespace SonarSweep;

public class SonarSweep
{
    public int GetNumberOfIncreasedDepthMeasurements(string[] inputLines)
    {
        List<Measurement> measurements = new();
        
        for (var index = 0; index < inputLines.Length; index++)
        {
            var measurement = Convert.ToInt32(inputLines[index]);
            var changeFromPrevious = MeasurementChange.NotApplicable;
            if (index > 0)
            {
                var previousMeasurement = Convert.ToInt32(inputLines[index - 1]);
                if (measurement > previousMeasurement)
                {
                    changeFromPrevious = MeasurementChange.Increased;
                }
                if (measurement < previousMeasurement)
                {
                    changeFromPrevious = MeasurementChange.Decreased;
                }
            }

            measurements.Add(new Measurement(Convert.ToInt32(measurement), changeFromPrevious));
        }
        
        return measurements.Count(x => x.ChangeFromPrevious == MeasurementChange.Increased);
    }
}