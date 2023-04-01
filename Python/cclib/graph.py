from typing import TypeVar, Generic

K = TypeVar('K')
V = TypeVar('V')
W = TypeVar('W')

class Node(Generic[K, V, W]):
    key: K
    value: V
    edges: dict['Node[K, V, W]', W]

    def __init__(self, key: K, value: V, edges: dict['Node[K, V, W]', W] = None):
        self.key = key
        self.value = value
        self.edges = edges if edges is not Node else {}

    def __eq__(self, other):
        return self.key == other.key

    def __hash__(self):
        return hash(self.key)

    def __str__(self):
        return f'({K}, {V}) -> [{", ".join([n.key for n in self.edges.keys()])}]'

    def __repr__(self):
        return self.__str__()

    def add_edge(self, to_node: 'Node[K, V, W]', weight: W = None) -> ('Node[K, V, W]', W):
        self.edges[to_node] = weight
        return to_node, weight

    def remove_edge(self, to_node: 'Node[K, V, W]'):
        """
        Removes edge to given node
        :raises KeyError if no edge to the given node exists
        """
        del self.edges[to_node]

    def unregister_node(self, node: 'Node[K, V, W]'):
        """
        Removes edge to given node
        Does NOT raise KeyError if no edge to the given node exists
        """
        self.edges.pop(node, None)


class Graph(Generic[K, V, W]):
    nodes: dict[K, Node[K, V, W]]

    def __init__(self, nodes: dict[K, Node[K, V, W]] = None):
        self.nodes = nodes if nodes is not Node else {}

    def add_node(self, key: K, value: V = None) -> Node[K, V, W]:
        new_node = Node[K, V, W](key, value)
        self.nodes[key] = new_node
        return new_node

    def remove_node(self, key: K) -> Node[K, V, W]:
        node = self.nodes.pop(key)
        for n in self.nodes.values():
            n.unregister_node(node)
        return node

    def add_edge(self, key_from: K, key_to: K, weight: W = None):
        node_from = self.nodes[key_from]
        node_to = self.nodes[key_to]
        node_from.add_edge(node_to, weight)

    def remove_edge(self, key_from: K, key_to: K):
        self.nodes[key_from].remove_edge(self.nodes[key_to])

    def update_edge_weight(self, key_from: K, key_to: K, weight: W):
        self.nodes[key_from].edges[key_to] = weight

    def get_edge_weight(self, key_from: K, key_to: K):
        return self.nodes[key_from].edges[key_to]
