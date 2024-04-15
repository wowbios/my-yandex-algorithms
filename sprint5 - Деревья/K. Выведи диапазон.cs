using System.IO;

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
    public static void PrintRange(Node root, int left, int right, StreamWriter writer)
    {
        l = left;
        r = right;
        _writer = writer;
        LMR(root, left, right, writer, false);
    }

    private static bool LMR(Node node, int l, int r, StreamWriter writer, bool found)
    {
        if (node is null)
            return true;
        
        if (!LMR(node.Left, l, r, writer, found))
            return false;

        if (node.Value >= l && node.Value <= r)
            if (!TryWrite(node.Value))
                return false;
        
        if (!LMR(node.Right, l, r, writer, found))
            return false;

        return true;
    }

    private static int l, r;
    private static StreamWriter _writer;
    private static bool TryWrite(int value)
    {
        if (value > r)
            return false;
        
        if (value >= l)
            _writer.WriteLine(value);
        
        return true;
    }
}
