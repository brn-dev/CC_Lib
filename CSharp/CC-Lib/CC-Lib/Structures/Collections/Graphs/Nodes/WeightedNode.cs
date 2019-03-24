using System.Collections.Generic;

namespace CC_Lib.Structures.Collections.Graphs.Nodes
{
    class WeightedNode<T> : Identifiable
    {
        public WeightedNode(int id = -1) : this(default(T), id)
        {
        }

        public WeightedNode(T value, int id = -1) : this(value, new List<(WeightedNode<T> node, double weight)>(), id)
        {
        }

        public WeightedNode(T value, IList<(WeightedNode<T> node, double weight)> children, int id = -1) : base(id)
        {
            Children = children;
            Value = value;
        }

        public IList<(WeightedNode<T> node, double weight)> Children { get; set; }
        public T Value { get; set; }
    }
}
