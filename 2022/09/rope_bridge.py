"""haakon8855"""

import numpy as np
from copy import deepcopy
from aoc_utils.utilities import Utilities


def chebyshev_distance(pos_1: list[int], pos_2: list[int]) -> int:
    """
    Returns the Chebyshev distance between two points.
    """
    return max(abs(pos_1[0] - pos_2[0]), abs(pos_1[1] - pos_2[1]))


def visualise(positions):
    """
    Visualises the movement of the rope given the positions of its knots.
    """
    grid = np.zeros((20, 20), int)
    offset = 10
    for i in range(len(positions) - 1, -1, -1):
        label = i
        if i == 0:
            label = 4
        grid[positions[i][1] + offset, positions[i][0] + offset] = label
    print(grid)


def solution_first_task(data: str) -> int:
    commands = data.split('\n')
    t_pos = [0, 0]
    h_pos = [0, 0]
    # Make a set of coordinates the tail has visited
    visited = set()
    for command in commands:
        # Loop through each command
        command = command.split(' ')
        direction = command[0]
        count = int(command[1])
        for _ in range(count):
            # Loop through each move in a command
            h_old = h_pos.copy()
            # Move the head of the rope the appropriate direction
            if direction == 'U':
                h_pos[1] -= 1
            elif direction == 'D':
                h_pos[1] += 1
            elif direction == 'L':
                h_pos[0] -= 1
            elif direction == 'R':
                h_pos[0] += 1
            dist = chebyshev_distance(t_pos, h_pos)
            # Tail should only be moved if it is more than
            # 1 Chebyshev distance away from the head
            if dist > 1:
                # If so, move the tail to the last location of the head
                t_pos = h_old
            # Add tail coordinates to the visited set
            visited.add(str(t_pos))
    # Answer is the number of unique coordinates the tail has visited
    return len(visited)


def solution_second_task(data: str) -> int:
    commands = data.split('\n')
    positions = [[0, 0] for _ in range(10)]
    visited = set()
    for command in commands:
        # Loop through each command
        command = command.split(' ')
        direction = command[0]
        count = int(command[1])
        for _ in range(count):
            # Loop through each move in a command
            positions_old = deepcopy(positions)
            # Move the head of the rope the appropriate direction
            if direction == 'U':
                positions[0][1] -= 1
            elif direction == 'D':
                positions[0][1] += 1
            elif direction == 'L':
                positions[0][0] -= 1
            elif direction == 'R':
                positions[0][0] += 1
            # Move the rest of the knots the appropriate amount
            for i in range(1, len(positions)):
                # Calculate Chebyshev distance between current knot and
                # knot in front of current
                dist = chebyshev_distance(positions[i], positions[i - 1])
                if dist > 1:
                    # Move the current knot if this distance is > 1
                    # Calculate the movement of the knot in front
                    movement_x = positions[i - 1][0] - positions_old[i - 1][0]
                    movement_y = positions[i - 1][1] - positions_old[i - 1][1]
                    if abs(movement_x) == 1 and abs(movement_y) == 1:
                        # If leading knot moved diagonally,
                        # we need to apply some special movement
                        # to the current knot
                        if positions[i][0] == positions[
                                i -
                                1][0] or positions[i][1] == positions[i -
                                                                      1][1]:
                            # If current knot and knot in front
                            # are in same row or col -> Move current knot
                            # to the spot between the two knots
                            # (average of coordinates)
                            positions[i][0] = int(
                                (positions[i][0] + positions[i - 1][0]) / 2)
                            positions[i][1] = int(
                                (positions[i][1] + positions[i - 1][1]) / 2)
                        else:
                            # Else move knot in the same way
                            # the leading knot moved
                            positions[i][0] += movement_x
                            positions[i][1] += movement_y
                    else:
                        # Leading knot did not move diagonally, move current
                        # knot like we moved the tail in task 1. (Set current
                        # knot position to last position of the knot in front)
                        positions[i] = positions_old[i - 1]
            visited.add(str(positions[-1]))
    return len(visited)


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/09/data/test_data.txt")
    DATA = Utilities.read_data("2022/09/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
