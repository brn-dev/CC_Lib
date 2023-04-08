import math

from cclib.geom2d.directional_mover import GridDirectionalMover

def main():
    mover = GridDirectionalMover()
    print(mover)
    for i in range(4):
        mover.turn_counter_clockwise()
        print(mover)
    for i in range(4):
        mover.turn_clockwise()
        print(mover)
    mover.turn_counter_clockwise()
    for i in range(2):
        mover.turn_flip()
        print(mover)


if __name__ == '__main__':
    main()
