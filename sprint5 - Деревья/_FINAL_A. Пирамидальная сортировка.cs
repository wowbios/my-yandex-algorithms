using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

public sealed record Competitor(string Name, int Solved, int Penalty) : IComparable<Competitor>
{
    public int CompareTo(Competitor? other)
    {
        if (other is null) return -1;

        int compareResult = Solved.CompareTo(other.Solved);
        if (compareResult != 0)
            return compareResult;

        compareResult = Penalty.CompareTo(other.Penalty);
        if (compareResult != 0)
            return -compareResult;

        return string.Compare(other.Name, Name);
    }

    public static bool operator >(Competitor? first, Competitor? second)
    {
        if (first is null && second is null)
            return false;
        if (first is null || second is null)
            return second is null;

        return first.CompareTo(second) > 0;
    }

    public static bool operator <(Competitor? first, Competitor? second)
    {
        if (first is null && second is null)
            return false;
        if (first is null || second is null)
            return second is null;

        return first.CompareTo(second) < 0;
    }
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
        int count = int.Parse(_reader.ReadLine()!);
        var heap = new Competitor?[count + 1]; // 1-based
        ReadCompetitorsToHeap(heap, count);

        Competitor? max;
        while ((max = PopMax(heap)) is not null)
            _writer.WriteLine(max.Name);
    }

    private void ReadCompetitorsToHeap(Competitor?[] heap, int count)
    {
        int index = 1;
        for (int i = 0; i < count; i++)
        {
            string[] values = _reader.ReadLine()!.Split(' ').ToArray();
            Competitor competitor = new(
                    values[0],
                    int.Parse(values[1]),
                    int.Parse(values[2]));

            index = Array.IndexOf(heap, null, index);
            heap[index] = competitor;
            SiftUp(heap, index);
        }
    }

    private static Competitor? PopMax(Competitor?[] heap)
    {
        Competitor? result = heap[1];
        (heap[1], heap[^1]) = (heap[^1], null);
        SiftDown(heap, 1);
        return result;
    }

    private static void SiftUp(Competitor?[] heap, int index)
    {
        while (index > 1)
        {
            int parent = index / 2;
            if (!(heap[index] > heap[parent]))
                break;

            (heap[index], heap[parent]) = (heap[parent], heap[index]);
            index = parent;
        }
    }

    private static void SiftDown(Competitor?[] array, int index)
    {
        while (true)
        {
            int leftChild = index * 2;
            int rightChild = index * 2 + 1;

            if (leftChild >= array.Length)
                break;

            int largestIndex = (rightChild < array.Length && array[rightChild] > array[leftChild])
                ? rightChild
                : leftChild;

            if (!(array[largestIndex] > array[index]))
                break;

            (array[largestIndex], array[index]) = (array[index], array[largestIndex]);
            index = largestIndex;
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
