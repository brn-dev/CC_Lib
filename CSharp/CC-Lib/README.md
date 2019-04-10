# _C\#_

# Algorithms
## GraphAlgorithms
### A*
https://en.wikipedia.org/wiki/A*_search_algorithm
### Dijkstra
https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm

# ExtensionMethods
### EnumerableExtensions
- SubArray
- HeadTail
- MakeChunks
- MinBy
- MaxBy

Iterate:
- ForEach
- Enumerate

### GraphExtensions
- Visualize
- VisualizeToConsole

### MatrixExtensions
- GetColumn, GetRow
- GetAt
- Copy
- MirrorVertically, MirrorHorizontally
- RotateClockwise, RotateCounterClockwise, Rotate180, RotateClockwiseTimes
- ToFlat, ToNested, To2DArray
- ForEach
- Enumerate
- All
- AssignEach
- Visualize, VisualizeToConsole
- Equals

### StringExtensions
Parse:
- ParseDyn
- ParseStringDyn
- ParseAndDeconstruct

Tokenize:
- ToTokens
- ToIntTokens
- ToDoubleTokens

# Structures
## Collections
### PriorityQueue
A queue which orders its elements by a given priority.

## Geometry2D
### Vector2
Represents a vector in two dimensional space.
### DirectionMover
DirectionMover simulates an etitiy, which can move in 2D space. 

## Geometry3D
### Vector3
Represents a vector in three dimensional space.

## Graphs
### GraphNode
Node used by graphs.
### IReadableGraph
Interface which defines method to read a graph.
### Graph
Abstract class which defines methods to manipulate a graph.
### SimpleGraph
Standard implementation of a graph.
### IdGraph
A graph which automatically assigns Ids to its nodes.
### CoordinateGraph
A graph which represents a two dimensional coordinate plane.

## Nodes
### Node
Node with children.
### WeightedNode
Node with children and weighted edges.
### BiDirectionalNode.
Node which automatically adds itself as a child to the node which has been added as a child.
### BiDirectionalWeightedNode
BiDirectionalNode but with weighted edges.
### BinaryNode
Node with two children.

## Trees
### BinaryTree
Standard implementation of a binary tree.

# Utils
### InputOutputUtils
Methods to handle input and output, handling files and console.
