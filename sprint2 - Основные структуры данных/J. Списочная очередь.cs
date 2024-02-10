/*
Любимый вариант очереди Тимофея — очередь, написанная с использованием связного списка. Помогите ему с реализацией. Очередь должна поддерживать выполнение трёх команд:

    get() — вывести элемент, находящийся в голове очереди, и удалить его. Если очередь пуста, то вывести «error».
    put(x) — добавить число x в очередь
    size() — вывести текущий размер очереди

Формат ввода

В первой строке записано количество команд n — целое число, не превосходящее 1000. В каждой из следующих n строк записаны команды по одной строке.
Формат вывода

Выведите ответ на каждый запрос по одному в строке.
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

public record Node
{
    public Node(Node? next, int value)
    {
        Next = next;
        Value = value;
    }

    public Node? Next { get; set;}

    public int Value {get;}
}

public class ListQueue
{
    private Node? _head, _tail;
    private int _size;

    public int? Get()
    {
        if (_head is null)
            return null;

        int value = _head.Value;
        _head = _head.Next;
        if (_head is null)
            _tail = null;

        _size--;

        return value;
    }

    public void Put(int value)
    {
        Node newNode = new Node(null, value);
        if (_head is null && _tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = new Node(null, value);
            _tail = _tail.Next;
        }
        _size ++;
    }

    public int GetSize() => _size;
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

    private static readonly Regex PushRegex = new Regex(@"put (-?\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    public void Run()
    {
        var commandCount = int.Parse(_reader.ReadLine()!);
        var queue = new ListQueue();

        for (int i = 0; i < commandCount; i++)
        {
            string command = _reader.ReadLine()!;   
            switch (command)
            {
                case "get":
                    var value = queue.Get();
                    _writer.WriteLine(value?.ToString() ?? "error");
                    break;
                case "size":
                    var size = queue.GetSize();
                    _writer.WriteLine(size);
                    break;
                default:
                    var match = PushRegex.Match(command).Groups[1].Value;
                    int toPut = int.Parse(match);
                    queue.Put(toPut);
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