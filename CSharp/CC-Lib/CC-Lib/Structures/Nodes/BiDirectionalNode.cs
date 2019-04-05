namespace CC_Lib.Structures.Nodes
{
    public class BiDirectionalNode <T> : Node<T>
    {
        public BiDirectionalNode(int id = -1) : this(default(T), id)
        {
        }

        public BiDirectionalNode(T value, int id = -1) : base(value, id)
        {
        }

        public override void AddConnection(Node<T> item)
        {
            base.AddConnection(item);
            item.AddConnection(this);
        }

        public override void RemoveConnection(Node<T> item)
        {
            base.RemoveConnection(item);
            item.RemoveConnection(this);
        }

        public override WeightedNode<T> ToWeightedNode()
        {
            var weightedNode = new BiDirectionalWeightedNode<T>(Value, Id);
            return weightedNode;
        }
    }
}
