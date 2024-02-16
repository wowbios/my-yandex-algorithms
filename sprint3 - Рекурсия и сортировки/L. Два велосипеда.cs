/*
Вася решил накопить денег на два одинаковых велосипеда — себе и сестре. У Васи есть копилка, в которую каждый день он может добавлять деньги (если, конечно, у него есть такая финансовая возможность). В процессе накопления Вася не вынимает деньги из копилки.

У вас есть информация о росте Васиных накоплений — сколько у Васи в копилке было денег в каждый из дней.

Ваша задача — по заданной стоимости велосипеда определить

    первый день, в которой Вася смог бы купить один велосипед,
    и первый день, в который Вася смог бы купить два велосипеда.

Подсказка: решение должно работать за O(log n).
Формат ввода

В первой строке дано число дней n, по которым велись наблюдения за Васиными накоплениями. 1 ≤ n ≤ 106.

В следующей строке записаны n целых неотрицательных чисел. Числа идут в порядке неубывания. Каждое из чисел не превосходит 106.

В третьей строке записано целое положительное число s — стоимость велосипеда. Это число не превосходит 106.
Формат вывода

Нужно вывести два числа — номера дней по условию задачи.

Если необходимой суммы в копилке не нашлось, нужно вернуть -1 вместо номера дня.
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
        var n = int.Parse(_reader.ReadLine()!);
        var values = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        var price = int.Parse(_reader.ReadLine()!);

        int first = FindDay(values, 0, n, price, -1);
        int second = FindDay(values, first == -1 ? 0 : first, n, price * 2, -1);
        _writer.WriteLine($"{IndexToDay(first)} {IndexToDay(second)}");
    }

    private int IndexToDay(int index) => index == -1 ? index : index + 1;

    private int FindDay(int[] arr, int left, int right, int price, int lastSuccess)
    {
        if (right <= left)
            return lastSuccess;

        var mid = (left + right) / 2;
        var midValue = arr[mid];            
        if (midValue < price)
            return FindDay(arr, mid + 1, right, price, lastSuccess);
        else
        {
            lastSuccess = mid;
            return FindDay(arr, left, mid, price, lastSuccess);
        }
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