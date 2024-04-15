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
        // _reader = new StreamReader(Console.OpenStandardInput());
        // _writer = new StreamWriter(Console.OpenStandardOutput());
        _reader = new StreamReader("input.txt");
        _writer = new StreamWriter("output.txt", false);
    }

    public void Run()
    {
        string text = _reader.ReadLine()!;

        int maxLen = 1;
        for (int i = 0; i < text.Length - maxLen; i++)
        {
            var set = new HashSet<char>();
            set.Add(text[i]);
            bool ok = true;
            for (int j = i + 1; j < text.Length; j++)
            {
                if (set.Contains(text[j]))
                {
                    maxLen = Math.Max(maxLen, j - i);
                    ok = false;
                    break;
                }
                set.Add(text[j]);
            }
            if (ok)
            {
                maxLen = Math.Max(maxLen, text.Length - i);
            }
        }

        _writer.WriteLine(maxLen);
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
