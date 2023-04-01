from typing import TypeVar, Generic, Optional

T = TypeVar('T')

class BinaryNode(Generic[T]):
    parent: Optional['BinaryNode[T]'] = None

    left: Optional['BinaryNode[T]'] = None
    right: Optional['BinaryNode[T]'] = None

    value: Optional[T]

    def __init__(
            self,
             parent: Optional['BinaryNode[T]'],
             left: Optional['BinaryNode[T]'],
             right: Optional['BinaryNode[T]'],
             value: Optional[T]
    ):
        self.parent = parent
        self.left = left
        self.right = right
        self.value = value


def get_children_layer(root: BinaryNode[T], depth: int):
    layer = [root]
    for _ in range(depth):
        new_layer = []
        for node in layer:
            if node.left:
                new_layer.append(node.left)
            if node.right:
                new_layer.append(node.right)
        layer = new_layer
    return layer
