using System;
using System.Collections.Generic;
using System.Linq;

namespace CC_Lib
{
    /// <summary>
    /// Short for Type, used for parsing
    /// </summary>
    public enum Ty
    {
        Str,
        Int,
        Long,
        Float,
        Double,
        Decimal,
        TimeSpan,
        DateTime
    }

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

        /// <summary>
        /// Splits a line at the whitespaces and converts the resulting tokens to the given types
        /// </summary>
        /// <param name="line">The line which will be split to tokens</param>
        /// <param name="ty">The types to which each to will be parsed respectively</param>
        /// <returns>The parsed tokens</returns>
        public static dynamic[] ParseLine(this string line, params Ty[] ty)
        {
            return ParseLine(line, DefaultSplitToken, ty);
        }

        /// <summary>
        /// Splits a line at the whitespaces and converts the resulting tokens to the given types
        /// </summary>
        /// <param name="line">The line which will be split to tokens</param>
        /// <param name="splitToken">The char whith which the line will be split</param>
        /// <param name="ty">The types to which each to will be parsed respectively</param>
        /// <returns>The parsed tokens</returns>
        public static dynamic[] ParseLine(this string line, char splitToken, params Ty[] ty)
        {
            return ParseLine(line.Split(splitToken), ty);
        }

        /// <summary>
        /// Uses the tokens to convert the given types
        /// </summary>
        /// <param name="lineTokens">The tokens which will be parsed</param>
        /// <param name="ty">The types to which each to will be parsed respectively</param>
        /// <returns>The parsed tokens</returns>
        public static dynamic[] ParseLine(this string[] lineTokens, params Ty[] ty)
        {
            var parsedTokens = new dynamic[lineTokens.Length];
            for (int i = 0; i < lineTokens.Length; i++)
            {
                switch (ty[i])
                {
                    case Ty.Str:
                        parsedTokens[i] = lineTokens[i];
                        break;
                    case Ty.Int:
                        parsedTokens[i] = int.Parse(lineTokens[i]);
                        break;
                    case Ty.Long:
                        parsedTokens[i] = long.Parse(lineTokens[i]);
                        break;
                    case Ty.Float:
                        parsedTokens[i] = float.Parse(lineTokens[i]);
                        break;
                    case Ty.Double:
                        parsedTokens[i] = double.Parse(lineTokens[i]);
                        break;
                    case Ty.Decimal:
                        parsedTokens[i] = decimal.Parse(lineTokens[i]);
                        break;
                    case Ty.DateTime:
                        parsedTokens[i] = DateTime.Parse(lineTokens[i]);
                        break;
                    case Ty.TimeSpan:
                        parsedTokens[i] = TimeSpan.Parse(lineTokens[i]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return parsedTokens;
        }
    }
}
