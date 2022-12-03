"""haakon8855"""

from aoc_utils.utilities import Utilities


def solution_first_task(data: str) -> int:
    return "Not implemented"


def solution_second_task(data: str) -> int:
    return "Not implemented"


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/template/data/test_data.txt")
    DATA = Utilities.read_data("2022/template/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
