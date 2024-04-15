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
    public static int Solve(Node root)
    {
        if (root is null)
            return 0;

        if (root.Left is null && root.Right is null)
            return root.Value;
        
        return Math.Max(root.Value, Math.Max(Solve(root.Left), Solve(root.Right)));
    }

    #if !REMOTE_JUDGE
    private static void Main()
    {
        var node1 = new Node(1);
        var node2 = new Node(-5);
        var node3 = new Node(3)
        {
            Left = node1,
            Right = node2
        };
        var node4 = new Node(2)
        {
            Left = node3
        };
        Console.WriteLine(Solve(node4));
    }
    #endif
}
