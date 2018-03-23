using System.Collections.Generic;

namespace CC_Lib.Structures
{
    class WeightedNode<T>
    {
        public WeightedNode() : this(default(T))
        {
        }

        public WeightedNode(T value) : this(value, new List<(Node<T> node, double weight)>())
        {
        }

        public WeightedNode(T value, List<(Node<T> node, double weight)> neighbors)
        {
            Neighbors = neighbors;
            Value = value;
        }

        public List<(Node<T> node, double weight)> Neighbors { get; set; }
        public T Value { get; set; }
    }
}
