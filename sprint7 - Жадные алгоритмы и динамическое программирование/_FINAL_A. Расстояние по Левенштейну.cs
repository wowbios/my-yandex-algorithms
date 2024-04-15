using System;
using System.Globalization;
using System.IO;
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
        string first = _reader.ReadLine()!;
        string second = _reader.ReadLine()!;

        int[,] dp = new int[first.Length + 1, second.Length + 1];

        for (int i = 0; i <= first.Length; i++)
            dp[i, 0] = i;

        for (int i = 0; i <= second.Length; i++)
            dp[0, i] = i;

        for (int i = 1; i <= first.Length; i++)
        {
            for (int j = 1; j <= second.Length; j++)
            {
                int add = dp[i, j - 1] + 1;
                int delete = dp[i - 1, j] + 1;
                int previous = dp[i - 1, j - 1];
                if (first[i - 1] != second[j - 1])
                    previous++;

                dp[i, j] = Math.Min(previous, Math.Min(add, delete));
            }
        }

        _writer.WriteLine(dp[first.Length, second.Length]);
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
