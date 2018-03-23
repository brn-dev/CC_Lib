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
            //InputOutput.ExecOnInputDirWithOutputFile(
            //    @"C:\Users\Brn\OneDrive - HTBLVA Wiener Neustadt\NetBeansProjects\School-CCC\level1",
            //    @"C:\Users\Brn\Desktop\output.txt", DoSmth);
            var pq = new PriorityQueue<int, string>();
            pq.Add("3", 3);
            pq.Add("2", 2);
            pq.Add("4", 4);
            pq.Add("1", 1);

            var x = pq[0];
            x = pq[1];
            x = pq[2];
            x = pq[3];

            string test = pq.PollLowest();

            test = pq.PollHighest();
        }

        public static string DoSmth(string[] strings)
        {
            return strings.Aggregate((aggregate, curr) => aggregate + "|" + curr);
        }
    }
}
