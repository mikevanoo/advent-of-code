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
        return IsInCorrectOrder(_left.Value, _right.Value) > 0;
    }

    private int IsInCorrectOrder(JToken leftArray, JToken rightArray)
    {
        if (leftArray.Type == JTokenType.Integer && rightArray.Type == JTokenType.Integer)
        {
            return Math.Sign((int)rightArray - (int)leftArray);
        }

        if (leftArray.Type == JTokenType.Array &&
            leftArray.Count() == 1 &&
            !leftArray[0].HasValues &&
            rightArray.Type == JTokenType.Array && 
            rightArray.Count() == 1 &&
            !rightArray[0].HasValues)
        {
            return IsInCorrectOrder(leftArray[0], rightArray[0]);
        }

        if (!leftArray.Any()) { return 1; }
        if (!rightArray.Any()) { return -1; }
            
        for (var index = 0; index < leftArray.Values().Count(); index++)
        {
            if (index > leftArray.Count() - 1) { return 1; }
            if (index > rightArray.Count() - 1) { return -1; }
                
            var result = IsInCorrectOrder(
                leftArray[index].Type != JTokenType.Array ? new JArray(leftArray[index]) : leftArray[index], 
                rightArray[index].Type != JTokenType.Array ? new JArray(rightArray[index]) : rightArray[index]);
            if (result != 0)
            {
                return result;
            }
        }

        return 0;
    }

    public override string ToString()
    {
        return _left +
               Environment.NewLine +
               _right;

    }
}