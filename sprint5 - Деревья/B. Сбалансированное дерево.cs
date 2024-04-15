using System;

#if !REMOTE_JUDGE
public class Node
{
    public int Value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

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
        
        var leftHeight = GetHeight(root.Left, 0);
        var rightHeight = GetHeight(root.Right, 0);
        if (Math.Abs(leftHeight - rightHeight) > 1)
            return false;
        
        return Solve(root.Left) && Solve(root.Right);
    }

    private static int GetHeight(Node node, int height)
    {
        if (node is null)
            return 0;
        
        height = Math.Max(GetHeight(node.Left, height), GetHeight(node.Right, height));

        return height + 1;
    }

    #if !REMOTE_JUDGE
    public static void M()
    {
        var node1 = new Node(1);
        var node2 = new Node(-5);
        var node3 = new Node(3)
        {
            Left = node1,
            Right = node2
        };
        var node4 = new Node(10);
        var node5 = new Node(2)
        {
            Left = node3,
            Right = node4
        };
        Console.WriteLine(Solve(node5));
    }
    #endif
}
