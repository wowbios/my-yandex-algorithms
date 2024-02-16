/*
Гоша любит играть в игру «Подпоследовательность»: даны 2 строки, и нужно понять, является ли первая из них подпоследовательностью второй. Когда строки достаточно длинные, очень трудно получить ответ на этот вопрос, просто посмотрев на них. Помогите Гоше написать функцию, которая решает эту задачу.
Формат ввода

В первой строке записана строка s.

Во второй —- строка t.

Обе строки состоят из маленьких латинских букв, длины строк не превосходят 150000. Строки не могут быть пустыми.
Формат вывода

Выведите True, если s является подпоследовательностью t, иначе —– False.
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
        string s = _reader.ReadLine()!;
        string t = _reader.ReadLine()!;
        _writer.WriteLine(Solve(s,t) ? "True" : "False");
    }

    private bool Solve(string s, string t)
    {
        if (t.Length < s.Length)
            return false;
        
        for (int i = 0, j = 0; i < s.Length; i++)
        {
            bool found = false;
            while (j < t.Length && !found)
                found = t[j++] == s[i];

            if (!found)
                return false;
        }
        
        return true;
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