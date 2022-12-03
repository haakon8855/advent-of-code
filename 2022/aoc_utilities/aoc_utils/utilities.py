"""haakon8855"""


class Utilities:
    """
    Utilities class containing reusable code across each day.
    """

    @staticmethod
    def read_data(path: str) -> str:
        """
        Reads the data from the provided file. Returned as a single string.
        """
        with open(path, 'r', encoding='utf-8') as file:
            return file.read()

    @staticmethod
    def print_outputs(first_test_out: str, first_out: str,
                      second_test_out: str, second_out: str):
        """
        Prints the four given inputs in the default format.
        """
        output = '=' * 80 + '\n'
        output += f'TEST1: {first_test_out}\n'
        output += f'REAL1: {first_out}\n\n'
        output += f'TEST2: {second_test_out}\n'
        output += f'REAL2: {second_out}\n'
        output += '=' * 80
        print(output)
