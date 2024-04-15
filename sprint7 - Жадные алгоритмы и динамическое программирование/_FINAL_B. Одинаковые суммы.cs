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
        (int[] scores, int sum) = ReadInput();       

        bool result = (sum % 2) switch
        {
            1 => false,
            _ => CanAccumulateSumFrom(sum / 2, scores)
        };

        _writer.WriteLine(result);
    }

    private (int[] scores, int sum) ReadInput()
    {
        int n = int.Parse(_reader.ReadLine()!);
        int[] scores = new int[n];
        string[] scoresText = _reader.ReadLine()!.Split(' ');
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            int currentScore = int.Parse(scoresText[i]);
            scores[i] = currentScore;
            sum += currentScore;
        }

        return (scores, sum);
    }

    private static bool CanAccumulateSumFrom(int sum, int[] scores)
    {
        int length = scores.Length;
        var dp = new bool[length + 1, sum + 1];
        for (int i = 1; i <= length; i++)
            dp[i, 0] = true;

        for (int i = 1; i <= length; i++)
            for (int j = 1; j <= sum; j++)
            {
                if (scores[i - 1] <= j)
                    dp[i, j] = dp[i - 1, j] || dp[i - 1, j - scores[i - 1]];
                else
                    dp[i, j] = dp[i - 1, j];
            }

        return dp[length, sum];
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
