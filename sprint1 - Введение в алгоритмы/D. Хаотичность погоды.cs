/*
Метеорологическая служба вашего города решила исследовать погоду новым способом.

    Под температурой воздуха в конкретный день будем понимать максимальную температуру в этот день.
    Под хаотичностью погоды за n дней служба понимает количество дней, в которые температура строго больше, чем в день до (если такой существует) и в день после текущего (если такой существует). Например, если за 5 дней максимальная температура воздуха составляла [1, 2, 5, 4, 8] градусов, то хаотичность за этот период равна 2: в 3-й и 5-й дни выполнялись описанные условия.

Определите по ежедневным показаниям температуры хаотичность погоды за этот период.

Заметим, что если число показаний n=1, то единственный день будет хаотичным.
Формат ввода

В первой строке дано число n –— длина периода измерений в днях, 1 ≤ n≤ 105. Во второй строке даны n целых чисел –— значения температуры в каждый из n дней. Значения температуры не превосходят 273 по модулю.
Формат вывода

Выведите единственное число — хаотичность за данный период.
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
        var n = int.Parse(_reader.ReadLine()!);
        int[] values = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();

        int chaos = 0;
        for (int i = 0; i < n; i++)
        {
            bool isChaos = (i - 1 >= 0, i + 1 < n) switch
            {
                (true, true) => values[i] > values[i - 1] && values[i] > values[i + 1],
                (false, true) => values[i] > values[i + 1],
                (true, false) => values[i] > values[i - 1],
                (false, false) => true
            };
            if (isChaos)
                chaos++;
        }

        _writer.WriteLine(chaos);
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