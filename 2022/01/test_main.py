"""haakon8855"""

import main


def test_top_elf_calories():
    """
    Function should return the total amount of calories carried by the elf
    carrying the most calories.
    """
    test_data_path = "2022/01/test_data.txt"
    val = main.most_calories(test_data_path)
    assert val == 24000

    test_data_path = "2022/01/test_data_2.txt"
    val = main.most_calories(test_data_path)
    assert val == 17030


def test_top_three_elves_calories():
    """
    Function should return the total amount of calories carried by the three
    elves carrying the most calories.
    """
    test_data_path = "2022/01/test_data.txt"
    val = main.top_n_most_calories(test_data_path)
    assert val == 45000

    test_data_path = "2022/01/test_data_2.txt"
    val = main.top_n_most_calories(test_data_path)
    assert val == 35030
