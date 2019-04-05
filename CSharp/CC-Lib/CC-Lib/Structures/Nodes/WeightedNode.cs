using System.Collections.Generic;
using System.Linq;
using CC_Lib.ExtensionMethods;

namespace CC_Lib.Structures.Nodes
{
    public class WeightedNode<T> : Identifiable
    {
        public T Value { get; set; }

        private readonly ISet<(WeightedNode<T> node, double weight)> _connections = new HashSet<(WeightedNode<T> node, double weight)>();

        public IEnumerable<(WeightedNode<T> node, double weight)> Connections => _connections;


        public WeightedNode(int id = -1) : this(default(T), id)
        {
        }

        public WeightedNode(T value, int id = -1) : base(id)
        {
            Value = value;
        }

        public virtual void AddConnection((WeightedNode<T> node, double weight) connection)
        {
            _connections.Add(connection);
        }

        public void AddConnection(WeightedNode<T> node, double weight)
        {
            AddConnection((node, weight));
        }

        public virtual void RemoveConnection((WeightedNode<T> node, double weight) connection)
        {
            _connections.Remove(connection);
        }

        public void RemoveConnection(WeightedNode<T> node, double weight)
        {
            RemoveConnection((node, weight));
        }

        public void RemoveAllConnectionsToNode(WeightedNode<T> node)
        {
            _connections.Where(x => x.node == node).ForEach(RemoveConnection);
        }

        public void ClearConnections()
        {
            _connections.Clear();
        }

        public virtual Node<T> ToSimpleNode()
        {
            var node = new Node<T>(Value, Id);
            return node;
        }


    }
}
