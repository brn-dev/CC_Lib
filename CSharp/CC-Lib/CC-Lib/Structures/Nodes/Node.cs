using System.Collections.Generic;

namespace CC_Lib.Structures.Nodes
{
    public class Node<T> : Identifiable
    {
        // ReSharper disable once InconsistentNaming
        protected ISet<Node<T>> _children = new HashSet<Node<T>>();

        public virtual IEnumerable<Node<T>> Children => _children;

        public T Value { get; set; }

        public Node(int id = -1) : this(default(T), id)
        {
        }

        public Node(T value, int id = -1) : base(id)
        {
            Value = value;
        }

        public virtual void AddConnection(Node<T> item)
        {
            _children.Add(item);
        }

        public virtual void RemoveConnection(Node<T> item)
        {
            _children.Remove(item);
        }

        public virtual WeightedNode<T> ToWeightedNode()
        {
            var weightedNode = new WeightedNode<T>(Value, Id);
            return weightedNode;
        }
    }
}
