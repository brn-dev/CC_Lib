import abc
from typing import Generic, TypeVar, Optional, Sequence

T = TypeVar('T')

class AbstractTreeNode(Generic[T], abc.ABC):

    def __str__(self):
        return f'{self.value} -> [{", ".join([str(c) for c in self.children])}]'

    def __repr__(self):
        return self.__str__()

    @property
    @abc.abstractmethod
    def value(self) -> T:
        ...

    @value.setter
    @abc.abstractmethod
    def value(self, val: T):
        ...

    @property
    @abc.abstractmethod
    def parent(self) -> Optional['AbstractTreeNode[T]']:
        ...

    @parent.setter
    @abc.abstractmethod
    def parent(self, p: Optional['AbstractTreeNode[T]']):
        ...

    @property
    @abc.abstractmethod
    def children(self) -> Sequence['AbstractTreeNode[T]']:
        ...

