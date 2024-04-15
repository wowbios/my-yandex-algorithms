using System;
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
        int n = int.Parse(_reader.ReadLine()!);
        int[] arr = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();

        int result = 0;

        if (n > 1)
        {
            bool b = false;
            int l = 0;
            int r = 1;

            while (r < n)
            {
                if (b)
                {
                    if (r == n-1)
                    {
                        result += arr[r] - arr[l];
                    }
                    else if (arr[r] > arr[r+1])
                    {
                        result += arr[r] - arr[l];
                        l = r + 1;
                        b = false;
                    }
                    r++;
                }
                else
                {
                    if (l == n - 1)
                    {
                        break;
                    }
                    else if (arr[l] < arr[l + 1])
                    {
                        b = true;
                        r = l + 1;
                    }
                    else
                        l++;
                }                
                
            }
        }

        _writer.WriteLine(result);
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