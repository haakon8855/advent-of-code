"""haakon8855"""

from aoc_utils.utilities import Utilities


def solution_first_task(data: str, unique_len: int = 4) -> int:
    for i in range(0, len(data) - unique_len):
        # For each substring of length unique_len in data
        if len(set(data[i:i + unique_len])) == unique_len:
            # If the set of unique characters in the substring is of length 4
            # Return the (1-indexed) index of the last char in the substring
            return i + unique_len
    return -1


def solution_second_task(data: str) -> int:
    return solution_first_task(data, unique_len=14)


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/06/data/test_data.txt")
    DATA = Utilities.read_data("2022/06/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
