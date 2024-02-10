/*
Васе очень нравятся задачи про строки, поэтому он придумал свою. Есть 2 строки s и t, состоящие только из строчных букв. Строка t получена перемешиванием букв строки s и добавлением 1 буквы в случайную позицию. Нужно найти добавленную букву.
Формат ввода

На вход подаются строки s и t, разделённые переносом строки. Длины строк не превосходят 1000 символов. Строки не бывают пустыми.
Формат вывода

Выведите лишнюю букву.
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
        var s = _reader.ReadLine()!;
        var t = _reader.ReadLine()!;
        var dict = new Dictionary<char, int>();
        foreach (var ch in s)
        {
            dict.TryGetValue(ch, out int count);
            dict[ch] = ++count;
        }

        foreach (var ch in t)
        {
            if (dict.TryGetValue(ch, out int count) && count > 0)
            {
                dict[ch] = --count;
            }
            else
            {
                _writer.WriteLine(ch);
                break;
            }
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