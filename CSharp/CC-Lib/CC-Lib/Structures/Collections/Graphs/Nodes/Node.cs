using System.Collections.Generic;

namespace CC_Lib.Structures.Collections.Graphs.Nodes
{
    class Node<T> : Identifiable
    {
        public Node(int id = -1) : this(default(T), id)
        {
        }

        public Node(T value, int id = -1) : this(value, new List<Node<T>>(), id)
        {
        }

        public Node(T value, IList<Node<T>> children, int id = -1) : base(id)
        {
            Children = children;
            Value = value;
        }

        public IList<Node<T>> Children { get; set; }
        public T Value { get; set; }
    }
}
