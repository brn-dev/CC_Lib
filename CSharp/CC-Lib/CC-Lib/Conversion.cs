using System.Collections.Generic;
using System.Linq;

namespace CC_Lib
{
    public static class Conversion
    {
        private const char DefaultSplitToken = ' ';

        /// <summary>
        /// Splits the strings inside the array at the split-token and returns the resulting 2-dimensional array.
        /// </summary>
        /// <param name="strings">The strings that should be token-ized</param>
        /// <param name="splitToken">The character where the strings should be splitted</param>
        public static string[][] ToTokens(this IEnumerable<string> strings, char splitToken = DefaultSplitToken)
        {
            return strings.Select(x => x.Split(splitToken)).ToArray();
        }

        public static int[][] ToIntTokens(this IEnumerable<string> strings, char splitToken = DefaultSplitToken)
        {
            return strings.Select(x => x.Split(splitToken).Select(int.Parse).ToArray()).ToArray();
        }

        public static double[][] ToDoubleTokens(this IEnumerable<string> strings, char splitToken = DefaultSplitToken)
        {
            return strings.Select(x => x.Split(splitToken).Select(double.Parse).ToArray()).ToArray();
        }
    }
}
