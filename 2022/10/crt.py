"""haakon8855"""

from aoc_utils.utilities import Utilities


def get_sig_strength(cycle: int, x_reg: int) -> int:
    if (cycle - 20) % 40 == 0:
        return cycle * x_reg
    return 0


def draw_pixel(cycle: int, x_reg: int, screen: str) -> str:
    cycle += 1
    if cycle % 40 == 0:
        screen += '\n'
    if cycle % 40 in range(x_reg, x_reg + 3):
        return cycle, screen + '#'
    return cycle, screen + '.'


def solution_first_task(data: str) -> int:
    instructions = data.split('\n')
    sig_strength_total = 0
    cycle = 0
    x_reg = 1
    for instruction in instructions:
        if len(instruction) == 4:
            # opcode is noop
            cycle += 1
            sig_strength_total += get_sig_strength(cycle, x_reg)
        else:
            # opcode is addx
            _, operand = instruction.split(' ')
            cycle += 1
            sig_strength_total += get_sig_strength(cycle, x_reg)
            cycle += 1
            sig_strength_total += get_sig_strength(cycle, x_reg)
            x_reg += int(operand)
    return sig_strength_total


def solution_second_task(data: str) -> int:
    screen = ''
    instructions = data.split('\n')
    cycle = -1
    x_reg = 0
    for instruction in instructions:
        if len(instruction) == 4:
            # opcode is noop
            cycle, screen = draw_pixel(cycle, x_reg, screen)
        else:
            # opcode is addx
            _, operand = instruction.split(' ')
            cycle, screen = draw_pixel(cycle, x_reg, screen)
            cycle, screen = draw_pixel(cycle, x_reg, screen)
            x_reg += int(operand)
    return screen


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/10/data/test_data.txt")
    DATA = Utilities.read_data("2022/10/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
