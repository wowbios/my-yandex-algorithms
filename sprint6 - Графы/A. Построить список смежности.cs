using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        Dictionary<int, List<int>> g = new Dictionary<int, List<int>>();
        for (int i = 0; i < m; i++)
        {
            (int u, int v) = Read(); // u -> v
            if (!g.TryGetValue(u, out var list))
                g[u] = list = new List<int>();
            
            g[u].Add(v);
        }
        for (int i = 1; i <= n; i++)
        {
            g.TryGetValue(i, out var value);
            var count = value?.Count ?? 0;
            _writer.Write(count);
            if (count > 0)
                _writer.Write(" " + string.Join(" ", value));
            _writer.WriteLine();
        }
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
