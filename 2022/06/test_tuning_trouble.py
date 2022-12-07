"""haakon8855"""

from aoc_utils.utilities import Utilities
import tuning_trouble


def test_solution_first_task():
    """
    Function should return the total amount of calories carried by the elf
    carrying the most calories.
    """
    test_data = Utilities.read_data("2022/06/data/test_data.txt")
    val = tuning_trouble.solution_first_task(test_data)
    assert val == 7

    test_data = Utilities.read_data("2022/06/data/test_data2.txt")
    val = tuning_trouble.solution_first_task(test_data)
    assert val == 10


def test_solution_second_task():
    """
    Function should return the total amount of calories carried by the three
    elves carrying the most calories.
    """
    test_data = Utilities.read_data("2022/06/data/test_data.txt")
    val = tuning_trouble.solution_second_task(test_data)
    assert val == 19

    test_data = Utilities.read_data("2022/06/data/test_data2.txt")
    val = tuning_trouble.solution_second_task(test_data)
    assert val == 29
