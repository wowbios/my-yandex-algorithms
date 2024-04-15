using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        // _reader = new StreamReader(Console.OpenStandardInput());
        // _writer = new StreamWriter(Console.OpenStandardOutput());
        _reader = new StreamReader("input.txt");
        _writer = new StreamWriter("output.txt", false);
    }

    public void Run()
    {
        int n = int.Parse(_reader.ReadLine()!);
        string[] all = _reader.ReadLine()!.Split(' ').ToArray();

        var d = new Dictionary<string, List<int>>();
        for (int i = 0; i < all.Length; i++)
        {
            char[] arr = all[i].ToCharArray();
            Array.Sort(arr);
            string str = new string(arr);
            if (!d.TryGetValue(str, out var indicies))
                d[str] = new List<int>{i};
            else
                indicies.Add(i);
        }

        foreach(var kvp in d)
            _writer.WriteLine(string.Join(" ", kvp.Value));
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
