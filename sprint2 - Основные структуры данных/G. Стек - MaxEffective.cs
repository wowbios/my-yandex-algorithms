/*
Реализуйте класс StackMaxEffective, поддерживающий операцию определения максимума среди элементов в стеке. Сложность операции должна быть O(1). Для пустого стека операция должна возвращать None. При этом push(x) и pop() также должны выполняться за константное время.
Формат ввода

В первой строке записано одно число — количество команд, оно не превосходит 100000. Далее идут команды по одной в строке. Команды могут быть следующих видов:

    push(x) — добавить число x в стек. Число x не превышает 105;
    pop() — удалить число с вершины стека;
    get_max() — напечатать максимальное число в стеке;
    top() — напечатать число с вершины стека;

Если стек пуст, при вызове команды get_max нужно напечатать «None», для команды pop и top — «error».

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

public record StackValue(StackValue? Previous, int Value, int Max);

public class StackMaxEffective
{
    public StackValue? _topValue;

    public void Push(int x)
    {
        _topValue = new (_topValue, x, _topValue is null ? x : Math.Max(_topValue.Max, x));
    }

    public int? Pop()
    {
        int? top = Top();
        if (_topValue is not null)
            _topValue = _topValue.Previous;

        return top;
    }

    public int? GetMax() => _topValue?.Max;

    public int? Top() => _topValue?.Value;
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
        var stack = new StackMaxEffective();
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
                case "top":
                    var top = stack.Top();
                    _writer.WriteLine(top?.ToString() ?? "error");
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