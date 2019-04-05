using System;

namespace CC_Lib.Structures.Graphs
{
    public class IdGraph<T> : SimpleGraph<int, T>
    {
        private int _nextId;

        public override GraphNode<int, T> AddNode(GraphNode<int, T> node)
        {
            throw new NotSupportedException();
        }

        public override GraphNode<int, T> AddNode(int key, T value = default(T))
        {
            throw new NotSupportedException();
        }

        public GraphNode<int, T> AddNode(T value = default(T))
        {
            var node = new GraphNode<int, T>(_nextId, value);
            _nextId++;
            return base.AddNode(node);
        }
    }
}
