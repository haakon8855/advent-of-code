"""haakon8855"""


def most_calories(path: str) -> int:
    """
    Returns the number of calories the elf with the most calories carries.
    """
    with open(path, 'r', encoding='utf-8') as file:
        raw = file.read()
    raw_list = raw.split('\n\n')
    highest = 0
    for elf in raw_list:
        elf_calories = sum(list(map(int, elf.split('\n'))))
        highest = max(highest, elf_calories)

    return highest


def top_n_most_calories(path: str, num: int = 3) -> int:
    """
    Returns the number of calories the elf with the most calories carries.
    """
    with open(path, 'r', encoding='utf-8') as file:
        raw = file.read()
    raw_list = raw.split('\n\n')
    highest = [0] * num
    for elf in raw_list:
        elf_calories = sum(list(map(int, elf.split('\n'))))
        lowest_highest = min(highest)
        index = highest.index(lowest_highest)
        highest[index] = max(lowest_highest, elf_calories)

    return sum(highest)


if __name__ == "__main__":
    print(top_n_most_calories("2022/01/test_data.txt"))
    print(top_n_most_calories("2022/01/data.txt"))
