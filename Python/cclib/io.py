import os
from collections import Callable
from pathlib import Path
from typing import Any


def apply_on_input_files(f: Callable[[list[str]], list[str]], input_folder_path: str, output_folder_path: str):
    for input_file_path in os.listdir(input_folder_path):
        input_file_path: Path = Path(os.path.join(input_folder_path, input_file_path))
        with open(input_file_path, 'r') as input_file:
            input_lines = input_file.readlines()
        output_lines = f(input_lines)
        output_file_path = os.path.join(output_folder_path, os.path.basename(input_file_path.with_suffix('.out')))
        with open(output_file_path, 'w') as output_file:
            output_file.writelines([output_line + '\n' for output_line in output_lines])


def apply_on_console(f: Callable[[str], str]):
    while True:
        input_text = input('> ')
        output_text = f(input_text)
        print(output_text)
        print('\n')


def split_and_convert(line: str, conversion_methods: list[Callable[[str], Any]], sep=' ') -> list[Any]:
    return [
        conversion_method(token)
        for token, conversion_method
        in zip(line.split(sep=sep), conversion_methods)
    ]
