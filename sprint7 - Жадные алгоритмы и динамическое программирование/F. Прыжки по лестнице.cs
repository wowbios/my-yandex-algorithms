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
        var line = _reader.ReadLine()!.Split(' ').Select(ulong.Parse).ToArray();
        ulong n = line[0];
        ulong k = line[1];

        const ulong mod = 1_000_000_000 + 7;

        var arr = new ulong[n];
        arr[0] = 1;
        for (ulong i = 1; i < n; i++)
        {
            for (ulong j = 1; j <= k; j++)
            {
                if (i >= j)
                {
                    var last = i-j;
                    arr[i] %= mod;
                    arr[i] += arr[last] % mod;
                }
            }
            arr[i] %= mod;
        }
        _writer.WriteLine(arr[n-1]);
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
