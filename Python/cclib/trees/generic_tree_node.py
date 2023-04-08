from typing import TypeVar, Optional, Sequence
from .abstract_tree_node import AbstractTreeNode

T = TypeVar('T')

class GenericTreeNode(AbstractTreeNode[T]):

    _value: T
    _parent: Optional['GenericTreeNode[T]']
    _children: list['GenericTreeNode[T]']

    def __init__(
            self,
            value: T,
            parent: Optional['GenericTreeNode[T]'] = None,
            children: Sequence['GenericTreeNode'] = None
    ):
        self._value = value
        self._parent = parent
        if children is not None:
            self._children = list(children)
        else:
            self._children = []

    @property
    def value(self) -> T:
        return self._value

    @value.setter
    def value(self, val: T):
        self._value = val

    @property
    def parent(self) -> Optional['GenericTreeNode[T]']:
        return self._parent

    @parent.setter
    def parent(self, p: Optional['GenericTreeNode[T]']):
        self._parent = p

    @property
    def children(self) -> Sequence['GenericTreeNode[T]']:
        return self._children

    def add_child(self, n: 'GenericTreeNode[T]'):
        self._children.append(n)
        n.parent = self

    def remove_child(self, n: 'GenericTreeNode'):
        self._children.remove(n)
        n.parent = None

