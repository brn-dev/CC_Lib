import math

from .point2d import Point2f, Point2i

FULL_ROTATION_RADIANS = 2 * math.pi

class DirectionalMover:

    position: Point2f
    # direction in radians
    direction: float

    def __init__(self, position: Point2f = Point2f(0, 0), direction=0.0):
        self.position = position
        self.direction = direction

    def __str__(self):
        return f'{self.position}#{self.direction}'

    def __repr__(self):
        return self.__str__()

    def turn(self, radians: float):
        self.direction = (self.direction + radians) % FULL_ROTATION_RADIANS

    def step(self, step_width: float = 1.0):
        self.position += Point2f(math.cos(self.direction) * step_width, math.sin(self.direction) * step_width)


class GridDirectionalMover:

    position: Point2i
    direction: tuple[int, int]

    def __init__(self):
        self.position = Point2i(0, 0)
        self.direction = (1, 0)

    def __str__(self):
        return f'{self.position}#{self.direction}'

    def __repr__(self):
        return self.__str__()

    def turn_clockwise(self):
        self.direction = (self.direction[1], -self.direction[0])

    def turn_counter_clockwise(self):
        self.direction = (-self.direction[1], self.direction[0])

    def turn_flip(self):
        self.direction = (-self.direction[0], -self.direction[1])

    def step(self, step_width: int = 1):
        self.position += Point2i(self.direction[0] * step_width, self.direction[1] * step_width)
