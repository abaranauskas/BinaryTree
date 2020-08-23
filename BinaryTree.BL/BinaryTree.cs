using CSharpFunctionalExtensions;

namespace BinaryTreeTask.BL
{
    public class BinaryTree
    {
        public BinaryTree(IRootNode rootNode)
        {
            if (rootNode == null)
                throw new System.ArgumentNullException(nameof(rootNode));

            RootNode = rootNode;
        }

        public IRootNode RootNode { get; private set; }

        public Result<int> GetMaxSumOfOddEventSequentialNodes()
        {
            return RootNode.GetMaxSumOfOddEventSequentialNodes();
        }
    }
}
