/*
Вот какую задачу Тимофей предложил на собеседовании одному из кандидатов. Если вы с ней ещё не сталкивались, то наверняка столкнётесь –— она довольно популярная.

Дана скобочная последовательность. Нужно определить, правильная ли она.

Будем придерживаться такого определения:

    пустая строка —– правильная скобочная последовательность;
    правильная скобочная последовательность, взятая в скобки одного типа, –— правильная скобочная последовательность;
    правильная скобочная последовательность с приписанной слева или справа правильной скобочной последовательностью —– тоже правильная.

На вход подаётся последовательность из скобок трёх видов: [], (), {}.

Напишите функцию is_correct_bracket_seq, которая принимает на вход скобочную последовательность и возвращает True, если последовательность правильная, а иначе False.
Формат ввода

На вход подаётся одна строка, содержащая скобочную последовательность. Скобки записаны подряд, без пробелов.
Формат вывода

Выведите «True» или «False».
*/
using System;
using System.Collections;
using System.Collections.Generic;
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

    private static readonly Regex PushRegex = new Regex(@"push (-?\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    public void Run()
    {
        var stack = new Stack<char>();
        var line = _reader.ReadLine()!;
        foreach( var ch in line)
        {
            switch (ch)
            {
                case '(' or '[' or '{':
                    stack.Push(ch);
                    break;
                case ')' when !stack.TryPop(out char top1) || top1 != '(':
                case ']' when !stack.TryPop(out char top2) || top2 != '[':
                case '}' when !stack.TryPop(out char top3) || top3 != '{':
                    _writer.WriteLine("False");
                    return;
            }
        }
        _writer.WriteLine(stack.Any() ? "False" : "True");
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