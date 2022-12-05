namespace SonarSweep;

public record struct Measurement(int Value, MeasurementChange ChangeFromPrevious);