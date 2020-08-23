using CSharpFunctionalExtensions;

namespace BinaryTreeTask.BL
{
    public abstract class Node : IRootNode
    {
        public int Value { get; }
        protected Node(int value, Node leftNode, Node rightNode)
        {
            Value = value;
            Left = leftNode;
            Right = rightNode;
        }

        public Node Left { get; private set; }
        public Node Right { get; private set; }

        protected int? GetMaxSum()
        {
            if (Right == default && Left == default)
                return Value;

            int? result = default;

            int? leftResult = null;
            if (GetType() != Left.GetType())
                leftResult = Left.GetMaxSum();

            int? rightResult = default;
            if (GetType() != Right.GetType())
                rightResult = Right.GetMaxSum();

            if (leftResult != null && rightResult != null)
                return leftResult.Value > rightResult.Value
                    ? leftResult.Value + Value
                    : rightResult.Value + Value;

            if (leftResult != null)
                return leftResult.Value + Value;

            if (rightResult != null)
                return rightResult.Value + Value;

            return result;
        }

        public static Node Create(int value, Node leftNode = default, Node rightNode = default)
        {
            if (value % 2 == 0)
                return new EvenNode(value, leftNode, rightNode);

            return new OddNode(value, leftNode, rightNode);
        }

        Result<int> IRootNode.GetMaxSumOfOddEventSequentialNodes()
        {
            var result = GetMaxSum();

            if (result == null)
                return Result.Failure<int>("Binary tree does not contain valid path.");

            return result.Value;
        }

        private class OddNode : Node
        {
            public OddNode(int value, Node leftNode, Node rightNode)
                : base(value, leftNode, rightNode)
            {
            }
        }

        private class EvenNode : Node
        {
            public EvenNode(int value, Node leftNode, Node rightNode)
                : base(value, leftNode, rightNode)
            {
            }
        }
    }
}