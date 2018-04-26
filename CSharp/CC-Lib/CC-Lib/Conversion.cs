using System;
using System.Globalization;
using System.Linq;

namespace CC_Lib
{
    public static class Conversion
    {
        private const char DefaultSplitToken = ' ';
        private static readonly CultureInfo NumberParsingCulture = new CultureInfo("en");
        private static readonly CultureInfo DateParsingCulture = new CultureInfo("en");
        private static readonly CultureInfo TimeParsingCulture = new CultureInfo("en");

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

        /// <summary>
        /// Parses the given string to the given type
        /// </summary>
        /// <param name="str">the source string</param>
        /// <param name="type">the destination type</param>
        /// <returns>A dynamic object of the given type representing the given string value</returns>
        public static dynamic ParseDyn(this string str, Type type)
        {
            if (type == typeof(string))
            {
                return str;
            }
            if (type == typeof(int))
            {
                return int.Parse(str, NumberParsingCulture);
            }
            if (type == typeof(long))
            {
                return long.Parse(str, NumberParsingCulture);
            }
            if (type == typeof(float))
            {
                return float.Parse(str, NumberParsingCulture);
            }
            if (type == typeof(double))
            {
                return double.Parse(str, NumberParsingCulture);
            }
            if (type == typeof(decimal))
            {
                return decimal.Parse(str, NumberParsingCulture);
            }
            if (type == typeof(DateTime))
            {
                return DateTime.Parse(str, DateParsingCulture);
            }
            if (type == typeof(TimeSpan))
            {
                return TimeSpan.Parse(str, TimeParsingCulture);
            }
            throw new ArgumentOutOfRangeException($"Type {type} not supported for parsing!");
        }

        public static TR Return<TR, TS>(this TS source, Func<TS, TR> func)
        {
            return func(source);
        }

        /// <summary>
        /// Splits a string at the whitespaces and converts the resulting tokens to the given types
        /// </summary>
        /// <param name="str">The string which will be split to tokens</param>
        /// <param name="types">The types to which each to will be parsed respectively</param>
        /// <returns>The parsed tokens</returns>
        public static dynamic[] ParseStringDyn(this string str, params Type[] types)
        {
            return ParseStringDyn(str, DefaultSplitToken, types);
        }

        /// <summary>
        /// Splits a string at the whitespaces and converts the resulting tokens to the given types
        /// </summary>
        /// <param name="str">The string which will be split to tokens</param>
        /// <param name="splitToken">The char whith which the string will be split</param>
        /// <param name="types">The types to which each to will be parsed respectively</param>
        /// <returns>The parsed tokens</returns>
        public static dynamic[] ParseStringDyn(this string str, char splitToken, params Type[] types)
        {
            return ParseStringDyn(str.Split(splitToken), types);
        }

        /// <summary>
        /// Uses the tokens to convert the given types
        /// </summary>
        /// <param name="stringTokens">The tokens which will be parsed</param>
        /// <param name="types">The types to which each to will be parsed respectively</param>
        /// <returns>The parsed tokens</returns>
        public static dynamic[] ParseStringDyn(this string[] stringTokens, params Type[] types)
        {
            if (stringTokens.Length != types.Length)
            {
                throw new ArgumentException("stringTokens has to have the same length as types!");
            }
            return stringTokens.Zip(types, (str, type) => ParseDyn(str, type)).ToArray();
        }

        #region ParseAndDeconstruct

        /// <summary>
        /// Parses the given tokens to into the given out parameters Type
        /// </summary>
        /// <param name="tokens">The source strings that should be parsed</param>
        /// <param name="t0">The variable to which the first element in the array will be parsed to</param>
        /// <returns>True everytime for the sake of method chaining</returns>
        public static bool ParseAndDeconstruct<T0>(this string[] tokens, out T0 t0)
        {
            if (tokens.Length < 1)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            return true;
        }

        /// <summary>
        /// Parses the given tokens to into the given out parameters Type
        /// </summary>
        /// <param name="str">The source string that should be parsed</param>
        /// <param name="t0">The variable to which the first element in the array will be parsed to</param>
        /// <param name="splitToken">The char where the string will be split</param>
        /// <returns>True everytime for the sake of method chaining</returns>
        public static bool ParseAndDeconstruct<T0>(this string str, out T0 t0, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0);
        }

        public static bool ParseAndDeconstruct<T0, T1>(this string[] tokens, out T0 t0, out T1 t1)
        {
            if (tokens.Length < 2)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1>(this string str, out T0 t0, out T1 t1, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2)
        {
            if (tokens.Length < 3)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2>(this string str, out T0 t0, out T1 t1, out T2 t2, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3)
        {
            if (tokens.Length < 4)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4)
        {
            if (tokens.Length < 5)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5)
        {
            if (tokens.Length < 6)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6)
        {
            if (tokens.Length < 7)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7)
        {
            if (tokens.Length < 8)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8)
        {
            if (tokens.Length < 9)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9)
        {
            if (tokens.Length < 10)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10)
        {
            if (tokens.Length < 11)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11)
        {
            if (tokens.Length < 12)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12)
        {
            if (tokens.Length < 13)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13)
        {
            if (tokens.Length < 14)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            t13 = tokens[13].ParseDyn(typeof(T13));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12, out t13);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14)
        {
            if (tokens.Length < 15)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            t13 = tokens[13].ParseDyn(typeof(T13));
            t14 = tokens[14].ParseDyn(typeof(T14));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12, out t13, out t14);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15)
        {
            if (tokens.Length < 16)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            t13 = tokens[13].ParseDyn(typeof(T13));
            t14 = tokens[14].ParseDyn(typeof(T14));
            t15 = tokens[15].ParseDyn(typeof(T15));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12, out t13, out t14, out t15);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16)
        {
            if (tokens.Length < 17)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            t13 = tokens[13].ParseDyn(typeof(T13));
            t14 = tokens[14].ParseDyn(typeof(T14));
            t15 = tokens[15].ParseDyn(typeof(T15));
            t16 = tokens[16].ParseDyn(typeof(T16));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12, out t13, out t14, out t15, out t16);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16, out T17 t17)
        {
            if (tokens.Length < 18)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            t13 = tokens[13].ParseDyn(typeof(T13));
            t14 = tokens[14].ParseDyn(typeof(T14));
            t15 = tokens[15].ParseDyn(typeof(T15));
            t16 = tokens[16].ParseDyn(typeof(T16));
            t17 = tokens[17].ParseDyn(typeof(T17));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16, out T17 t17, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12, out t13, out t14, out t15, out t16, out t17);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16, out T17 t17, out T18 t18)
        {
            if (tokens.Length < 19)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            t13 = tokens[13].ParseDyn(typeof(T13));
            t14 = tokens[14].ParseDyn(typeof(T14));
            t15 = tokens[15].ParseDyn(typeof(T15));
            t16 = tokens[16].ParseDyn(typeof(T16));
            t17 = tokens[17].ParseDyn(typeof(T17));
            t18 = tokens[18].ParseDyn(typeof(T18));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16, out T17 t17, out T18 t18, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12, out t13, out t14, out t15, out t16, out t17, out t18);
        }


        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this string[] tokens, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16, out T17 t17, out T18 t18, out T19 t19)
        {
            if (tokens.Length < 20)
            {
                throw new ArgumentException("Not enough tokens in the token array to deconstruct it!");
            }
            t0 = tokens[0].ParseDyn(typeof(T0));
            t1 = tokens[1].ParseDyn(typeof(T1));
            t2 = tokens[2].ParseDyn(typeof(T2));
            t3 = tokens[3].ParseDyn(typeof(T3));
            t4 = tokens[4].ParseDyn(typeof(T4));
            t5 = tokens[5].ParseDyn(typeof(T5));
            t6 = tokens[6].ParseDyn(typeof(T6));
            t7 = tokens[7].ParseDyn(typeof(T7));
            t8 = tokens[8].ParseDyn(typeof(T8));
            t9 = tokens[9].ParseDyn(typeof(T9));
            t10 = tokens[10].ParseDyn(typeof(T10));
            t11 = tokens[11].ParseDyn(typeof(T11));
            t12 = tokens[12].ParseDyn(typeof(T12));
            t13 = tokens[13].ParseDyn(typeof(T13));
            t14 = tokens[14].ParseDyn(typeof(T14));
            t15 = tokens[15].ParseDyn(typeof(T15));
            t16 = tokens[16].ParseDyn(typeof(T16));
            t17 = tokens[17].ParseDyn(typeof(T17));
            t18 = tokens[18].ParseDyn(typeof(T18));
            t19 = tokens[19].ParseDyn(typeof(T19));
            return true;
        }

        public static bool ParseAndDeconstruct<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this string str, out T0 t0, out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7, out T8 t8, out T9 t9, out T10 t10, out T11 t11, out T12 t12, out T13 t13, out T14 t14, out T15 t15, out T16 t16, out T17 t17, out T18 t18, out T19 t19, char splitToken = DefaultSplitToken)
        {
            return ParseAndDeconstruct(str.ToTokens(splitToken), out t0, out t1, out t2, out t3, out t4, out t5, out t6, out t7, out t8, out t9, out t10, out t11, out t12, out t13, out t14, out t15, out t16, out t17, out t18, out t19);
        }
        #endregion
    }

}