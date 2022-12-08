"""haakon8855"""

from aoc_utils.utilities import Utilities


def get_dir_size(tree: dict, directory: str, recursive=False) -> int:
    """
    Return size of the given directory.
    If recursive=True, returns the total size of the directory, including
    size of child directories. Else only size of files in given directory.
    """
    size = 0
    cd_dir = tree[directory]  # List of contents of given dir
    for entry in cd_dir:
        # For each item in the given dir
        if isinstance(entry, int):
            # If the entry is an int -> it is the size of a file
            size += entry
        else:
            # Else, it is the name of a child dir
            if recursive:
                # If recursive=True, add size of the child dir
                size += get_dir_size(tree, entry, recursive=recursive)
    return size


def create_tree(data: str) -> tuple[dict, list]:
    dirs = data.split('cd ')[1:]
    counter = 0  # Counter for handling duplicate dir names
    duplicates = {}  # Dict for handling duplicate dir names
    dir_names = []  # List containing all dir names
    tree = {}  # The final dict containing a list for all dir names
    for directory in dirs:
        # For each directory entry
        # Split string on line break
        contents = directory.split('\n')
        # Remove all lines starting with $
        contents = [item for item in contents if not item.startswith('$')]
        # First line contains current directory name
        dir_name = contents[0]
        if dir_name in duplicates:
            # If the current dir name is the same name as another directory
            # rename the current folder to the last element in the list in
            # the duplicates-dict.
            new_dir_name = duplicates[dir_name].pop()
            dir_name = new_dir_name
        # Remove the first line from the contents (the line containing cd name)
        contents = contents[1:]
        # Skip current directory if dir name starts with .
        if dir_name[0] == '.':
            continue
        # Add current dir name to list of visited dirs
        dir_names.append(dir_name)
        cd_dir = []
        for file in contents:
            # For each file/dir in cd
            # Split line on space
            entry = file.split(' ')
            if entry[0] == 'dir':
                # If first word is 'dir' -> add the name of
                # that dir to children
                name = entry[1]  # Name of child dir
                if name in dir_names:
                    # If child dir name is a duplicate name of another dir
                    # Need to rename this dir to avoid conflicts
                    # Add an int to the end of the name
                    new_name = name + str(counter)
                    if name not in duplicates:
                        # Add a list of duplicate names
                        # Duplicate names are removed in a LIFO order
                        duplicates[name] = []
                    # Add the new name of the currend child dir to the stack
                    duplicates[name].append(new_name)
                    # Rename the child dir
                    name = new_name
                    # Remember to increase the counter
                    counter += 1
                # Append the child dir name to the list
                cd_dir.append(name)
            else:
                # Else, the entry is a file and we should
                # add an int of the file size instead.
                cd_dir.append(int(entry[0]))
        # Add the list of file sizes and child dir
        # names to the list of all dirs
        tree[dir_name] = cd_dir
    return tree, dir_names


def solution_first_task(data: str) -> int:
    # Get the file tree and a list of all directory names
    tree, dir_names = create_tree(data)

    total_size = 0
    for dir_name in dir_names:
        # For each directory in the file system
        # Get the total size of each dir
        size = get_dir_size(tree, dir_name, recursive=True)
        if size <= 100000:
            # If the total size of a dir is above a threshold,
            # add it to the total
            total_size += size

    return total_size


def solution_second_task(data: str) -> int:
    # Get the file tree and a list of all directory names
    tree, dir_names = create_tree(data)

    total_space = 70_000_000  # Total amount of space on the drive
    needed_space = 30_000_000  # Required avail. space to update
    # Currently used space
    used_space = get_dir_size(tree, '/', recursive=True)
    # Currently available space
    available_space = total_space - used_space
    # Amount of space needed to delete in order to have enough space to update
    deletion_needed = needed_space - available_space

    # Find the smalles directory larger than deletion_needed
    lowest = total_space
    for dir_name in dir_names:
        # For each directory in the file system
        # Get the total size of each dir
        size = get_dir_size(tree, dir_name, recursive=True)
        if size >= deletion_needed and size < lowest:
            # If the dir size is large enough and lower than the current
            # "record" for lowest deletable folder, update lowest amount
            lowest = size
    return lowest


if __name__ == "__main__":
    TEST_DATA = Utilities.read_data("2022/07/data/test_data.txt")
    DATA = Utilities.read_data("2022/07/data/data.txt")

    Utilities.print_outputs(solution_first_task(TEST_DATA),
                            solution_first_task(DATA),
                            solution_second_task(TEST_DATA),
                            solution_second_task(DATA))
