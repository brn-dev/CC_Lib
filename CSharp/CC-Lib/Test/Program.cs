using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CC_Lib;
using CC_Lib.Structures;

namespace Test
{
    class Program
    {
        private static void Main()
        {
            InputOutput.ExecFileInputWithOutputFile(@"C:\Users\Brn\Desktop\testInput.txt", @"C:\Users\Brn\Desktop\testOutput.txt", DoSmth);
        }

        public static string DoSmth(string strings)
        {
            return strings.Split(' ').Aggregate((aggregate, curr) => aggregate + "|" + curr);
        }
    }
}
