import queue
import threading
from typing import Callable, Any, TypeVar, Generic, Optional

INPUT_TYPE = TypeVar('INPUT_TYPE')
OUTPUT_TYPE = TypeVar('OUTPUT_TYPE')

class ParallelSolver(Generic[INPUT_TYPE, OUTPUT_TYPE]):

    print_status: bool

    task_queue: queue.Queue[tuple[int, INPUT_TYPE]]
    threads: list[threading.Thread]

    func: Callable[[INPUT_TYPE], OUTPUT_TYPE]
    outputs: list[Optional[OUTPUT_TYPE]]

    def __print(self, msg: str):
        if self.print_status:
            print(msg)

    def __worker(self):
        worker_id = threading.currentThread().name
        while True:
            i, task = self.task_queue.get()
            if task is None:
                self.__print(f'{worker_id} stopping since None was received through task-queue')
                break
            self.__print(f'{worker_id}: Starting task {i}')
            self.outputs[i] = self.func(task)
            self.task_queue.task_done()
            self.__print(f'{worker_id}: Finished task {i}')

    def __start_workers(self, num_workers):
        if self.threads:
            raise ValueError(f'No workers must be running!')
        self.__print(f'Staring {num_workers} workers')
        for i in range(num_workers):
            t = threading.Thread(target=self.__worker, name=f'Worker-{i}')
            t.start()
            self.threads.append(t)
        self.__print(f'Workers started')

    def __stop_workers(self):
        self.__print('Stopping workers')
        for _ in self.threads:
            self.task_queue.put((-1, None))
        for t in self.threads:
            t.join()
        self.__print('Workers stopped')

    def execute(
            self,
            func: Callable[[INPUT_TYPE], OUTPUT_TYPE],
            inputs: list[INPUT_TYPE],
            num_workers=16,
            print_status=False,
    ) -> list[OUTPUT_TYPE]:
        self.print_status = print_status

        self.task_queue = queue.Queue()
        self.threads = []

        self.func = func
        self.outputs = [None] * len(inputs)

        self.__start_workers(num_workers)

        for i, input_line in enumerate(inputs):
            self.task_queue.put((i, input_line))

        self.task_queue.join()
        self.__stop_workers()

        return self.outputs



