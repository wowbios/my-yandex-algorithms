/*
Рита решила оставить у себя одежду только трёх цветов: розового, жёлтого и малинового. После того как вещи других расцветок были убраны, Рита захотела отсортировать свой новый гардероб по цветам. Сначала должны идти вещи розового цвета, потом —– жёлтого, и в конце —– малинового. Помогите Рите справиться с этой задачей.

Примечание: попробуйте решить задачу за один проход по массиву!
Формат ввода

В первой строке задано количество предметов в гардеробе: n –— оно не превосходит 1000000. Во второй строке даётся массив, в котором указан цвет для каждого предмета. Розовый цвет обозначен 0, жёлтый —– 1, малиновый –— 2.
Формат вывода

Нужно вывести в строку через пробел цвета предметов в правильном порядке.
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
        var values = _reader.ReadLine()!;
        var temp = new int[3];
        foreach (var value in values)
        {
            if (value is ' ')
                continue;

            temp[value - 48]++;
        }

        _writer.WriteLine(
            string.Join(" ",
            Enumerable.Repeat('0', temp[0])
            .Concat(Enumerable.Repeat('1', temp[1]))
            .Concat(Enumerable.Repeat('2', temp[2])))
        );
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