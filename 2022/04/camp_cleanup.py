"""haakon8855"""

import numpy as np
from aoc_utils.utilities import Utilities


def solution_first_task(data: str) -> int:
    # Split on lines (now one pair in each element)
    pairs = data.split('\n')
    # Split on comma, (now each element in the pair contains one elf)
    pairs = [pair.split(',') for pair in pairs]
    # Split on dash (shape=(n, 2, 2))
    pairs = [[elf.split('-') for elf in pair] for pair in pairs]
    # Convert to int
    pairs = np.array(pairs).astype(np.int32)
    # Flatten each element in the first dimension
    pairs = pairs.reshape(pairs.shape[0], -1)

    def fully_contains(pair: np.array) -> bool:
        # Make sets for each elf's range
        elf_1 = set(range(pair[0], pair[1] + 1))
        elf_2 = set(range(pair[2], pair[3] + 1))
        # Check if one of the sets is a subset of the other
        return elf_1.issubset(elf_2) or elf_2.issubset(elf_1)

    return sum(np.apply_along_axis(fully_contains, 1, pairs))


def solution_second_task(data: str) -> int:
    # Split on lines (now one pair in each element)
    pairs = data.split('\n')
    # Split on comma, (now each element in the pair contains one elf)
    pairs = [pair.split(',') for pair in pairs]
    # Split on dash (shape=(n, 2, 2))
    pairs = [[elf.split('-') for elf in pair] for pair in pairs]
    # Convert to int
    pairs = np.array(pairs).astype(np.int32)
    # Flatten each element in the first dimension
    pairs = pairs.reshape(pairs.shape[0], -1)

    def overlaps(pair: np.array) -> bool:
        # Make sets for each elf's range
        elf_1 = set(range(pair[0], pair[1] + 1))
        elf_2 = set(range(pair[2], pair[3] + 1))
        # Check if there is an overlap between the sets
        return not elf_1.isdisjoint(elf_2)

    return sum(np.apply_along_axis(overlaps, 1, pairs))


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/04/data/test_data.txt")
    DATA = Utilities.read_data("2022/04/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
