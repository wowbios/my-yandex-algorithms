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
        int m = int.Parse(_reader.ReadLine()!);
        int n = int.Parse(_reader.ReadLine()!);

        (int c, int m)[] arr = new (int c, int m)[n];
        for (int i = 0; i < n; i++)
        {
            var line = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
            arr[i] = (line[0], line[1]);
        }

        ulong total = 0;
        int left = m;
        foreach (var item in arr.OrderByDescending(x=>x.c))
        {
            int take = item.m > left ? left : item.m;
            total += (ulong)take * (ulong)item.c;

            if (item.m > left)
                break;
            
            left -= take;
        }
        _writer.WriteLine(total);
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
