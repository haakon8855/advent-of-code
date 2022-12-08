"""haakon8855"""

import numpy as np
from aoc_utils.utilities import Utilities


def get_tree_array(data: str) -> np.array:
    num_lines = data.count('\n') + 1  # Number of rows in the forest
    # Remove line breaks
    data = data.replace('\n', '')
    # Add spaces between each character
    data = ' '.join(data)
    # Create numpy array from the numbers
    trees = np.fromstring(data, dtype=int, sep=' ')
    # Reshape the array to a 2d representation
    trees = trees.reshape(num_lines, -1)
    return trees


def get_first_true(array: np.array) -> int:
    """
    Returns first occurence of the value true in the array (1-indexed).
    If no values are true, the length of the array is returned.
    """
    for i in range(len(array)):
        if array[i]:
            return i + 1
    return len(array)


def solution_first_task(data: str) -> int:
    # Get trees
    trees = get_tree_array(data)
    # Array of visible trees (1=visible, 0=hidden)
    visible = np.zeros(shape=trees.shape)
    # Look at the forest from the outside and mark which trees are visible
    for i in range(1, trees.shape[0] - 1):
        max = -1
        for j in range(0, trees.shape[1]):
            if trees[i, j] > max:
                max = trees[i, j]
                visible[i, j] = 1
                if max == 9:
                    break
        max = -1
        for j in range(trees.shape[1] - 1, -1, -1):
            if trees[i, j] > max:
                max = trees[i, j]
                visible[i, j] = 1
                if max == 9:
                    break
    for j in range(0, trees.shape[1]):
        max = -1
        for i in range(0, trees.shape[0]):
            if trees[i, j] > max:
                max = trees[i, j]
                visible[i, j] = 1
                if max == 9:
                    break
        max = -1
        for i in range(trees.shape[0] - 1, -1, -1):
            if trees[i, j] > max:
                max = trees[i, j]
                visible[i, j] = 1
                if max == 9:
                    break
    return int(np.sum(visible))


def solution_second_task(data: str) -> int:
    trees = get_tree_array(data)
    best_scenic_value = 0
    for i in range(1, trees.shape[0] - 1):
        for j in range(1, trees.shape[1] - 1):
            num = trees[i, j]
            # Get trees above, below, left and right
            up = np.flip(trees[0:i, j])
            down = trees[i + 1:, j]
            left = np.flip(trees[i, 0:j])
            right = trees[i, j + 1:]
            # Multiply together, number of trees visible from current tree
            scenic_value = get_first_true(up >= num) * get_first_true(
                down >= num) * get_first_true(left >= num) * get_first_true(
                    right >= num)
            # Save the best scenic value so far
            best_scenic_value = max(best_scenic_value, scenic_value)
    return best_scenic_value


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/08/data/test_data.txt")
    DATA = Utilities.read_data("2022/08/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
