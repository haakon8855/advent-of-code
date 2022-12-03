"""haakon8855"""

from aoc_utils.utilities import Utilities


def most_calories(data: str) -> int:
    """
    Returns the number of calories the elf with the most calories carries.
    """
    raw_list = data.split('\n\n')
    highest = 0
    for elf in raw_list:
        elf_calories = sum(list(map(int, elf.split('\n'))))
        highest = max(highest, elf_calories)
    return highest


def top_n_most_calories(data: str, num: int = 3) -> int:
    """
    Returns the number of calories the elf with the most calories carries.
    """
    raw_list = data.split('\n\n')
    highest = [0] * num
    for elf in raw_list:
        elf_calories = sum(list(map(int, elf.split('\n'))))
        lowest_highest = min(highest)
        index = highest.index(lowest_highest)
        highest[index] = max(lowest_highest, elf_calories)
    return sum(highest)


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/01/data/test_data.txt")
    DATA = Utilities.read_data("2022/01/data/data.txt")

    Utilities.print_outputs(most_calories(TEST_DATA), most_calories(DATA),
                            top_n_most_calories(TEST_DATA),
                            top_n_most_calories(DATA))
