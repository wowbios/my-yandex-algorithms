/*
Вечером ребята решили поиграть в игру «Большое число».
Даны числа. Нужно определить, какое самое большое число можно из них составить.
Формат ввода

В первой строке записано n — количество чисел. Оно не превосходит 100.
Во второй строке через пробел записаны n неотрицательных чисел, каждое из которых не превосходит 1000.
Формат вывода

Нужно вывести самое большое число, которое можно составить из данных чисел.
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
        _ = _reader.ReadLine()!;
        string[] input = _reader.ReadLine()!.Split(' ').ToArray();
        Sort(input, (x,y) => int.Parse(x + y) > int.Parse(y + x));
        _writer.WriteLine(string.Join("", input));
    }

    private static void Sort(string[] input, Func<string, string, bool> comp)
    {
        for (int i = 0; i < input.Length; i++)
        {
            var element = input[i];
            int j = i;
            while (j > 0 && comp(element, input[j-1]))
            {
                input[j] = input[j-1];
                j--;
            }
            input[j] = element;
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