"""haakon8855"""

from aoc_utils.utilities import Utilities


def get_priority(letter: str) -> int:
    # make lower case letters a-z have values 1-26
    priority = ord(letter) - 96
    # make upper case letters A-Z have values 27-52
    if priority <= 0:
        return priority + 58
    return priority


def solution_first_task(data: str) -> int:
    sum_priority = 0
    sacks = data.split('\n')
    for sack in sacks:
        # Get first and second compartment (first and second half of string)
        first, second = sack[:int(len(sack) / 2)], sack[int(len(sack) / 2):]
        # Create sets of each half and get the intersection to see which
        # letter is in both (only one according to problem description).
        overlap = set(first).intersection(set(second))
        sum_priority += get_priority(overlap.pop())
    return sum_priority


def solution_second_task(data: str) -> int:
    sacks = data.split('\n')
    sum_badges = 0
    for i in range(0, len(sacks), 3):
        first = set(sacks[i])
        second = set(sacks[i + 1])
        third = set(sacks[i + 2])
        group_badge = first.intersection(second).intersection(third).pop()
        sum_badges += get_priority(group_badge)
    return sum_badges


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/03/data/test_data.txt")
    DATA = Utilities.read_data("2022/03/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
