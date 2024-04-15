using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

public readonly record struct Edge(int To, int Weight);

public class Program : IDisposable
{
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    private readonly HashSet<int> _notAdded = new();
    private readonly PriorityQueue<Edge, int> _priorityQueue = new();

    public Program()
    {
        _reader = new StreamReader("input.txt");
        _writer = new StreamWriter("output.txt", false);
    }

    public void Run()
    {
        (int vertexCount, int edgeCount) = ReadSize();
        List<Edge>[] matrix = new List<Edge>[vertexCount];
        for (int i = 0; i < edgeCount; i++)
        {
            (int from, int to, int weight) = ReadEdge();
            @from--;
            to--;
            matrix[from] ??= new List<Edge>();
            matrix[from].Add(new Edge(to, weight));
            matrix[to] ??= new List<Edge>();
            matrix[to].Add(new Edge(from, weight));
        }

        for (int i = 0; i < vertexCount; i++)
            _notAdded.Add(i);

        int size = FindMaximumSpanningTreeWeight(matrix);
        _writer.WriteLine(_notAdded.Count > 0 ? "Oops! I did it again" : size);
    }

    private int FindMaximumSpanningTreeWeight(List<Edge>[] matrix)
    {
        int weight = 0;
        var firstVertex = 0;
        AddVertex(firstVertex, matrix);

        while (_notAdded.Count > 0 && _priorityQueue.Count > 0)
        {
            Edge maximumEdge = _priorityQueue.Dequeue();
            if (_notAdded.Contains(maximumEdge.To))
            {
                weight += maximumEdge.Weight;
                AddVertex(maximumEdge.To, matrix);
            }
        }
        return weight;
    }

    private void AddVertex(int vertex, List<Edge>[] matrix)
    {
        _notAdded.Remove(vertex);
        if (matrix[vertex] is { Count: > 0 })
        {
            foreach (Edge edge in matrix[vertex].Where(x => _notAdded.Contains(x.To)))
                _priorityQueue.Enqueue(edge, int.MaxValue - edge.Weight);
        }
    }

    private (int vertexCount, int edgeCount) ReadSize()
    {
        int[] line = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        return (line[0], line[1]);
    }

    private (int from, int to, int weight) ReadEdge()
    {
        int[] line = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        return (line[0], line[1], line[2]);
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