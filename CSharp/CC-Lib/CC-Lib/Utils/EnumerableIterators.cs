using System;
using System.Collections.Generic;

namespace CC_Lib.Utils
{
    public static class EnumerableIterators
    {
        /// <summary>
        /// Performs the given the given action on each element of the given IEnumerable and also passes the index of the current element 
        /// </summary>
        /// <param name="enumerable">The source for the enumeration</param>
        /// <param name="action">A action which takes an int (the index) and an element. This function is called for each element int the IEnumerable</param>
        public static void Enumerate<T>(this IEnumerable<T> enumerable, Action<int, T> action)
        {
            var index = 0;
            foreach (T value in enumerable)
            {
                action(index++, value);
            }
        }

        /// <summary>
        /// Performs the given the given function on each element of the given IEnumerable and also passes the index of the current element.
        /// If the given function returns false, the iteration will be stopped.
        /// </summary>
        /// <param name="enumerable">The source for the enumeration</param>
        /// <param name="func">
        /// A function which takes an int (the index) and an element. 
        /// It should return true if the enumeration should be continued, false if it should be stopped.
        /// </param>
        /// <returns>True the given function returned true for all elements, otherwise false</returns>
        public static bool Enumerate<T>(this IEnumerable<T> enumerable, Func<int, T, bool> func)
        {
            int index = 0;
            foreach (T value in enumerable)
            {
                if (!func(index++, value))
                {
                    return false;
                }
            }
            return true;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> func)
        {
            foreach (var elem in enumerable)
            {
                func(elem);
            }
        }

    }
}
