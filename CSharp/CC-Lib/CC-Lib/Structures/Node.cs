using System.Collections.Generic;

namespace CC_Lib.Structures
{
    class Node<T> : Identifiable
    {
        public Node(int id = -1) : this(default(T))
        {
        }

        public Node(T value, int id = -1) : this(value, new List<Node<T>>())
        {
        }

        public Node(T value, List<Node<T>> neighbors, int id = -1) : base(id)
        {
            Neighbors = neighbors;
            Value = value;
        }

        public List<Node<T>> Neighbors { get; set; }
        public T Value { get; set; }
    }
}
