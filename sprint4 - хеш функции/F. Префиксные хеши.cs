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
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());
    }

    public void Run()
    {
        ulong a = ulong.Parse(_reader.ReadLine()!);
        ulong m = ulong.Parse(_reader.ReadLine()!);
        string text = _reader.ReadLine()!;
        int t = int.Parse(_reader.ReadLine()!);

        var len = text.Length;

        var pows = new ulong[len];
        pows[0] = 1;
        for (int i = 1; i < text.Length; i++)
            pows[i] = pows[i-1] * a % m;

        var prefixes = GetReversePolynomialHashEnum(text, a, m).ToArray();

        for (int i = 0; i < t; i++)
        {
            var parts = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
            int li = parts[0] - 1;
            int ri = parts[1] - 1;

            var right = (long)prefixes[ri];
            if (li > 0)
            {
                var left = (long)prefixes[li - 1];
                right -= left * (long)pows[ri - li + 1] % (long)m;
            }
            if (right < 0) right += (long)m;
            _writer.WriteLine(right % (long)m);
        }
    }

    private static IEnumerable<ulong> GetReversePolynomialHashEnum(string text, ulong a, ulong m)
    {
        ulong hash = 0;
        foreach(var ch in text)
            yield return hash = (hash * a + ch) % m;
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
