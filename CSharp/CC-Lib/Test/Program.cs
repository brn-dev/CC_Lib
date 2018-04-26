﻿using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CC_Lib;
using MoreLinq;

namespace Test
{

    class Program
    {
        private static void Main()
        {
            
            "abc 123 123.456 2018-04-26 17:47"
                .ParseAndDeconstruct(out string str, 
                                     out int i, 
                                     out double d, 
                                     out DateTime dt, 
                                     out TimeSpan ts);
            
        }

        private static void DeconstructorGenerator()
        {
            for (int i = 1; i < 21; i++)
            {
                string genericList = "";
                string parameterList = "";
                string assignmentList = "";
                string passingList = "";
                for (int j = 1; j < i; j++)
                {
                    genericList += $", T{j}";
                    parameterList += $", out T{j} t{j}";
                    assignmentList += $"t{j} = tokens[{j}].ParseDyn(typeof(T{j}));\n";
                    passingList += $", out t{j}";
                }

                Console.WriteLine(@"public static void ParseAndDeconstruct<T0" + genericList + @">(this string[] tokens, out T0 t0" + parameterList + @")
                {
                    if (tokens.Length < " + i + @")
                    {
                        throw new ArgumentException(""Not enough tokens in the token array to deconstruct it!"");
                    }
                    t0 = tokens[0].ParseDyn(typeof(T0));
                    " + assignmentList + @"
                }
                ");
                Console.WriteLine(@"public static void ParseAndDeconstruct<T0" + genericList + @">(this string str, out T0 t0" + parameterList + @", char splitToken = DefaultSplitToken)
        {
            ParseAndDeconstruct(str.ToTokens(splitToken), out t0" + passingList + @");
        }
");
                Console.ReadLine();
            }

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
