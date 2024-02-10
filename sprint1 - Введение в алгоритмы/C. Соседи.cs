/*
Дана матрица. Нужно написать функцию, которая для элемента возвращает всех его соседей. Соседним считается элемент, находящийся от текущего на одну ячейку влево, вправо, вверх или вниз. Диагональные элементы соседними не считаются.

Например, в матрице A соседними элементами для (0, 0) будут 2 и 0. А для (2, 1) –— 1, 2, 7, 7. 

Формат ввода
В первой строке задано n — количество строк матрицы. Во второй — количество столбцов m. Числа m и n не превосходят 1000. В следующих n строках задана матрица. Элементы матрицы — целые числа, по модулю не превосходящие 1000. В последних двух строках записаны координаты элемента, соседей которого нужно найти. Индексация начинается с нуля.

Формат вывода
Напечатайте нужные числа в возрастающем порядке через пробел.
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
        var rows = int.Parse(_reader.ReadLine()!);
        var cols = int.Parse(_reader.ReadLine()!);

        var matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            var rowValues = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rowValues[j];
            }
        }

        var searchRow = int.Parse(_reader.ReadLine()!);
        var searchCol = int.Parse(_reader.ReadLine()!);

        var answer = string.Join(" ", 
        new (int, int)[] { (0, 1), (-1, 0), (0, -1), (1, 0) }
            .Select(x => (searchRow + x.Item1, searchCol + x.Item2))
            .Where(x => x.Item1 >= 0 && x.Item1 < rows && x.Item2 >= 0 && x.Item2 < cols)
            .Select(x => matrix[x.Item1, x.Item2])
            .OrderBy(x=>x));

        _writer.WriteLine(answer);
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