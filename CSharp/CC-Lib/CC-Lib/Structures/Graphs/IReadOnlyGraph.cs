using System.Collections.Generic;

namespace CC_Lib.Structures.Graphs
{
    public interface IReadOnlyGraph<TK, TV>
    {
        IEnumerable<GraphNode<TK, TV>> Nodes { get; }

        IReadOnlyDictionary<GraphNode<TK, TV>, IReadOnlyDictionary<GraphNode<TK, TV>, double>> ConnectionsOf { get; }

        GraphNode<TK, TV> this[TK key] { get; }
    }
}
