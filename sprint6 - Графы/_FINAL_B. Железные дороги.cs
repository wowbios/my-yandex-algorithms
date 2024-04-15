using System;
using System.Globalization;
using System.IO;
using System.Threading;

public enum Color : byte
{
    White,
    Grey,
    Black
}

public class Program : IDisposable
{
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public Program()
    {
        _reader = new StreamReader("input.txt");
        _writer = new StreamWriter("output.txt", false);
    }

    public void Run()
    {
        (int size, bool[,] matrix) = ReadAdjacencyMatrix();

        bool isOptimal = true;

        var colors = new Color[size];
        for (int i = 0; i < size; i++)
        {
            if (colors[i] is Color.Black)
                continue;

            if (HaveCycles(i, matrix, colors))
            {
                isOptimal = false;
                break;
            }
        }

        _writer.WriteLine(isOptimal ? "YES" : "NO");
    }

    private (int size, bool[,] matrix) ReadAdjacencyMatrix()
    {
        int n = int.Parse(_reader.ReadLine()!);
        bool[,] matrix = new bool[n,n];

        for (int i = 0; i < n - 1; i++)
        {
            string line = _reader.ReadLine()!;
            for (int j = 0; j < line.Length; j++)
            {
                int key = i;
                int value = i + j + 1;

                if (line[j] == 'R')
                    (key, value) = (value, key); // B: 1 -> 2, R: 2 -> 1

                matrix[key, value] = true;
            }
        }

        return (n, matrix);
    }

    private static bool HaveCycles(int vertex, bool[,] matrix, Color[] colors)
    {        
        colors[vertex] = Color.Grey;

        for (int outVertex = 0; outVertex < matrix.GetLength(0); outVertex++)
        {
            bool isConnected = matrix[vertex, outVertex];
            if (!isConnected)
                continue;
            
            if (colors[outVertex] is Color.Grey
                || (colors[outVertex] is Color.White && HaveCycles(outVertex, matrix, colors)))
                return true;
        }

        colors[vertex] = Color.Black;

        return false;
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
        // Solution.M();
    }
}