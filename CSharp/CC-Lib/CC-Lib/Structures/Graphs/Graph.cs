using System;
using System.Collections.Generic;

namespace CC_Lib.Structures.Graphs
{
    public abstract class Graph<TK, TV> : IReadOnlyGraph<TK, TV>
        where TK : IEquatable<TK>
    {
        public double DefaultWeight { get; set; }

        public GraphDirectionalMode DirectionalMode { get; }

        public abstract IEnumerable<GraphNode<TK, TV>> Nodes { get; }

        public abstract IReadOnlyDictionary<GraphNode<TK, TV>, IReadOnlyDictionary<GraphNode<TK, TV>, double>> ConnectionsOf { get; }

        public abstract GraphNode<TK, TV> AddNode(GraphNode<TK, TV> node);

        public abstract void RemoveNode(TK key);

        public abstract void AddConnection(TK from, TK to, double weight);

        public abstract void RemoveConnection(TK from, TK to);

        public abstract void UpdateConnectionWeight(TK from, TK to, double newWeight);

        public abstract GraphNode<TK, TV> this[TK key] { get; }

        /// <summary>
        /// Creates a graph.
        /// The edges are bidirectional by default - meaning that when adding a connection, the connection will be added to both nodes.
        /// </summary>
        /// <param name="directionalMode">Determines whether the edges are unidirectional or bidirectional</param>
        /// <param name="defaultWeight">The default weight if the weight isn't given when adding connections</param>
        protected Graph(GraphDirectionalMode directionalMode = GraphDirectionalMode.BiDirectional, double defaultWeight = 1)
        {
            DirectionalMode = directionalMode;
            DefaultWeight = defaultWeight;
        }

        public virtual GraphNode<TK, TV> AddNode(TK key, TV value = default(TV))
        {
            var node = new GraphNode<TK, TV>(key, value);
            return AddNode(node);
        }

        public virtual void RemoveNode(GraphNode<TK, TV> node)
        {
            RemoveNode(node.Key);
        }

        public virtual void AddConnection(TK from, TK to)
        {
            AddConnection(from, to, DefaultWeight);
        }

        public virtual void RemoveConnection(GraphNode<TK, TV> from, GraphNode<TK, TV> to)
        {
            RemoveConnection(from.Key, to.Key);
        }

        public virtual void UpdateConnectionWeight(GraphNode<TK, TV> from, GraphNode<TK, TV> to, double newWeight)
        {
            UpdateConnectionWeight(from.Key, to.Key, newWeight);
        }
    } 
}
