/*
 У Тимофея было n (0≤n≤32) стажёров. Каждый стажёр хотел быть лучше своих предшественников, поэтому i-й стажёр делал столько коммитов, сколько делали два предыдущих стажёра в сумме. Два первых стажёра были менее инициативными —– они сделали по одному коммиту.

Пусть Fi —– число коммитов, сделанных i-м стажёром (стажёры нумеруются с нуля). Тогда выполняется следующее: F0=F1=1. Для всех i≥2  выполнено Fi=Fi−1+Fi−2.

Определите, сколько кода напишет следующий стажёр –— найдите Fn.

Решение должно быть реализовано рекурсивно.

Формат ввода
На вход подаётся n — целое число в диапазоне от 0 до 32.
Формат вывода
Нужно вывести Fn. 
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
        _writer.WriteLine(Fib(n));
    }

    private static int Fib(int n)
    {
        if (n is 0 or 1)
            return 1;

        return Fib(n - 1) + Fib(n - 2);
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