/*
После того, как Гоша узнал про сортировку слиянием и быструю сортировку, он решил придумать свой метод сортировки, который предполагал бы разделение данных на части.
Назвал он свою сортировку Частичной.
Этим методом можно отсортировать n уникальных чисел a1, a2, … , an, находящихся в диапазоне от 0 до n - 1.
Алгоритм сортировки состоит из трёх шагов:

    Разбить исходную последовательность на k блоков B1, …, Bk. Блоки могут иметь разные размеры. Если размер блока i равен si, то B1 ={ a1, …, as1 }, B2 = { as1 + 1, … , as1 + s2 } и так далее.
    Отсортировать каждый из блоков.
    Объединить блоки — записать сначала отсортированный блок B1, потом B2, … , Bk

Частичная сортировка лучше обычной в том случае, если в первом пункте у нас получится разбить последовательность на большое число блоков. Однако далеко не каждое такое разбиение подходит. Определите максимальное число блоков, на которое можно разбить исходную последовательность, чтобы сортировка отработала корректно.

Рассмотрим пример: a = [3, 2, 0, 1, 4, 6, 5].
Минимальный размер первого блока B1 равен 4. Если взять лишь первые два элемента, то отсортированная последовательность будет начинаться с двойки, что неправильно. Если взять первые три элемента, то последовательность будет начинаться с нуля, но после него сразу же пойдёт двойка. Первые четыре элемента уже гарантируют корректный префикс после объединения блоков. Четвёрку можно взять как самостоятельный блок из одного элемента. Последние два элемента надо объединить в третий блок. Таким образом:

B1 = { 3, 2, 0, 1 }
B2 = { 4 }
B3 = { 6, 5 }

В данном примере ответ равен 3. Максимальное число блоков равно трём.
Формат ввода

В первой строке задано n — количество чисел для сортировки (n ≤ 1000).
В следующей строке записаны числа от 0 до n - 1, которые надо разбить на блоки.
Формат вывода

Выведите максимальное количество блоков, на которое можно разбить данные при использовании метода Частичной сортировки.
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
        _ = ReadInt();
        var arr = ReadArray();

        // SUM == N * (N-1)
        int blocksCount = 0;
        int sum = 0;
        int expected = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
            expected += i;

            if (sum == expected)
                blocksCount ++;
        }
        _writer.WriteLine(blocksCount);
    }

    private int ReadInt() => int.Parse(_reader.ReadLine()!);

    private int[] ReadArray() => _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();

    public void Dispose()
    {
        _reader.Dispose();
        _writer.Flush();
        _writer.Dispose();
    }

    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Solution.M(Array.Empty<string>());
        using var program = new Program();
        program.Run();
    }
}