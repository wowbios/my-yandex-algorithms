/*
Гоша долго путешествовал и измерил площадь каждого из n островов Алгосов, но ему этого мало! Теперь он захотел оценить, насколько разнообразными являются острова в составе архипелага.

Для этого Гоша рассмотрел все пары островов (таких пар, напомним, n * (n-1) / 2) и посчитал попарно разницу площадей между всеми островами. Теперь он собирается упорядочить полученные разницы, чтобы взять k-ую по порядку из них.

Помоги Гоше найти k-ю минимальную разницу между площадями эффективно.

Пояснения к примерам

Пример 1

Выпишем все пары площадей и найдём соответствующие разницы

    |2 - 3| = 1
    |3 - 4| = 1
    |2 - 4| = 2

Так как нам нужна 2-я по величине разница, то ответ будет 1.

Пример 2

У нас есть два одинаковых элемента в массиве —– две единицы, поэтому минимальная (первая) разница равна нулю.
Формат ввода

В первой строке записано натуральное число n –— количество островов в архипелаге (2 ≤ n ≤ 100 000).

В следующей строке через пробел записаны n площадей островов — n натуральных чисел, каждое из которых не превосходит 1 000 000.

В последней строке задано число k. Оно находится в диапазоне от 1 до n(n - 1) / 2.
Формат вывода

Выведите одно число –— k-ую минимальную разницу. 
*/

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
        using var f =  File.OpenRead("input.txt");
        using var sr = new StreamReader(f);
        var n = int.Parse(sr.ReadLine()!);
        var islands = sr.ReadLine()!.Split(' ')
            .Select(long.Parse).ToArray();
        var k = long.Parse(sr.ReadLine()!);

        _writer.WriteLine(KthMinimumDifference(islands, k));
    }
public static long KthMinimumDifference(long[] islands, long k)
{
    Array.Sort(islands);
    long low = 0;
    long high = islands[islands.Length - 1] - islands[0];
    long mid;

    while (low < high)
    {
        mid = (high + low) / 2;

        if (C(mid, islands) >= k)
            high = mid;
        else
            low = mid + 1;
    }

    return low;
}

public static long C(long mid, long[] islands)
{
    long count = 0;
    long j = 1;

    for (int i = 0; i < islands.Length - 1; i++)
    {
        while (j < islands.Length && islands[j] - islands[i] <= mid)
            j++;

        count += j - i - 1;
    }

    return count;
}


    private int ReadInt() => int.Parse(_reader.ReadLine()!);

    private long[] ReadArray() => _reader.ReadLine()!.Split(' ').Select(long.Parse).ToArray();

    public void Dispose()
    {
        _reader.Dispose();
        _writer.Flush();
        _writer.Dispose();
    }

    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Solution.M(Array.Empty<string>());
        using var program = new Program();
        program.Run();
    }
}