/*
Тимофей решил купить несколько домов на знаменитом среди разработчиков Алгосском архипелаге. Он нашёл n объявлений о продаже, где указана стоимость каждого дома в алгосских франках. А у Тимофея есть k франков. Помогите ему определить, какое наибольшее количество домов на Алгосах он сможет приобрести за эти деньги.
Формат ввода

В первой строке через пробел записаны натуральные числа n и k.

n — количество домов, которые рассматривает Тимофей, оно не превосходит 100000;

k — общий бюджет, не превосходит 100000;

В следующей строке через пробел записано n стоимостей домов. Каждое из чисел не превосходит 100000. Все стоимости — натуральные числа.
Формат вывода

Выведите одно число —– наибольшее количество домов, которое может купить Тимофей.
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
        var temp = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        var budget = temp[1];

        var houses = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        Array.Sort(houses, (x,y) => x-y);
        var total = houses.TakeWhile(x => (budget-=x)>=0).Count();
        _writer.WriteLine(total);
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