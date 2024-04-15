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

            if (!edges.TryGetValue(v, out list))
                edges[v] = list = new List<int>();
            
            list.Add(u);
        }
        int start = int.Parse(_reader.ReadLine()!);
        var path = Visit(start, n, edges, colors);
        _writer.WriteLine(path.Max());

    }

    private int[] Visit(int point, int n, Dictionary<int, List<int>> edges, int[] colors)
    {
        int[] dist = new int[n + 1];
        var planned = new Queue<int>();
        planned.Enqueue(point);

        while (planned.TryDequeue(out int next))
        {
            if (colors[next] != 0)
                continue;
            
            colors[next] = 1;
            if (edges.TryGetValue(next, out var list))
            {
                foreach (var other in list.OrderBy(x=>x).Where(x => colors[x] == 0))
                {
                    dist[other] = dist[other] > 0 ? Math.Min(dist[next] + 1, dist[other]) : dist[next] + 1;
                    planned.Enqueue(other);
                }
            }
        }
        return dist;
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
