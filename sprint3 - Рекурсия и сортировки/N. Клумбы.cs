/*
Алла захотела, чтобы у неё под окном были узкие клумбы с тюльпанам. На схеме земельного участка клумбы обозначаются просто горизонтальными отрезками, лежащими на одной прямой. Для ландшафтных работ было нанято n садовников. Каждый из них обрабатывал какой-то отрезок на схеме. Процесс был организован не очень хорошо, иногда один и тот же отрезок или его часть могли быть обработаны сразу несколькими садовниками. Таким образом, отрезки, обрабатываемые двумя разными садовниками, сливаются в один. Непрерывный обработанный отрезок затем станет клумбой. Нужно определить границы будущих клумб.

Рассмотрим примеры.

Пример 1:
Даны 4 отрезка: [7,8], [7,8] ,[2,3], [6,10]. Два одинаковых отрезка [7,8] и [7,8] сливаются в один, но потом их накрывает отрезок [6,10]. Таким образом, имеем две клумбы с координатами [2,3] и [6,10].

Пример 2
Во втором сэмпле опять 4 отрезка: [2,3], [3,4], [3,4], [5,6]. Отрезки [2,3], [3,4] и [3,4] сольются в один отрезок [2,4]. Отрезок [5,6] ни с кем не объединяется, добавляем его в ответ.

Формат ввода
В первой строке задано количество садовников n. Число садовников не превосходит 100000.

В следующих n строках через пробел записаны координаты клумб в формате: start end, где start —– координата начала, end —– координата конца. Оба числа целые, неотрицательные и не превосходят 107. start строго меньше, чем end.
Формат вывода
Нужно вывести координаты каждой из получившихся клумб в отдельных строках. Данные должны выводиться в отсортированном порядке —– сначала клумбы с меньшими координатами, затем —– с бОльшими. 
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

public readonly record struct Plant(int Start, int End);

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
        Plant[] plants = new Plant[n];
        for (int i = 0; i < n; i++)
        {
            var plant = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
            plants[i] = new Plant(plant[0], plant[1]);
        }

        Array.Sort(plants, (prev,next) => prev.Start - next.Start);
        List<Plant> result = new List<Plant>();
        Plant temp = plants[0];
        foreach (Plant plant in plants.Skip(1))
        {
            if (plant.Start == temp.Start)
                temp = temp with {End = Math.Max(temp.End, plant.End)};
            else if (temp.End >= plant.Start)
                temp = temp with {End = Math.Max(plant.End, temp.End)};
            else if (temp.End < plant.Start)
            {
                result.Add(temp);
                temp = plant;
            }            
        }
        result.Add(temp);
        _writer.WriteLine(string.Join("\n", result.Select(x=> $"{x.Start} {x.End}")));
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