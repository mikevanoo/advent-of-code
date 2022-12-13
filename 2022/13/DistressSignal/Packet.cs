using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DistressSignal;

public class Packet
{
    public JArray Value { get; private set; } = new();

    public void ParseInput(string inputLine)
    {
        Value = JArray.Parse(inputLine);
    }

    public override string ToString()
    {
        return Value.ToString(Formatting.None);
    }
}