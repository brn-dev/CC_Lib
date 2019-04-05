using System.Linq;

namespace CC_Lib.ExtensionMethods
{
    public static partial class StringExtensions
    {
        private const char DefaultSplitToken = ' ';

        public static string[] ToTokens(this string str, char splitToken = DefaultSplitToken)
        {
            return str.Split(splitToken);
        }

        public static int[] ToIntTokens(this string[] strings)
        {
            return strings.Select(int.Parse).ToArray();
        }

        public static int[] ToIntTokens(this string str, char splitToken = DefaultSplitToken)
        {
            return str.ToTokens(splitToken).ToIntTokens();
        }

        public static double[] ToDoubleTokens(this string[] strings)
        {
            return strings.Select(double.Parse).ToArray();
        }

        public static double[] ToDoubleTokens(this string str, char splitToken = DefaultSplitToken)
        {
            return str.ToTokens(splitToken).ToDoubleTokens();
        }
    }
}
