/*
Нужно реализовать класс StackMax, который поддерживает операцию определения максимума среди всех элементов в стеке. Класс должен поддерживать операции push(x), где x – целое число, pop() и get_max().

Формат ввода

В первой строке записано одно число n — количество команд, которое не превосходит 10000. В следующих n строках идут команды. Команды могут быть следующих видов:

    push(x) — добавить число x в стек. Число x не превышает 105;

    pop() — удалить число с вершины стека;

    get_max() — напечатать максимальное число в стеке;

Если стек пуст, при вызове команды get_max() нужно напечатать «None», для команды pop() — «error».
Формат вывода

Для каждой команды get_max() напечатайте результат её выполнения. Если стек пустой, для команды get_max() напечатайте «None». Если происходит удаление из пустого стека — напечатайте «error». 
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

public class StackMax
{
    private readonly List<int> _list = new List<int>();
    private int? _max;

    public void Push(int x)
    {
        _max = null;
        _list.Add(x);
    }

    public int? Pop()
    {
        if (_list.Count is 0)
            return null;

        _max = null;
        int lastIndex = _list.Count - 1;
        int element = _list[lastIndex];
        _list.RemoveAt(lastIndex);

        return element;
    }

    public int? GetMax()
    {
        if (_max is null)
            _max = _list.Count is 0 ? null : _list.Max();

        return _max;
    }
}

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
        var stack = new StackMax();
        var n = int.Parse(_reader.ReadLine()!);
        for (int i = 0; i < n; i++)
        {
            string command = _reader.ReadLine()!;   
            switch (command)
            {
                case "get_max":
                    var max = stack.GetMax();
                    _writer.WriteLine(max?.ToString() ?? "None");
                    break;
                case "pop":
                    var pop = stack.Pop();
                    if (pop is null)
                    _writer.WriteLine("error");
                    break;
                default:
                    var match = PushRegex.Match(command).Groups[1].Value;
                    stack.Push(int.Parse(match));
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