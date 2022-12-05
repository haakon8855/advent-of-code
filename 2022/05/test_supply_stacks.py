"""haakon8855"""

from aoc_utils.utilities import Utilities
import supply_stacks


def test_solution_first_task():
    """
    Function should return the total amount of calories carried by the elf
    carrying the most calories.
    """
    test_data = Utilities.read_data("2022/05/data/test_data.txt")
    val = supply_stacks.solution_first_task(test_data)
    assert val == "CMZ"


def test_solution_second_task():
    """
    Function should return the total amount of calories carried by the three
    elves carrying the most calories.
    """
    test_data = Utilities.read_data("2022/05/data/test_data.txt")
    val = supply_stacks.solution_second_task(test_data)
    assert val == "MCD"
