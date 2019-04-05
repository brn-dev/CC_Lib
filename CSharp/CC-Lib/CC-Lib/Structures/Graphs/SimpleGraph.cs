using CC_Lib.Structures.Collections;
using System;
using System.Collections.Generic;

namespace CC_Lib.Structures.Graphs
{

    public class SimpleGraph<TK, TV> : Graph<TK, TV>
        where TK : IEquatable<TK>
    {
        private readonly IDictionary<TK, GraphNode<TK, TV>> _nodes = new Dictionary<TK, GraphNode<TK, TV>>();

        private readonly Dictionary<GraphNode<TK, TV>, IReadOnlyDictionary<GraphNode<TK, TV>, double>> _connectionsOf 
            = new Dictionary<GraphNode<TK, TV>, IReadOnlyDictionary<GraphNode<TK, TV>, double>>();

        public override IEnumerable<GraphNode<TK, TV>> Nodes => _nodes.Values;

        public override IReadOnlyDictionary<GraphNode<TK, TV>, IReadOnlyDictionary<GraphNode<TK, TV>, double>> ConnectionsOf => _connectionsOf;

        /// <inheritdoc />
        /// <see cref="T:CC_Lib.Structures.Graphs.Graph`2" />
        public SimpleGraph(GraphDirectionalMode directionalMode = GraphDirectionalMode.BiDirectional, double defaultWeight = 1) 
            : base(directionalMode, defaultWeight)
        { 
        }

        public override GraphNode<TK, TV> AddNode(GraphNode<TK, TV> node)
        {
            if (_nodes.ContainsKey(node.Key))
            {
                throw new KeyExistsException(node.Key);
            }

            _nodes[node.Key] = node;
            _connectionsOf[node] = new Dictionary<GraphNode<TK, TV>, double>();
            return node;
        }

        public override void RemoveNode(TK key)
        {
            _nodes.Remove(key);
            _connectionsOf.Remove(this[key]);
        }

        public override void AddConnection(TK from, TK to, double weight)
        {
            (_connectionsOf[this[from]] as IDictionary<GraphNode<TK, TV>, double>)?.Add(this[to], weight);
            if (DirectionalMode == GraphDirectionalMode.BiDirectional)
            {
                (_connectionsOf[this[to]] as IDictionary<GraphNode<TK, TV>, double>)?.Add(this[from], weight);
            }
        }

        public override void RemoveConnection(TK from, TK to)
        {
            (_connectionsOf[this[from]] as IDictionary<GraphNode<TK, TV>, double>)?.Remove(this[to]);
            if (DirectionalMode == GraphDirectionalMode.BiDirectional)
            {
                (_connectionsOf[this[to]] as IDictionary<GraphNode<TK, TV>, double>)?.Remove(this[from]);
            }
        }

        public override void UpdateConnectionWeight(TK from, TK to, double newWeight)
        {
            if (!(_connectionsOf[this[from]] is IDictionary<GraphNode<TK, TV>, double> fromDict))
            {
                return;
            }
            fromDict[this[to]] = newWeight;
            if (DirectionalMode != GraphDirectionalMode.BiDirectional)
            {
                return;
            }

            if (!(_connectionsOf[this[to]] is IDictionary<GraphNode<TK, TV>, double> toDict))
            {
                return;
            }
            toDict[this[from]] = newWeight;
        }

        public override GraphNode<TK, TV> this[TK key] => _nodes[key];
    }
}
