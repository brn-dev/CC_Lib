using System;
using System.Collections.Generic;
using CC_Lib;

namespace Test
{

    class Test
    {
        public Test(string str, int x, double d, DateTime dt, TimeSpan ts)
        {
            Str = str;
            I = x;
            D = d;
            Dt = dt;
            Ts = ts;
        }

        public string Str { get; set; }

        public int I { get; set; }

        public double D { get; set; }

        public DateTime Dt { get; set; }

        public TimeSpan Ts { get; set; }
    }

    class Program
    {
        private static void Main()
        {
            var x = new int[,]
            {
                {1, 2, 3},
                {4, 5, 6},
            };
            x.VisualizeToConsole();

            var row = x.GetRow(1);
            var col = x.GetColumn(2);

            var mV = x.MirrorVertically();
            mV.VisualizeToConsole();
            var mH = x.MirrorHorizontally();
            mH.VisualizeToConsole();

            var rcc = x.RotateCounterClockwise();
            rcc.VisualizeToConsole();

            var rc = x.RotateClockwise();
            rc.VisualizeToConsole();

            rcc.RotateCounterClockwise().VisualizeToConsole();
            rc.RotateClockwise().VisualizeToConsole();

            var r180 = x.Rotate180();
            r180.VisualizeToConsole();

            Console.ForegroundColor = ConsoleColor.Cyan;
            x.RotateClockwiseTimes(0).VisualizeToConsole();
            x.RotateClockwiseTimes(1).VisualizeToConsole();
            x.RotateClockwiseTimes(2).VisualizeToConsole();
            x.RotateClockwiseTimes(3).VisualizeToConsole();
            x.RotateClockwiseTimes(4).VisualizeToConsole();
            x.RotateClockwiseTimes(-1).VisualizeToConsole();
            x.RotateClockwiseTimes(-2).VisualizeToConsole();
            x.RotateClockwiseTimes(-3).VisualizeToConsole();
            x.RotateClockwiseTimes(-4).VisualizeToConsole();
            Console.ReadLine();
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
