using System.Numerics;

namespace AoC.Common;

public static class NumberExtensions
{
   public static T Minimum<T>(this IEnumerable<T> values)
      where T : INumber<T>, IMinMaxValue<T>
   {
      return values.Aggregate(T.MaxValue, T.Min);
   }
   
   public static T Maximum<T>(this IEnumerable<T> values)
      where T : INumber<T>, IMinMaxValue<T>
   {
      return values.Aggregate(T.MinValue, T.Max);
   }
}