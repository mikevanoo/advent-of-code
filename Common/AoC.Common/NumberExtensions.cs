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
   
   public static T GreatestCommonDivisor<T>(this IEnumerable<T> values)
      where T : INumber<T>
   {
      return values.Aggregate(GreatestCommonDivisor);
   }
   
   public static T GreatestCommonDivisor<T>(T a, T b)
      where T : INumber<T>
   {
      while (b != T.Zero)
      {
         var remainder = a % b;
         a = b;
         b = remainder;
      }
 
      return a;
   }
   
   public static T LowestCommonMultiple<T>(this IEnumerable<T> values)
      where T : INumber<T>
   {
      return values.Aggregate(LowestCommonMultiple);
   }
   
   public static T LowestCommonMultiple<T>(T a, T b)
      where T : INumber<T>
   {
      return a * b / GreatestCommonDivisor(a, b);
   }
}