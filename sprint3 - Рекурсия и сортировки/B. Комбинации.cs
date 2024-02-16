/*
На клавиатуре старых мобильных телефонов каждой цифре соответствовало несколько букв. Примерно так:

2:'abc',
3:'def',
4:'ghi',
5:'jkl',
6:'mno',
7:'pqrs',
8:'tuv',
9:'wxyz'

Вам известно в каком порядке были нажаты кнопки телефона, без учета повторов. Напечатайте все комбинации букв, которые можно набрать такой последовательностью нажатий.
Формат ввода

На вход подается строка, состоящая из цифр 2-9 включительно. Длина строки не превосходит 10 символов.
Формат вывода

Выведите все возможные комбинации букв через пробел в лексикографическом (алфавитном) порядке по возрастанию.
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
/*
((()))
(()())
(())()
()(())
()()()
*/
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
        string input = _reader.ReadLine()!;
        PrintAllWords(input.Length, input, 0, "");
    }

    private bool _anyWord = false;

    private void PrintAllWords(int n, string input, int index, string word)
    {
        if (n <= 0)
        {
            if (_anyWord)
                _writer.Write(' ');
            _writer.Write(word);
            _anyWord = true;
            return;
        }

        void PrintNext(char addition) => PrintAllWords(n - 1, input, index + 1, word + addition);

        char next = input[index];
        switch (next)
        {
            case '2':
                PrintNext('a');
                PrintNext('b');
                PrintNext('c');
                break;
            case '3':
                PrintNext('d');
                PrintNext('e');
                PrintNext('f');
                break;
            case '4':
                PrintNext('g');
                PrintNext('h');
                PrintNext('i');
                break;
            case '5':
                PrintNext('j');
                PrintNext('k');
                PrintNext('l');
                break;
            case '6':
                PrintNext('m');
                PrintNext('n');
                PrintNext('o');
                break;
            case '7':
                PrintNext('p');
                PrintNext('q');
                PrintNext('r');
                PrintNext('s');
                break;
            case '8':
                PrintNext('t');
                PrintNext('u');
                PrintNext('v');
                break;
            case '9':
                PrintNext('w');
                PrintNext('x');
                PrintNext('y');
                PrintNext('z');
                break;
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