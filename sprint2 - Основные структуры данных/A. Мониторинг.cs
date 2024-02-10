/*
Алла получила задание, связанное с мониторингом работы различных серверов. Требуется понять, сколько времени обрабатываются определённые запросы на конкретных серверах. Эту информацию нужно хранить в матрице, где номер столбца соответствуют идентификатору запроса, а номер строки — идентификатору сервера. Алла перепутала строки и столбцы местами. С каждым бывает. Помогите ей исправить баг.

Есть матрица размера m × n. Нужно написать функцию, которая её транспонирует.

Транспонированная матрица получается из исходной заменой строк на столбцы.

Например, для матрицы А (слева) транспонированной будет следующая матрица (справа):

Формат ввода

В первой строке задано число n — количество строк матрицы.
Во второй строке задано m — число столбцов, m и n не превосходят 1000. В следующих n строках задана матрица. Числа в ней не превосходят по модулю 1000.
Формат вывода

Напечатайте транспонированную матрицу в том же формате, который задан во входных данных. Каждая строка матрицы выводится на отдельной строке, элементы разделяются пробелами. 
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
        var rows = int.Parse(_reader.ReadLine()!);
        var cols = int.Parse(_reader.ReadLine()!);

        var matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            int[] line = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
            for (int j = 0; j < cols; j++)
                matrix[i, j] = line[j];
        }

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (j!=0)
                    _writer.Write(' ');
                _writer.Write(matrix[j,i]);
            }
            _writer.WriteLine();
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