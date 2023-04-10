import math
import time

from cclib.parallel import ParallelSolver

def func(s: str) -> str:
    result = math.factorial(int(s))
    time.sleep(1)
    return str(result)

def main():
    inputs = [str(i) for i in range(100)]

    solver = ParallelSolver()
    outputs = solver.execute(func, inputs, num_workers=50, print_status=True)
    print(outputs)



if __name__ == '__main__':
    main()
