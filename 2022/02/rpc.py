"""haakon8855"""

import numpy as np
from aoc_utils.utilities import Utilities


def get_score(pair: np.array) -> int:
    if pair[0] == pair[1]:
        # Wins if numbers are equal
        return 6
    elif (pair[0] - 1) % 3 == pair[1]:
        # Draws if opponet's move is one less than own move
        return 3
    # Loses if opponet's move is one greater than own move
    return 0


def solution_first_task(data: str) -> int:
    # Dictionary to move to number representation
    move_score = {'A': 1, 'B': 2, 'C': 3, 'X': 1, 'Y': 2, 'Z': 3}
    rows = data.split('\n')
    # Split each row into the two letters as a list and make the whole thing
    # a 2d numpy array.
    letters = np.array([item.split(' ') for item in rows])
    # Convert all letters in the array to numbers using the move_score dict
    numbers = np.vectorize(move_score.get)(letters)
    # Get the total score from doing moves by summing the right column
    score = sum(numbers[:, 1])
    # Add 1 to each number in the left column
    numbers[:, 0] = (numbers[:, 0] + 1)
    numbers %= 3
    # Add scores from winning, losing or drawing.
    score += sum(list(map(get_score, numbers)))
    return score


def solution_second_task(data: str) -> int:
    # Dictionary to move to number representation
    move_id = {'A': 0, 'B': 1, 'C': 2}
    move_score = {'X': 0, 'Y': 3, 'Z': 6}
    rows = data.split('\n')
    # Split each row into the two letters as a list and make the whole thing
    # a 2d numpy array.
    letters = np.array([item.split(' ') for item in rows])
    # Convert left column to number representation
    moves = np.vectorize(move_id.get)(letters[:, 0])
    # Extract right column
    results = letters[:, 1]
    # Calculate sum of score (only counting winning or drawing)
    score = sum(np.vectorize(move_score.get)(results))
    # Iterate through each round and add to the score depending on which
    # move was made.
    for i in range(len(moves)):
        opp_move = moves[i]
        result = results[i]
        if result == 'X':
            # Should lose
            diff = 1 + ((opp_move - 1) % 3)
            score += diff
        elif result == 'Y':
            # Should draw
            diff = 1 + opp_move
            score += diff
        else:
            # Should win
            diff = 1 + ((opp_move + 1) % 3)
            score += diff
    return score


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/02/data/test_data.txt")
    DATA = Utilities.read_data("2022/02/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
