namespace CC_Lib.Structures.Nodes
{
    class BiDirectionalWeightedNode<T> : WeightedNode<T>
    {
        public BiDirectionalWeightedNode(int id = -1) : this(default(T), id)
        {

        }

        public BiDirectionalWeightedNode(T value, int id = -1) : base(value, id)
        {

        }

        public override void AddConnection((WeightedNode<T> node, double weight) connection)
        {
            base.AddConnection(connection);
            connection.node.AddConnection(this, connection.weight);
        }

        public override void RemoveConnection((WeightedNode<T> node, double weight) connection)
        {
            base.RemoveConnection(connection);
            connection.node.RemoveConnection(this, connection.weight);
        }

        public override Node<T> ToSimpleNode()
        {
            var node = new BiDirectionalNode<T>(Value, Id);
            return node;
        }
    }
}
