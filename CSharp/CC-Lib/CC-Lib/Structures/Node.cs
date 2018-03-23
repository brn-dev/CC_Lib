using System.Collections.Generic;

namespace CC_Lib.Structures
{
    class Node<T>
    {
        public Node() : this(default(T))
        {
        }

        public Node(T value) : this(value, new List<Node<T>>())
        {
        }

        public Node(T value, List<Node<T>> neighbors)
        {
            Neighbors = neighbors;
            Value = value;
        }

        public List<Node<T>> Neighbors { get; set; }
        public T Value { get; set; }
    }
}
