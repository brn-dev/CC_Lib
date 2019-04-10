using System;
using System.Collections.Generic;
using System.Linq;

namespace CC_Lib.ExtensionMethods
{
    public static partial class EnumerableExtensions
    {
        public static T[] SubArray<T>(this IEnumerable<T> source, int from, int toExclusive = -1)
        {
            var arr = source.ToArray();
            if (toExclusive < 0)
            {
                toExclusive = arr.Length;
            }
            var length = toExclusive - from;
            var result = new T[length];
            Array.Copy(arr, from, result, 0, length);
            return result;
        }

        #region HeadTail

        public static (T, T[]) HeadTail<T>(this IEnumerable<T> enumerable)
        {
            var arr = enumerable as T[] ?? enumerable.ToArray();

            if (typeof(T).IsValueType && arr.Length == 0)
            {
                throw new ArgumentException("The value-type enumerable needs to have at least one element");
            }

            var count = arr.Length;
            switch (count)
            {
                case 0:
                    return (default(T), new T[0]);
                case 1:
                    return (arr[0], new T[0]);
                default:
                    var head = arr[0];
                    var tail = arr.SubArray(1);
                    return (head, tail);
            }
        }

        public static (T, T[]) HeadTail<T>(this IEnumerable<T> enumerable, out T head, out T[] tail)
        {
            (head, tail) = HeadTail(enumerable);
            return (head, tail);
        }

        #endregion

        public static T[][] MakeChunks<T>(IEnumerable<T> enumerable, int chunkSize)
        {
            var arr = enumerable.ToArray();

            if (arr.Length % chunkSize != 0)
            {
                throw new ArgumentException(
                    $"length is not evenly divisible by the chunkSize ({arr.Length}/{chunkSize})");
            }

            var chunks = new T[arr.Length / chunkSize][];

            for (var i = 0; i < chunks.Length; i++)
            {
                var index = 3 * chunkSize;
                chunks[i] = arr.SubArray(index, index + chunkSize);
            }

            return chunks;
        }

        public static T MinBy<T, TS>(this IEnumerable<T> enumerable, Func<T, TS> selector)
            where TS : IComparable<TS>
        {
            var arr = enumerable as T[] ?? enumerable.ToArray();

            if (arr.Length == 0)
            {
                if (typeof(T).IsClass)
                {
                    return default(T);
                }
                throw new ArgumentException(
                    "Enumerable doesn't contain any elements. " +
                    $"Calling {nameof(MinBy)} with a non class generic type " +
                    "requires at least one element to be in the enumerable.");
            }

            var min = arr[0];

            for (var i = 1; i < arr.Length; i++)
            {
                if (selector(arr[i]).CompareTo(selector(min)) < 0)
                {
                    min = arr[i];
                }
            }

            return min;
        }

        public static T MaxBy<T, TS>(this IEnumerable<T> enumerable, Func<T, TS> selector)
            where TS : IComparable<TS>
        {
            var arr = enumerable as T[] ?? enumerable.ToArray();

            if (arr.Length == 0)
            {
                if (typeof(T).IsClass)
                {
                    return default(T);
                }
                throw new ArgumentException(
                    "Enumerable doesn't contain any elements. " +
                    $"Calling {nameof(MaxBy)} with a non class generic type " +
                    "requires at least one element to be in the enumerable.");
            }

            var max = arr[0];

            for (var i = 1; i < arr.Length; i++)
            {
                if (selector(arr[i]).CompareTo(selector(max)) > 0)
                {
                    max = arr[i];
                }
            }

            return max;
        }
    }
}
