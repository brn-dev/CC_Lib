using System;
using System.Linq;
using CC_Lib.Structures.Graphs;

namespace CC_Lib.ExtensionMethods
{
    public static class GraphExtensions
    {
        public static string Visualize<TK, TV>(this IReadableGraph<TK, TV> graph)
        {
            var sb = "";

            foreach (var keyValuePair in graph.ConnectionsOf)
            {
                sb += $"{keyValuePair.Key.Key}\n";
                sb = keyValuePair.Value.Aggregate(sb, (current, valuePair) => current + $"    {valuePair.Key}\n");
            }

            return sb;
        }

        public static void VisualizeToConsole<TK, TV>(this IReadableGraph<TK, TV> graph)
        {
            Console.WriteLine(graph.Visualize());
        }
    }
}
