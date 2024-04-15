/*
-- ПРИНЦИП РАБОТЫ --
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

public class Program : IDisposable
{
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public Program()
    {
        _reader = new StreamReader("input.txt");
        _writer = new StreamWriter("output.txt", false);
    }

    public void Run()
    {
        (int n, int m) = Read();
        var colors = new int[n + 1];
        Dictionary<int, List<int>> edges = new Dictionary<int, List<int>>();
        for (int i = 0; i < m; i++)
        {
            (int u, int v) = Read();
            if (!edges.TryGetValue(u, out var list))
                edges[u] = list = new List<int>();
            
            list.Add(v);
        }
        var stack = new Stack<int>();
        int index = 1;
        while ((index = Array.IndexOf(colors, 0, index)) != -1)
        {
            Visit(index, edges, colors, stack);
        }
        
        while (stack.TryPop(out int value))
            _writer.Write(value + " ");
    }

    private void Visit(int point, Dictionary<int, List<int>> edges, int[] colors, Stack<int> nodes)
    {
        if (colors[point] != 0)
            return;
        
        colors[point] = 1;
        if (edges.TryGetValue(point, out var list))
        {
            foreach (var other in list.OrderBy(x=>x).Where(x=>colors[x] == 0))
            {
                Visit(other, edges, colors, nodes);
            }
        }
        nodes.Push(point);
    }

    private (int,int) Read()
    {
        int[] line = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        return (line[0], line[1]);
    }

    public void Dispose()
    {
        _reader.Dispose();
        _writer.Flush();
        _writer.Dispose();
    }

    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        using var program = new Program();
        program.Run();
        // Solution.M();
    }
}
