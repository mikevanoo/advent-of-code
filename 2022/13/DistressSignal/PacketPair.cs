using Newtonsoft.Json.Linq;

namespace DistressSignal;

public class PacketPair
{
    private Packet _left = new();
    private Packet _right = new();

    public void ParseInput(string left, string right)
    {
        _left = new Packet();
        _left.ParseInput(left);

        _right = new Packet();
        _right.ParseInput(right);
    }

    public bool IsInCorrectOrder()
    {
        return IsInCorrectOrder(_left.Value, _right.Value).Value;
    }

    private bool? IsInCorrectOrder(JToken leftArray, JToken rightArray)
    {
        if (leftArray.Type == JTokenType.Integer && rightArray.Type == JTokenType.Integer)
        {
            if ((int)leftArray != (int)rightArray)
            {
                return (int)leftArray < (int)rightArray;
            }
        }
        else
        {
            for (var index = 0; index < leftArray.Values().Count(); index++)
            {
                var result = IsInCorrectOrder(
                    leftArray[index].Type != JTokenType.Array ? new JArray(leftArray[index]) : leftArray[index], 
                    rightArray[index].Type != JTokenType.Array ? new JArray(rightArray[index]) : rightArray[index]);
                if (result.HasValue)
                {
                    return result;
                }
            }
        }
        
        return null;
    }

    public override string ToString()
    {
        return _left +
               Environment.NewLine +
               _right;

    }
}