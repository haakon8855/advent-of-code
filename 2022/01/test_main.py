"""haakon8855"""

import main

from aoc_utils.utilities import Utilities


def test_top_elf_calories():
    """
    Function should return the total amount of calories carried by the elf
    carrying the most calories.
    """
    test_data = Utilities.read_data("2022/01/data/test_data.txt")
    val = main.most_calories(test_data)
    assert val == 24000

    test_data_2 = Utilities.read_data("2022/01/data/test_data_2.txt")
    val = main.most_calories(test_data_2)
    assert val == 17030


def test_top_three_elves_calories():
    """
    Function should return the total amount of calories carried by the three
    elves carrying the most calories.
    """
    test_data = Utilities.read_data("2022/01/data/test_data.txt")
    val = main.top_n_most_calories(test_data)
    assert val == 45000

    test_data_2 = Utilities.read_data("2022/01/data/test_data_2.txt")
    val = main.top_n_most_calories(test_data_2)
    assert val == 35030
