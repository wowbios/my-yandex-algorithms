using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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
        string s = _reader.ReadLine()!;
        string s2 = _reader.ReadLine()!;
        _writer.WriteLine(Same(s,s2) ? "YES":"NO");
    }

    private bool Same(string a, string b)
    {
        if (a.Length != b.Length)
            return false;

        var set = new HashSet<char>();
        var map = new Dictionary<char, char>();
        for (int i = 0; i < a.Length; i++)
        {
            var ai = a[i];
            var bi = b[i];

            if (map.TryGetValue(ai, out var expected))
            {
                if (bi != expected)
                    return false;
            }
            else
            {
                if (set.Contains(bi))
                    return false;

                map[ai] = bi;
                set.Add(bi);
            }
        }
        return true;
    }

    private int ReadInt() => int.Parse(_reader.ReadLine()!);

    private int[] ReadArray() => _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();

    public void Dispose()
    {
        _reader.Dispose();
        _writer.Flush();
        _writer.Dispose();
    }

    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Solution.M();
        using var program = new Program();
        program.Run();
    }
}
