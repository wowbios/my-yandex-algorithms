/*
Астрологи объявили день очередей ограниченного размера. Тимофею нужно написать класс MyQueueSized, который принимает параметр max_size, означающий максимально допустимое количество элементов в очереди.

Помогите ему —– реализуйте программу, которая будет эмулировать работу такой очереди. Функции, которые надо поддержать, описаны в формате ввода.
Формат ввода

В первой строке записано одно число — количество команд, оно не превосходит 5000.
Во второй строке задан максимально допустимый размер очереди, он не превосходит 5000.
Далее идут команды по одной на строке. Команды могут быть следующих видов:

    push(x) — добавить число x в очередь;

    pop() — удалить число из очереди и вывести на печать;

    peek() — напечатать первое число в очереди;

    size() — вернуть размер очереди;

При превышении допустимого размера очереди нужно вывести «error». При вызове операций pop() или peek() для пустой очереди нужно вывести «None».

Формат вывода

Напечатайте результаты выполнения нужных команд, по одному на строке.
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

public class MyQueueSized
{
    private readonly int _maxSize;
    private int _size;
    private readonly int[] _mem;
    private int _head, _tail;

    public MyQueueSized(int maxSize)
    {
        _maxSize = maxSize;
        _mem = new int[maxSize];
    }

    public bool Push(int value)
    {
        if (_size + 1 > _maxSize)
            return false;

        _mem[_tail] = value;
        _size++;
        _tail++;
        _tail %= _maxSize;

        return true;
    }

    public int? Pop()
    {
        if (_size is 0)
            return null;
        
        var value = _mem[_head];
        _size--;
        _head++;
        _head %= _maxSize;
        return value;
    }

    public int? Peek() => _size is 0 ? null : _mem[_head];

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

    private static readonly Regex PushRegex = new Regex(@"push (-?\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    public void Run()
    {
        var commandCount = int.Parse(_reader.ReadLine()!);
        var queueSize = int.Parse(_reader.ReadLine()!);
        var queue = new MyQueueSized(queueSize);

        for (int i = 0; i < commandCount; i++)
        {
            string command = _reader.ReadLine()!;   
            switch (command)
            {
                case "pop":
                    var pop = queue.Pop();
                    _writer.WriteLine(pop?.ToString() ?? "None");
                    break;
                case "peek":
                    var top = queue.Peek();
                    _writer.WriteLine(top?.ToString() ?? "None");
                    break;
                case "size":
                    var size = queue.GetSize();
                    _writer.WriteLine(size);
                    break;
                default:
                    var match = PushRegex.Match(command).Groups[1].Value;
                    if (!queue.Push(int.Parse(match)))
                        _writer.WriteLine("error");
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