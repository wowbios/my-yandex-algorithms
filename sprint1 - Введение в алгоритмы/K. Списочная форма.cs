/*
Вася просил Аллу помочь решить задачу. На этот раз по информатике.

Для неотрицательного целого числа X списочная форма –— это массив его цифр слева направо. К примеру, для 1231 списочная форма будет [1,2,3,1]. На вход подается количество цифр числа Х, списочная форма неотрицательного числа Х и неотрицательное число K. Число К не превосходят 10000. Длина числа Х не превосходит 1000.

Нужно вернуть списочную форму числа X + K.

Не используйте встроенные средства языка для сложения длинных чисел.
Формат ввода

В первой строке — длина списочной формы числа X. На следующей строке — сама списочная форма с цифрами записанными через пробел.

В последней строке записано число K, 0 ≤ K ≤ 10000.
Формат вывода

Выведите списочную форму числа X+K. 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
        var n = int.Parse(_reader.ReadLine()!);
        var xStr = _reader.ReadLine()!;
        var k = _reader.ReadLine()!
            .PadLeft(n, '0')
            .Select(x => (int)char.GetNumericValue(x))
            .ToArray();

        var x = string.Join("", xStr.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .PadLeft(Math.Max(n, k.Length), '0')
            .Select(ch => (int)char.GetNumericValue(ch));

        List<int> chars = new ();
        int rem = 0;
        foreach (var (xi, ki) in x.Zip(k).Reverse())
        {
            int next = xi + ki + rem;
            rem = next / 10;
            next %= 10;
            chars.Add(next);
        }
        if (rem > 0)
            chars.Add(rem);
        
        chars.Reverse();
        _writer.WriteLine(string.Join(" ", chars));
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