#!/bin/bash

# Script for formatting all modified files (as reported by git-diff) in the project.
# Find all changed files in the current repository, except deleted files. This list is passed to the find utility.
# Find all non-directory files in the passed-in list whose name does not begin with an underscore and ends with a .h, .c, .m, or .mm extension.
# Only process files newer than this script, which is being touched after each run, so files are only reformatted if they have been changed since the last run.
# Their paths are passed to the clang-format tool, which uses the .clang-format definition closest to each file, does not format if no definition is found, and formats in-place.
find . -newer $0 -type f \( \! -path "*Pods/*" -a \! -path "*.framework/*" -a \( -name "*.h" -o -name "*.c" -o -name "*.m" -o -name "*.mm" \) \) -exec \
clang-format -style=file -fallback-style=none -i {} +
touch $0