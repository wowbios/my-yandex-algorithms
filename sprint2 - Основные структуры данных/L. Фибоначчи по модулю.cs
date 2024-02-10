/*
У Тимофея было очень много стажёров, целых N (0 ≤ N ≤ 106) человек. Каждый стажёр хотел быть лучше своих предшественников, поэтому i-й стажёр делал столько коммитов, сколько делали два предыдущих стажёра в сумме. Два первых стажёра были менее инициативными — они сделали по одному коммиту.

Пусть Fi —– число коммитов, сделанных i-м стажёром (стажёры нумеруются с нуля). Первые два стажёра сделали по одному коммиту: F0=F1=1. Для всех i≥ 2 выполнено Fi=Fi−1+Fi−2.

Определите, сколько кода напишет следующий стажёр –— найдите последние k цифр числа Fn.


Как найти k последних цифр

Чтобы вычислить k последних цифр некоторого числа x, достаточно взять остаток от его деления на число 10k. Эта операция обозначается как x mod 10k. Узнайте, как записывается операция взятия остатка по модулю в вашем языке программирования.

Также обратите внимание на возможное переполнение целочисленных типов, если в вашем языке такое случается.
Формат ввода

В первой строке записаны через пробел два целых числа n (0 ≤ n ≤ 106) и k (1 ≤ k ≤ 8).
Формат вывода

Выведите единственное число – последние k цифр числа Fn.

Если в искомом числе меньше k цифр, то выведите само число без ведущих нулей.
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
        var values = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        var n = values[0];
        var k = values[1];

        var fib = Fib(n, (int)Math.Pow(10, k));
        _writer.WriteLine(fib);
    }

    private long Fib(int n, int k)
    {
        int a = 0;
        long fst = 0, snd = 0;
        while (a <= n)
        {
            long fib = a is 0 or 1 ? 1 : fst + snd;
            fib %= k;
            (fst, snd) = (snd, fib);
            a++;
        }
        return snd;
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