using CSharpFunctionalExtensions;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace BinaryTreeTask.BL
{
    public sealed class BinaryTreeReader
    {
        private readonly IFileSystem _fileSystem;

        public BinaryTreeReader()
            :this(new FileSystem())
        {
        }

        public BinaryTreeReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Result<BinaryTree> GetBinaryTree(string filePath)
        {
            try
            {
                var lines = _fileSystem.File.ReadAllLines(filePath);

                var validationResult = ValidateBinaryTreeStructure(lines);
                if (validationResult.IsFailure)
                    return Result.Failure<BinaryTree>(validationResult.Error);

                var node = BuildNodeTree(lines);
                return new BinaryTree(node);
            }
            catch (FileNotFoundException ex)
            {
                return Result.Failure<BinaryTree>(ex.Message);
            }
            catch (System.Exception ex)
            {
                // log and other actions
                throw;
            }

        }

        private IRootNode BuildNodeTree(string[] lines)
        {           
            var childNodes = lines[lines.Length - 1].Split(' ')
                .Select(x => int.Parse(x))
                .Select(x => Node.Create(x))
                .ToArray();

            for (int i = lines.Length - 2; i >= 0; i--)
            {
                childNodes = lines[i].Split(' ')
                    .Select(x => int.Parse(x))
                    .Select((x, j) => Node.Create(x, childNodes[j], childNodes[j + 1]))
                    .ToArray();
            }

            return childNodes.First();
        }

        private Result ValidateBinaryTreeStructure(string[] lines)
        {
            var collectionsOfStringCollections = lines.Select(x => x.Split(' ')).ToArray();

            if (collectionsOfStringCollections[0].Length != 1)
                return Result.Failure("Invalid binary tree data structure.");

            for (int i = 1; i < collectionsOfStringCollections.Length - 1; i++)
            {
                if ((collectionsOfStringCollections[i + 1].Length - collectionsOfStringCollections[i].Length) != 1)
                   return Result.Failure("Invalid binary tree data structure.");
            }
            return Result.Success();
        }
    }
}