"""haakon8855"""

from aoc_utils.utilities import Utilities
import rope_bridge


def test_solution_first_task():
    """
    Function should return the total amount of calories carried by the elf
    carrying the most calories.
    """
    test_data = Utilities.read_data("2022/09/data/test_data.txt")
    val = rope_bridge.solution_first_task(test_data)
    assert val == 88


def test_solution_second_task():
    """
    Function should return the total amount of calories carried by the three
    elves carrying the most calories.
    """
    test_data = Utilities.read_data("2022/09/data/test_data.txt")
    val = rope_bridge.solution_second_task(test_data)
    assert val == 36
