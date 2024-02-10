/*
Тимофей ищет место, чтобы построить себе дом. Улица, на которой он хочет жить, имеет длину n, то есть состоит из n одинаковых идущих подряд участков. Каждый участок либо пустой, либо на нём уже построен дом.

Общительный Тимофей не хочет жить далеко от других людей на этой улице. Поэтому ему важно для каждого участка знать расстояние до ближайшего пустого участка. Если участок пустой, эта величина будет равна нулю — расстояние до самого себя.

Помогите Тимофею посчитать искомые расстояния. Для этого у вас есть карта улицы. Дома в городе Тимофея нумеровались в том порядке, в котором строились, поэтому их номера на карте никак не упорядочены. Пустые участки обозначены нулями.
Формат ввода

В первой строке дана длина улицы —– n (1 ≤ n ≤ 106). В следующей строке записаны n целых неотрицательных чисел — номера домов и обозначения пустых участков на карте (нули). Гарантируется, что в последовательности есть хотя бы один ноль. Номера домов (положительные числа) уникальны и не превосходят 109.
Формат вывода

Для каждого из участков выведите расстояние до ближайшего нуля. Числа выводите в одну строку, разделяя их пробелами.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using Pointer = System.Int32;

public class Program : IDisposable
{
    private const Pointer UnknownPointer = -1;
    private const int EmptyField = 0;

    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public Program()
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());
    }

    public void Run()
    {
        int[] houses = ReadInput();

        int[] remotenessMap = CreateRemotenessMap(houses);

        _writer.WriteLine(string.Join(" ", remotenessMap));
    }

    private int[] ReadInput()
    {
        _ = _reader.ReadLine()!;
        int[] houses = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        return houses;
    }

    private static int[] CreateRemotenessMap(int[] houses)
    {
        List<int> result = new();

        Pointer left = UnknownPointer, right = UnknownPointer;
        for (int i = 0; i < houses.Length; i++)
        {
            int house = houses[i];
            if (house != EmptyField)
                continue;

            if (left is UnknownPointer)
            {
                left = i;
                AddDescRange(result, left);
            }
            else if (right is UnknownPointer)
            {
                right = i;
                AddAscDescRange(result, left, right);
            }
            else
            {
                (left, right) = (right, i);
                AddAscDescRange(result, left, right);
            }
        }
        AddAscRange(result, houses.Length - (right == -1 ? left : right));

        return result.ToArray();
    }

    private static void AddAscDescRange(List<int> result, int from, int to)
    {
        int mid = from + (int)Math.Ceiling((to - from) / 2d);
        AddAscRange(result, mid - from);
        AddDescRange(result, to - mid);
    }

    private static void AddAscRange(List<int> result, int count)
    {
        for (int i = 0; i < count; i++)
            result.Add(i);
    }

    private static void AddDescRange(List<int> result, int count)
    {
        for (int i = count; i > 0; i--)
            result.Add(i);
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