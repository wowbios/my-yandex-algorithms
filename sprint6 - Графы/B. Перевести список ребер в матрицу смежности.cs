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
        int[,] matrix = new int[n,n];
        for (int i = 0; i < m; i++)
        {
            (int u, int v) = Read(); // u -> v
            matrix[u - 1, v - 1] = 1;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _writer.Write(matrix[i,j]);
                if (j!=n-1)
                    _writer.Write(' ');
            }
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
