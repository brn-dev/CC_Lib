import math
from dataclasses import dataclass
from .geom2d_utils import GRID_DIRECTIONS_INCL_DIAGONAL, GRID_DIRECTIONS_EXCL_DIAGONAL

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
        directions = GRID_DIRECTIONS_INCL_DIAGONAL if diagonal else GRID_DIRECTIONS_EXCL_DIAGONAL
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
