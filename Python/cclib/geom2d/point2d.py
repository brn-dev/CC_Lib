import math
from dataclasses import dataclass
from typing import Union, overload

IntOrFloat = Union[int, float]


GRID_DIRECTIONS_INCL_DIAGONAL = (
    (-1,  1), (0,  1), (1,  1),
    (-1,  0),          (1,  0),
    (-1, -1), (0, -1), (1, -1)
)

GRID_DIRECTIONS_EXCL_DIAGONAL = (
              (0,  1),
    (-1,  0),          (1,  0),
              (0, -1)
)


def get_neighbors(x: int, y: int, diagonal=True) -> list[tuple[int, int]]:
    directions = GRID_DIRECTIONS_INCL_DIAGONAL if diagonal else GRID_DIRECTIONS_INCL_DIAGONAL
    return [
        (x + dx, y + dy)
        for dx, dy
        in directions
    ]

def are_neighboring(x1: int, y1: int, x2: int, y2: int, diagonal=True) -> bool:
    dx_abs = abs(x1 - x2)
    dy_abs = abs(y1 - y2)

    if dx_abs + dy_abs == 1:
        return True

    return diagonal and dx_abs == 1 and dy_abs == 1

def norm(x: IntOrFloat, y: IntOrFloat) -> float:
    return math.sqrt(x ** 2 + y ** 2)

@overload
def square_norm(x: int, y: int) -> int:
    ...
def square_norm(x: float, y: float) -> float:
    return x ** 2 + y ** 2

def distance_between(x1: IntOrFloat, y1: IntOrFloat, x2: IntOrFloat, y2: IntOrFloat) -> float:
    return math.sqrt((x1 - x2) ** 2 + (y1 - y2) ** 2)

@overload
def manhattan_distance_between(x1: int, y1: int, x2: int, y2: int) -> int:
    ...
def manhattan_distance_between(x1: float, y1: float, x2: float, y2: float) -> float:
    return abs(x1 - x2) + abs(y1 - y2)


@dataclass(frozen=True)
class Point2i:
    x: int
    y: int

    def __add__(self, other: 'Point2i'):
        return Point2i(self.x + other.x, self.y + other.y)

    def __sub__(self, other: 'Point2i'):
        return Point2i(self.x - other.x, self.y - other.y)

    def __str__(self):
        return f'({self.x}, {self.y})'

    def __repr__(self):
        return self.__str__()

    def get_neighbors(self, diagonal=True) -> list['Point2i']:
        directions = GRID_DIRECTIONS_INCL_DIAGONAL if diagonal else GRID_DIRECTIONS_INCL_DIAGONAL
        return [
            Point2i(self.x + dx, self.y + dy)
            for dx, dy
            in directions
        ]

    def is_neighbor(self, p: 'Point2i', diagonal=True) -> bool:
        dx_abs = abs(p.x - self.x)
        dy_abs = abs(p.y - self.y)

        if dx_abs + dy_abs == 1:
            return True

        return diagonal and dx_abs == 1 and dy_abs == 1

    def distance_to(self, p: 'Point2i') -> float:
        return math.sqrt((p.x - self.x) ** 2 + (p.y - self.y) ** 2)

    def manhattan_distance_to(self, p: 'Point2i') -> int:
        return abs(p.x - self.x) + abs(p.y - self.y)

    def norm(self) -> float:
        return math.sqrt(self.x ** 2 + self.y ** 2)

    def square_norm(self) -> int:
        return self.x ** 2 + self.y ** 2


@dataclass(frozen=True)
class Point2f:
    x: float
    y: float

    def __add__(self, other: 'Point2f'):
        return Point2f(self.x + other.x, self.y + other.y)

    def __sub__(self, other: 'Point2f'):
        return Point2f(self.x - other.x, self.y - other.y)

    def __str__(self):
        return f'({self.x}, {self.y})'

    def __repr__(self):
        return self.__str__()

    def distance_to(self, p: 'Point2f') -> float:
        return math.sqrt((p.x - self.x) ** 2 + (p.y - self.y) ** 2)

    def manhattan_distance_to(self, p: 'Point2f') -> float:
        return abs(p.x - self.x) + abs(p.y - self.y)

    def norm(self) -> float:
        return math.sqrt(self.x ** 2 + self.y ** 2)

    def square_norm(self) -> float:
        return self.x ** 2 + self.y ** 2
