from cclib.io import apply_on_console

def func(input_str: str) -> str:
    return input_str * 2

def main():
    apply_on_console(func)

if __name__ == '__main__':
    main()
