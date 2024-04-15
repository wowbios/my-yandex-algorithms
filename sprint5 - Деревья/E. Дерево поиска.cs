#if !REMOTE_JUDGE
using System;

public class Node
{
    public int Value;
    public Node Left;
    public Node Right;

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}
#endif

public class Solution
{
    public static bool Solve(Node root)
    {
        if (root is null)
            return true;
        
        var value = root.Value;
        var ok = IsLessSub(root.Left, value) && IsMoreSub(root.Right, value);

        return ok && Solve(root.Left) && Solve(root.Right);

        static bool IsLessSub(Node node, int value)
        {
            if (node is null)
                return true;

            if (node.Value >= value)
                return false;
            
            return IsLessSub(node.Left, value) && IsLessSub(node.Right, value);
        }

        static bool IsMoreSub(Node node, int value)
        {
            if (node is null)
                return true;
                
            if (node.Value <= value)
                return false;
            
            return IsMoreSub(node.Left, value) && IsMoreSub(node.Right, value);
        }
    }

    #if !REMOTE_JUDGE
    private static void Main()
    {
        var node1 = new Node(1);
        var node2 = new Node(4);
        var node3 = new Node(3)
        {
            Left = node1,
            Right = node2
        };
        var node4 = new Node(8);
        var node5 = new Node(5)
        {
            Left = node3,
            Right = node4
        };
        Console.WriteLine(Solve(node5));
        node2.Value = 5;
        Console.WriteLine(Solve(node5));
    }
    #endif
}
