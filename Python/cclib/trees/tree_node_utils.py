from typing import TypeVar, Sequence

from .abstract_tree_node import AbstractTreeNode

T = TypeVar('T')

def get_descendant_layer(root: AbstractTreeNode[T], depth: int) -> Sequence[AbstractTreeNode[T]]:
    layer = (root,)
    for _ in range(depth):
        layer = (
            child
            for node in layer
            for child in node.children
        )
    return tuple(layer)
