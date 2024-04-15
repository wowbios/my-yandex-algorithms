#if !REMOTE_JUDGE
using System;

public class Node
{
    public int Value { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }

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
    public static Node Remove(Node root, int key)
    {
        var fakeRoot = new Node(key + 1)
        {
            Right = root
        };
        Remove(root, fakeRoot, key);
        return fakeRoot.Right;
    }

    private static bool IsLeaf(Node? node) => node is { Left: null, Right: null };

    private static void ReplaceChild(Node parent, Node oldChild, Node? newChild)
    {
        if (parent.Left?.Value == oldChild.Value)
            parent.Left = newChild;
        else
            parent.Right = newChild;
    }

    private static void RemoveChild(Node parent, Node child)
        => ReplaceChild(parent, child, null);

    private static void Remove(Node? node, Node parent, int key)
    {
        if (node is null)
            return;

        if (node.Value != key)
        {
            Node? child = key > node.Value ? node.Right : node.Left;
            Remove(child, node, key);
            return;
        }

        if (IsLeaf(node))
        {
            RemoveChild(parent, node);
        }
        else if (node.Left is null || node.Right is null)
        {
            ReplaceChild(parent, node, node.Left ?? node.Right);
        }
        else
        {
            Node mostFitParent = node;
            Node mostFit = node.Right;
            
            while (!IsLeaf(mostFit))
                (mostFitParent, mostFit) = (mostFit, mostFit.Left ?? mostFit.Right);

            node.Value = mostFit.Value;
            RemoveChild(mostFitParent, mostFit);
        }
    }

#if !REMOTE_JUDGE
    public static void M()
    {
        var node1 = new Node(3);
        var node2 = new Node(2)
        {
            Right = node1
        };
        var node3 = new Node(8);
        var node4 = new Node(11);
        var node5 = new Node(10)
        {
            Left = node3,
            Right = node4
        };
        var node7 = new Node(5)
        {
            Left = node2,
            Right = node5
        };

        
        var newHead = Remove(node7, 2);
    }
    #endif
}
