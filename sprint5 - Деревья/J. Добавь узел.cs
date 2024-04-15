#if !REMOTE_JUDGE
using System;

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
    public static Node Insert(Node root, int key)
    {
        InsertChild(root, key);
        return root;
    }

    private static void InsertChild(Node root, int key)
    {
        if (key >= root.Value)
        {
            if (root.Right is null)
            {
                root.Right = new Node(key);
            }
            else
            {
                InsertChild(root.Right, key);
            }
        }
        else
        {
            if (root.Left is null)
            {
                root.Left = new Node(key);
            }
            else
            {
                InsertChild(root.Left, key);
            }
        }
    }

#if !REMOTE_JUDGE
    public static void M()
    {
        var node1 = new Node(7);
        var node2 = new Node(9)
        {
            Left = node1
        };

        var node3 = new Node(7)
        {
            Right = node2
        };
        var newHead = Insert(node3, 7);
        Console.WriteLine(newHead == node3);
        Console.WriteLine(newHead.Left.Value == 7);
    }
    #endif
}
