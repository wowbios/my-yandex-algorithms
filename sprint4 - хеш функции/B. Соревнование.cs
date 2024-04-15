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
        var n = ReadInt();
        var arr = n > 0 ? ReadArray() : Array.Empty<int>();

        int maxLen = 0;
        int balance = 0;
        var d = new Dictionary<int,int>();
        d[0] = 0;
        int acc = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            acc += arr[i] == 1 ? 1 : -1;
            d[acc] = i;
        }
        // foreach (var kvp in d)
        // {
        //     _writer.WriteLine($"{kvp.Key} = {kvp.Value}");
        // }
        for (int i = 0; i < arr.Length; i++)
        {
            int a = arr[i];
            balance += a == 1 ? 1 : -1;

            if (balance != 0)
            {
                if (d.TryGetValue(balance, out var k))
                {
                    maxLen = Math.Max(maxLen, k - i);
                }
                else
                    d[balance] = i;
            }
            else
                maxLen = Math.Max(maxLen, i + 1);
        }
        _writer.WriteLine(maxLen);
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
