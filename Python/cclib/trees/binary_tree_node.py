from typing import TypeVar, Optional, Sequence
from .abstract_tree_node import AbstractTreeNode

T = TypeVar('T')

class BinaryTreeNode(AbstractTreeNode[T]):

    _value: T
    _parent: Optional['BinaryTreeNode[T]'] = None

    _left: Optional['BinaryTreeNode[T]'] = None
    _right: Optional['BinaryTreeNode[T]'] = None

    def __init__(
            self,
            value: Optional[T],
            parent: Optional['BinaryTreeNode[T]'] = None,
            left: Optional['BinaryTreeNode[T]'] = None,
            right: Optional['BinaryTreeNode[T]'] = None,
    ):
        self.parent = parent
        self.left = left
        self.right = right
        self.value = value

    @property
    def value(self) -> T:
        return self._value

    @value.setter
    def value(self, val: T):
        self._value = val

    @property
    def parent(self) -> Optional['BinaryTreeNode[T]']:
        return self._parent

    @parent.setter
    def parent(self, p: Optional['BinaryTreeNode[T]']):
        self._parent = p

    @property
    def left(self):
        return self._left

    @left.setter
    def left(self, n: Optional['BinaryTreeNode[T]']):
        self._left = n
        if n is not None:
            n.parent = self

    @property
    def right(self):
        return self._right

    @right.setter
    def right(self, n: Optional['BinaryTreeNode[T]']):
        self._right = n
        if n is not None:
            n.parent = self

    @property
    def children(self) -> Sequence['AbstractTreeNode[T]']:
        if self._left is not None and self._right is not None:
            return self.left, self.right
        if self._left is not None:
            return self._left,
        if self._right is not None:
            return self._right,
        return ()

