using System;
using System.Collections.Generic;
using System.Linq;
using CC_Lib.ExtensionMethods;
using CC_Lib.Structures.Graphs;
using static System.Double;

namespace CC_Lib.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public delegate double HeuristicFunction<TK, TV>(IReadableGraph<TK, TV> graph, GraphNode<TK, TV> from, GraphNode<TK, TV> to)
            where TK : IEquatable<TK>;

        private static IEnumerable<GraphNode<TK, TV>> ReconstructPath<TK, TV>(
            Dictionary<GraphNode<TK, TV>, GraphNode<TK, TV>> cameFrom, 
            GraphNode<TK, TV> current
            )
            where TK : IEquatable<TK>
        {
            var totalPath = new List<GraphNode<TK, TV>> {current};
            while (cameFrom.Keys.Contains(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }

            return totalPath;
        }

        public static IEnumerable<GraphNode<TK, TV>> AStar<TK, TV>(
            this IReadableGraph<TK, TV> graph, 
            TK from, 
            TK to, 
            HeuristicFunction<TK, TV> heuristicFunction
            )
            where TK : IEquatable<TK>
        {
            var fromNode = graph[from];
            var toNode = graph[to];

            var closedSet = new HashSet<GraphNode<TK, TV>>();
            var openSet = new HashSet<GraphNode<TK, TV>> {fromNode};
            var cameFrom = new Dictionary<GraphNode<TK, TV>, GraphNode<TK, TV>>();
            var gScore = new Dictionary<GraphNode<TK, TV>, double>();
            var fScore = new Dictionary<GraphNode<TK, TV>, double>();

            foreach (var node in graph.Nodes)
            {
                cameFrom[node] = null;
                gScore[node] = PositiveInfinity;
                fScore[node] = PositiveInfinity;
            }


            gScore[fromNode] = 0;
            fScore[fromNode] = heuristicFunction(graph, fromNode, toNode);

            while (openSet.Count > 0)
            {
                var current = openSet.First(x => x.Equals(fScore.MinBy(kv => kv.Value).Key));

                if (current.Equals(toNode))
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var connection in graph.ConnectionsOf[current])
                {
                    if (closedSet.Contains(connection.Key))
                    {
                        continue;
                    }

                    var tentativeGScore = gScore[current] + connection.Value;

                    if (!openSet.Contains(connection.Key))
                    {
                        openSet.Add(connection.Key);
                    } 
                    else if (tentativeGScore >= gScore[connection.Key])
                    {
                        continue;
                    }

                    cameFrom[connection.Key] = current;
                    gScore[connection.Key] = tentativeGScore;
                    fScore[connection.Key] = gScore[connection.Key] + heuristicFunction(graph, connection.Key, toNode);
                }
            }

            return new GraphNode<TK, TV>[0];
        }
    }
}
