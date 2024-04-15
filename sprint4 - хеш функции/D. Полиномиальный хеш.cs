using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

// https://ru.algorithmica.org/cs/hashing/polynomial/
public class Program : IDisposable
{
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public Program()
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());
    }

    public void Run()
    {
        int a = int.Parse(_reader.ReadLine()!);
        int m = int.Parse(_reader.ReadLine()!);
        string text = _reader.ReadLine()!;
        var hash = GetReversePolynomialHash(text, a, m);
        _writer.WriteLine(hash);
    }

    private ulong GetReversePolynomialHash(string text, int a, int m)
    {
        ulong add = (ulong)a;
        ulong modulo = (ulong)m;
        ulong hash = 0;
        foreach(var ch in text)
        {
            hash = (hash * add + ch) % modulo;
        }

        return hash;
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
    }
}
