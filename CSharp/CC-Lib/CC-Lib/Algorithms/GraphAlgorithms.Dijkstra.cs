using System;
using System.Collections.Generic;
using CC_Lib.Structures.Collections;
using CC_Lib.Structures.Graphs;

namespace CC_Lib.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public static IEnumerable<GraphNode<TK, TV>> Dijkstra<TK, TV>(this IReadOnlyGraph<TK, TV> graph, TK from, TK to)
            where TK : IEquatable<TK>
        {
            if (graph[from] is null)
            {
                throw new ArgumentException($"Invalid from key {from}");
            }

            if (graph[to] is null)
            {
                throw new ArgumentException($"Invalid from key {to}");
            }

            var distance = new Dictionary<GraphNode<TK, TV>, double> {[graph[from]] = 0};
            var previous = new Dictionary<GraphNode<TK, TV>, GraphNode<TK, TV>>();

            var queue = new PriorityQueue<double, GraphNode<TK, TV>>();

            foreach (var node in graph.Nodes)
            {
                if (!node.Key.Equals(from))
                {
                    distance[node] = double.MaxValue;
                }

                previous[node] = null;
                queue.Add(node, distance[node]);
            }

            while (queue.Count > 0)
            {
                var node = queue.PollLowest();

                if (node.Equals(to))
                {
                    IList<GraphNode<TK, TV>> list = new List<GraphNode<TK, TV>>();
                    var node = graph[to];
                    while (node != null)
                    {
                        list.Insert(0, node);
                        node = previous[node];
                    }

                    return list;
                }

                foreach (var neighbor in graph.ConnectionsOf[node.Key])
                {
                    var newDistance = distance[node] + neighbor.Value;
                    if (!(newDistance < distance[neighbor.Key.Key])) continue;
                    distance[neighbor.Key.Key] = newDistance;
                    previous[neighbor.Key.Key] = graph[node];
                    queue.UpdatePriority(neighbor.Key.Key, newDistance);
                }
            }

            return null;
        }
    }
}
