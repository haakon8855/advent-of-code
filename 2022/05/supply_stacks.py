"""haakon8855"""

from aoc_utils.utilities import Utilities


def solution_first_task(data: str, should_reverse: bool = True) -> int:
    lines = data.split('\n')
    stack_lines = []  # Store lines containing stacks
    moves = None  # Store lines containing moves
    num_lines = 0
    for i, line in enumerate(lines):
        # Loop through each line
        if line == '':
            # If line is empty, rest of lines are moves
            moves = lines[i + 1:]
            break
        if line[1] == '1':
            num_lines = int(line[-2])
        else:
            # If line is not the stack number line, add to stack list
            stack_lines.append(line)
    # Create and fill stacks
    stacks = [[] for _ in range(num_lines)]
    for i in range(len(stack_lines) - 1, -1, -1):
        # For each line with stacks
        for j in range(num_lines):
            # For each stack within these lines
            # get the appropriate character at the correct
            # position in the line
            char = stack_lines[i][1 + 4 * j]
            # Add it to the stack if it is alphabetic
            if char.isalpha():
                stacks[j].append(char)
    if should_reverse:
        reverse = slice(None, None, -1)  # Slice to reverse a list
    else:
        reverse = slice(None, None, 1)  # Slice to not reverse a list
    # Apply each move to the stacks
    for move in moves:
        split_move = move.split(' ')
        amount = int(split_move[1])
        source = int(split_move[3]) - 1
        target = int(split_move[5]) - 1
        # Add a reversed list when moving multiple items
        stacks[target] += stacks[source][-amount:][reverse]
        # Remove the moved items from the source list
        stacks[source] = stacks[source][:-amount]
    # Put the last element in each stack in a string
    output = ''
    for stack in stacks:
        output += stack.pop()
    return output


def solution_second_task(data: str) -> int:
    return solution_first_task(data, should_reverse=False)


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/05/data/test_data.txt")
    DATA = Utilities.read_data("2022/05/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
