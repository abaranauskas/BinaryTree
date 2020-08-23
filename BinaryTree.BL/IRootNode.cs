using CSharpFunctionalExtensions;

namespace BinaryTreeTask.BL
{
    public interface IRootNode
    {
        Node Left { get; }
        Node Right { get; }
        Result<int> GetMaxSumOfOddEventSequentialNodes();
    }
}