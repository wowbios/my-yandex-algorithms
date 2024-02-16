/*
Перед сном Рита решила поиграть в игру на телефоне. Дан массив целых чисел, в котором каждый элемент обозначает длину стороны треугольника. Нужно определить максимально возможный периметр треугольника, составленного из сторон с длинами из заданного массива. Помогите Рите скорее закончить игру и пойти спать.

Напомним, что из трёх отрезков с длинами a ≤ b ≤ c можно составить треугольник, если выполнено неравенство треугольника: c < a + b

Разберём пример:
даны длины сторон 6, 3, 3, 2. Попробуем в качестве наибольшей стороны выбрать 6. Неравенство треугольника не может выполниться, так как остались 3, 3, 2 —– максимальная сумма из них равна 6.

Без шестёрки оставшиеся три отрезка уже образуют треугольник со сторонами 3, 3, 2. Неравенство выполняется: 3 < 3 + 2. Периметр равен 3 + 3 + 2 = 8.
Формат ввода

В первой строке записано количество отрезков n, 3≤ n≤ 10000.

Во второй строке записано n неотрицательных чисел, не превосходящих 10 000, –— длины отрезков.
Формат вывода

Нужно вывести одно число —– наибольший периметр треугольника.

Гарантируется, что тройка чисел, которая может образовать треугольник, всегда есть.
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
        _ = _reader.ReadLine()!;
        var sides = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        Array.Sort(sides, (x, y) => y - x);

        int perimeter = 0;
        for (int i = 0; i < sides.Length - 2; i++)
        {
            var longest = sides[i];
            var mid = sides[i+1];
            var lowest = sides[i+2];

            if (longest < mid + lowest)
            {
                perimeter = longest + mid + lowest;
                break;
            }
        }

        _writer.WriteLine(perimeter);
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

        // Solution.M(Array.Empty<string>());
        using var program = new Program();
        program.Run();
    }
}