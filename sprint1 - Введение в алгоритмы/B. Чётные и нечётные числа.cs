/*
Представьте себе онлайн-игру для поездки в метро: игрок нажимает на кнопку, и на экране появляются три случайных числа. Если все три числа оказываются одной чётности, игрок выигрывает.

Напишите программу, которая по трём числам определяет, выиграл игрок или нет.
Формат ввода

В первой строке записаны три случайных целых числа a, b и c. Числа не превосходят 109 по модулю.
Формат вывода

Выведите «WIN», если игрок выиграл, и «FAIL» в противном случае.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        string line = _reader.ReadLine()!;
        bool isFirst = true;
        bool isOdd = true;
        for (int i = 1; i <= line.Length; i++)
        {
            if (i == line.Length || line[i] == ' ')
            {
                var odd = (int)char.GetNumericValue(line[i-1]) % 2 == 0;
                if (isFirst)
                {
                    isOdd = odd;
                    isFirst = false;
                }
                else if (isOdd != odd)
                {
                    _writer.WriteLine("FAIL");
                    return;
                }
            }
        }
        _writer.WriteLine("WIN");
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