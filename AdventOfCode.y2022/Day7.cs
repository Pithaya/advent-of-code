using AdventOfCode.Common;

namespace AdventOfCode.y2022
{
    class File
    {
        public ulong Size { get; set; }
        public string Name { get; set; }

        public File(string fileDescription)
        {
            this.Size = ulong.Parse(fileDescription.Split(" ")[0]);
            this.Name = fileDescription.Split(" ")[1];
        }
    }

    class Directory
    {
        public Dictionary<string, Directory> Directories { get; set; }
        public List<File> Files { get; set; }

        public string Name { get; set; }
        public Directory Parent { get; set; }

        public ulong Size => Files.Select(f => f.Size).Sum() + Directories.Select(f => f.Value.Size).Sum();

        public Directory(string dirDescription, Directory parentDir)
        {
            this.Directories = new Dictionary<string, Directory>();
            this.Files = new List<File>();

            this.Name = dirDescription.Split(" ").Last();
            this.Parent = parentDir;
        }

        public Directory this[string name]
        {
            get => this.Directories[name];
        }
    }

    public class Day7 : Day
    {
        public Day7(string inputFolder) : base(inputFolder)
        { }

        private Directory BuildTree(IEnumerable<string> input)
        {
            Directory root = new Directory("/", null!);

            Directory currentDir = root;

            // Build the directory tree
            foreach (var line in input)
            {
                if (line.StartsWith("$"))
                {
                    string command = line.Split(" ")[1];

                    if (command == "cd")
                    {
                        string directoryName = line.Split(" ").Last();

                        if (directoryName == "..")
                        {
                            currentDir = currentDir.Parent;
                        }
                        else if (directoryName == "/")
                        {
                            currentDir = root;
                        }
                        else
                        {
                            currentDir = currentDir[directoryName]; // Should always have been added by ls
                        }
                    }
                    else
                    {
                        // Command is "ls"
                        continue;
                    }
                }
                else
                {
                    if (line.StartsWith("dir"))
                    {
                        var dir = new Directory(line, currentDir);
                        currentDir.Directories.Add(dir.Name, dir);
                    }
                    else
                    {
                        currentDir.Files.Add(new File(line));
                    }
                }
            }

            return root;
        }

        private List<Directory> FindInTree(Directory currentRoot, Func<Directory, bool> predicate)
        {
            List<Directory> result = new List<Directory>();

            if (predicate(currentRoot))
            {
                result.Add(currentRoot);
            }

            foreach (var child in currentRoot.Directories.Values)
            {
                result.AddRange(FindInTree(child, predicate));
            }

            return result;
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Directory root = BuildTree(input);

            var dirs = FindInTree(root, (dir) => dir.Size <= 100000);

            return dirs
                .Select(d => d.Size)
                .Sum()
                .ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Directory root = BuildTree(input);

            ulong totalDiskSpace = 70000000;
            ulong neededSpace = 30000000;
            ulong unusedSpace = totalDiskSpace - root.Size;

            ulong neededDeletionSize = neededSpace - unusedSpace;

            var dirs = FindInTree(root, (dir) => dir.Size >= neededDeletionSize);

            return dirs
                .Select(d => d.Size)
                .Min()
                .ToString();
        }
    }
}
