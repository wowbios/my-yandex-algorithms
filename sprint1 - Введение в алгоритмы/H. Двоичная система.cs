/*
Тимофей записал два числа в двоичной системе счисления и попросил Гошу вывести их сумму, также в двоичной системе. Встроенную в язык программирования возможность сложения двоичных чисел применять нельзя. Помогите Гоше решить задачу.

Решение должно работать за O(N), где N –— количество разрядов максимального числа на входе.
Формат ввода

Два числа в двоичной системе счисления, каждое на отдельной строке. Длина каждого числа не превосходит 10 000 символов.
Формат вывода

Одно число в двоичной системе счисления.
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
        string fst = _reader.ReadLine()!;
        string snd = _reader.ReadLine()!;

        var paddingSize = Math.Max(fst.Length, snd.Length);
        if (fst.Length > snd.Length)
            snd = snd.PadLeft(paddingSize, '0');
        else
            fst = fst.PadLeft(paddingSize, '0');

        List<char> values = new List<char>();
        bool overBuf = false;
        foreach ((char first, char second) in fst.Zip(snd).Reverse())
        {
            (char result, overBuf) = (first, second) switch 
            {
                ('1', '1') => (overBuf ? '1' : '0', true),
                ('0', '0') => (overBuf ? '1' : '0', false),
                ('0', '1') or ('1', '0') when !overBuf => ('1', false),
                ('0', '1') or ('1', '0') when overBuf => ('0', true),
                _ => ('0', false)
            };
            values.Add(result);
        }
        if (overBuf)
            values.Add('1');
        _writer.WriteLine(string.Join("", values.Reverse<char>()));
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