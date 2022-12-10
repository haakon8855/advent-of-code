"""haakon8855"""

from aoc_utils.utilities import Utilities
import crt


def test_solution_first_task():
    """
    Function should return the total amount of calories carried by the elf
    carrying the most calories.
    """
    test_data = Utilities.read_data("2022/10/data/test_data.txt")
    val = crt.solution_first_task(test_data)
    assert val == 13140
