using System;
using System.Collections.Generic;
using System.Linq;

namespace CC_Lib.ExtensionMethods
{
    public static partial class EnumerableExtensions
    {
        #region ForEach

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> body)
        {
            foreach (var elem in enumerable)
            {
                body(elem);
            }
        }

        public static void ForEach<T>(this IEnumerable<dynamic> enumerable, Action<T> body, params Func<dynamic, IEnumerable<dynamic>>[] selectors)
        {
            var (currentSelector, selectorTail) = selectors.HeadTail();

            if (currentSelector != null)
            {
                enumerable.ForEach(
                    elem => currentSelector(elem).ForEach(body, selectorTail)
                );
            }
            else
            {
                foreach (var elem in enumerable)
                {
                    body(elem);
                }
            }
        }

        #endregion

        #region Enumerate

        /// <summary>
        /// Performs the given the given body on each element of the given IEnumerable and also passes the index of the current element 
        /// </summary>
        /// <param name="enumerable">The source for the enumeration</param>
        /// <param name="body">A body which takes an int (the index) and an element. This function is called for each element int the IEnumerable</param>
        public static void Enumerate<T>(this IEnumerable<T> enumerable, Action<int, T> body)
        {
            var index = 0;
            foreach (var value in enumerable)
            {
                body(index++, value);
            }
        }

        /// <summary>
        /// Performs the given the given function on each element of the given IEnumerable and also passes the index of the current element.
        /// If the given function returns false, the iteration will be stopped.
        /// </summary>
        /// <param name="enumerable">The source for the enumeration</param>
        /// <param name="body">
        /// A function which takes an int (the index) and an element. 
        /// It should return true if the enumeration should be continued, false if it should be stopped.
        /// </param>
        /// <returns>True the given function returned true for all elements, otherwise false</returns>
        public static bool Enumerate<T>(this IEnumerable<T> enumerable, Func<int, T, bool> body)
        {
            var index = 0;
            return enumerable.All(value => body(index++, value));
        }

        #endregion

    }
}
