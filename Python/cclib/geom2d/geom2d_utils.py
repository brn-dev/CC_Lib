import math
from typing import Sequence, overload, Union

FULL_ROTATION_RADIANS = 2 * math.pi

IntOrFloat = Union[int, float]

GridDirection = tuple[int, int]

GRID_DIRECTIONS_INCL_DIAGONAL: Sequence[GridDirection] = (
    (-1,  1), (0,  1), (1,  1),
    (-1,  0),          (1,  0),
    (-1, -1), (0, -1), (1, -1)
)

GRID_DIRECTIONS_EXCL_DIAGONAL: Sequence[GridDirection] = (
              (0,  1),
    (-1,  0),          (1,  0),
              (0, -1)
)


def get_neighbors(x: int, y: int, diagonal=True) -> list[tuple[int, int]]:
    directions = GRID_DIRECTIONS_INCL_DIAGONAL if diagonal else GRID_DIRECTIONS_EXCL_DIAGONAL
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


def radians_to_degrees(radians: float) -> float:
    return radians * 180 / math.pi


def degrees_to_radians(degrees: float) -> float:
    return degrees * math.pi / 180


def turn_grid_direction_clockwise(direction: GridDirection) -> GridDirection:
    return direction[1], -direction[0]


def turn_grid_direction_counter_clockwise(direction: GridDirection) -> GridDirection:
    return -direction[1], direction[0]


def turn_grid_direction_flip(direction: GridDirection) -> GridDirection:
    return -direction[0], -direction[1]


def turn_radians(original_radians: float, by_radians: float) -> float:
    return (original_radians + by_radians) % FULL_ROTATION_RADIANS
