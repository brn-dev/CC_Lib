namespace CC_Lib.Structures.Collections.Graphs.Nodes
{
    class BinaryNode<T>
    {
        public BinaryNode() : this(default(T))
        {
        }

        public BinaryNode(T value) : this(value, null)
        {
        }

        public BinaryNode(T value, BinaryNode<T> parent) : this(value, parent, null, null)
        {
        }

        public BinaryNode(T value, BinaryNode<T> parent, BinaryNode<T> leftChild, BinaryNode<T> rightChild)
        {
            Parent = parent;
            LeftChild = leftChild;
            RightChild = rightChild;
            Value = value;
        }

        public BinaryNode<T> Parent { get; set; }
        public BinaryNode<T> LeftChild { get; set; }
        public BinaryNode<T> RightChild { get; set; }
        public T Value { get; set; }
    }
}
