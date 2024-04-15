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
        int n = int.Parse(_reader.ReadLine()!);
        int s = int.Parse(_reader.ReadLine()!);
        if (n == 0)
        {
            _writer.WriteLine(0);
            return;
        }

        int[] array = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();

        var fours = GetFoursMap(array, s);
        _writer.WriteLine(fours.Length);
        foreach ((int i1,int i2,int i3,int i4) in fours)
            _writer.WriteLine($"{i1} {i2} {i3} {i4}");
    }

    record TwoSum(int fst, int snd, long total, int i, int j);

    private (int,int,int,int)[] GetFoursMap(int[] array, int s)
    {
        int counter = 0;
        var sums = new TwoSum[array.Length * array.Length / 2 - array.Length/2];
        for (int i = 0; i < array.Length; i++)
        {
            var x = array[i];
            for (int j = i + 1; j < array.Length; j++)
            {
                var y = array[j];
                sums[counter++] = new TwoSum(x, y, x + y, i, j);
            }
        }

        var fours = new HashSet<(int, int, int, int)>();
        var set = new Dictionary<long, Stack<TwoSum>>();

        for (int i = 0; i < sums.Length; i++)
        {
            var sum = sums[i];
            if (set.TryGetValue(sum.total, out var pairs))
            {
                foreach (var pair in pairs)
                {
                    if (pair.i == sum.i ||
                        pair.i == sum.j ||
                        pair.j == sum.i ||
                        pair.j == sum.j)
                        continue;

                    var arr = new int[]{
                        pair.fst,
                        pair.snd,
                        sum.fst,sum.snd
                    };
                    Array.Sort(arr);
                    fours.Add((arr[0], arr[1], arr[2], arr[3]));
                }
            }
            
                var diff = s - sum.total;
                if (!set.TryGetValue(diff, out var list))
                {
                    set[diff] = new Stack<TwoSum>();
                    set[diff].Push(sum);
                }
                else
                    list.Push(sum);
        }

        
        // foreach (var arr in result
        //     .Where(x=> x.Item1.i != x.Item2.i && x.Item1.i != x.Item2.j && x.Item1.j != x.Item2.i && x.Item1.j != x.Item2.j)
        //     .Select(x=>new int[]{x.Item2.fst, x.Item2.snd, x.Item1.fst, x.Item1.snd}))
        // {
        //     Array.Sort(arr);
        //     fours.Add((arr[0], arr[1], arr[2], arr[3]));
        // }
        return fours.OrderBy(x=>x).ToArray();
    }

    private (int,int,int,int)[] GetFoursN3(int[] array, int s)
    {
        var fours = new HashSet<(int, int, int, int)>();

        Array.Sort(array);

        var leftHistory = new HashSet<int>();
        for (int i = 0; i < array.Length; i++)
        {
            var x1 = array[i];

            var nextHistory = new HashSet<int>();
            for (int j = i + 1; j < array.Length; j++)
            {
                var x2 = array[j];

                for (int k = j + 1; k < array.Length; k++)
                {
                    var y = array[k];
                    var expect = s - x1 - x2 - y;
                    if (leftHistory.Contains(expect))
                    {
                        fours.Add((expect, x1, x2, y));
                    }
                    else if (nextHistory.Contains(expect))
                    {
                        fours.Add((x1, expect, x2, y));
                    }
                }

                nextHistory.Add(x2);
            }
            
            leftHistory.Add(x1);
        }
        return fours.OrderBy(x=>x).ToArray();
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
