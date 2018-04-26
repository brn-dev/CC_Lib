using System.Collections.Generic;

namespace CC_Lib.Structures
{
    class WeightedNode<T> : Identifiable
    {
        public WeightedNode(int id = -1) : this(default(T), id)
        {
        }

        public WeightedNode(T value, int id = -1) : this(value, new List<(Node<T> node, double weight)>(), id)
        {
        }

        public WeightedNode(T value, List<(Node<T> node, double weight)> neighbors, int id = -1) : base(id)
        {
            Neighbors = neighbors;
            Value = value;
        }

        public List<(Node<T> node, double weight)> Neighbors { get; set; }
        public T Value { get; set; }
    }
}
