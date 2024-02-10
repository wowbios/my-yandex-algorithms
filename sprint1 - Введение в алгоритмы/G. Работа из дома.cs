/*
Вася реализовал функцию, которая переводит целое число из десятичной системы в двоичную. Но, кажется, она получилась не очень оптимальной.

Попробуйте написать более эффективную программу.

Не используйте встроенные средства языка по переводу чисел в бинарное представление.
Формат ввода

На вход подаётся целое число в диапазоне от 0 до 10000.
Формат вывода

Выведите двоичное представление этого числа.
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
        int n = int.Parse(_reader.ReadLine()!);
        int r = 14;
        List<char> chars = new List<char>();
        
        while (r >= 0)
        {
            var pow = (int)Math.Pow(2,r);
            if (n >= pow)
            {
                chars.Add('1');
                n -= pow;
            }
            else
                chars.Add('0');

            r --;
        }
        _writer.WriteLine(string.Join("", chars).TrimStart('0').PadLeft(1, '0'));
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