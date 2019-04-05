using CC_Lib.Structures.Geometry2D;
using System.Collections.Generic;
using System.Linq;

namespace CC_Lib.Structures.Graphs
{
    public class CoordinateGraph : IReadOnlyGraph<Vector2, double>
    {
        public int Width { get; }
        public int Height { get; }
        public bool DiagonalConnections { get; }
        public IEnumerable<Vector2> DisabledCoordinates { get; }

        private readonly Vector2 _boundVector;

        private readonly IDictionary<Vector2, GraphNode<Vector2, double>> _nodes =
            new Dictionary<Vector2, GraphNode<Vector2, double>>();

        public IEnumerable<GraphNode<Vector2, double>> Nodes => _nodes.Values;

        private readonly Dictionary<GraphNode<Vector2, double>, IReadOnlyDictionary<GraphNode<Vector2, double>, double>>
            _connectionsOf
                = new Dictionary<GraphNode<Vector2, double>, IReadOnlyDictionary<GraphNode<Vector2, double>, double>>();

        public IReadOnlyDictionary<GraphNode<Vector2, double>, IReadOnlyDictionary<GraphNode<Vector2, double>, double>>
            ConnectionsOf => _connectionsOf;

        public CoordinateGraph(int width, int height, bool diagonalConnections = false)
            : this(width, height, new List<Vector2>(), diagonalConnections)
        {

        }

        public CoordinateGraph(int width, int height, IEnumerable<Vector2> disabledCoordinates,
            bool diagonalConnections = false)
        {
            Height = height;
            Width = width;
            DiagonalConnections = diagonalConnections;
            DisabledCoordinates = disabledCoordinates;
            _boundVector = new Vector2(Width, Height);
            MakeNodes();
            MakeConnections();
        }

        public GraphNode<Vector2, double> this[double x, double y] => this[new Vector2(x, y)];

        public GraphNode<Vector2, double> this[Vector2 key] => _nodes[key];

        private void MakeNodes()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var coordinate = new Vector2(x, y);
                    if (DisabledCoordinates.Contains(coordinate)) continue;
                    // Could the value of the GraphNode be used for something?
                    var node = new GraphNode<Vector2, double>(coordinate, 0);
                    _nodes.Add(coordinate, node);
                    _connectionsOf[node] = new Dictionary<GraphNode<Vector2, double>, double>();
                }
            }
        }

        private void MakeConnections()
        {
            foreach (var node in _nodes.Values)
            {
                var coordinate = node.Key;
                var (x, y) = coordinate;

                var neighbors = new List<Vector2>
                {
                    new Vector2(x, y + 1),
                    new Vector2(x, y - 1),
                    new Vector2(x + 1, y),
                    new Vector2(x - 1, y)
                };

                if (DiagonalConnections)
                {
                    neighbors.AddRange(new List<Vector2>
                    {
                        new Vector2(x + 1, y + 1),
                        new Vector2(x - 1, y - 1),
                        new Vector2(x + 1, y - 1),
                        new Vector2(x - 1, y + 1)
                    });
                }

                neighbors.ForEach(neighbor => CheckAndAddConnection(coordinate, neighbor));
            }
        }

        private void CheckAndAddConnection(Vector2 from, Vector2 to)
        {
            if (!to.IsInBounds(_boundVector))
            {
                return;
            }

            var toNode = this[to];
            if (toNode is null)
            {
                return;
            }

            (_connectionsOf[this[from]] as IDictionary<GraphNode<Vector2, double>, double>)?.Add(toNode, 1);
        }
    }
}
