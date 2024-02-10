/*
Игра «Тренажёр для скоростной печати» представляет собой поле 4x4 из клавиш, на которых — либо точка, либо цифра от одного до девяти. Суть игры следующая: каждый раунд на поле появляется комбинация цифр и точек. В момент времени t игрок должен одновременно нажать на все клавиши, где есть цифра t.

Если в момент t нажаты все нужные клавиши, игроки получают один балл. Если клавиш с цифрой t на поле нет, победное очко не начисляется

Два игрока в один момент могут нажать на k клавиш каждый. Найдите число баллов, которое смогут заработать Гоша и Тимофей, если будут нажимать на клавиши вдвоём. Рассмотрим пример 1, в котором k=3.

Допустим, t=1. В таком случае один игрок должен нажать на две кнопки с цифрой 1. Чтобы узнать, сколько клавиш нажмут два игрока, воспользуемся формулой: k*2. Получается, что вместе мальчики нажмут на шесть клавиш и получат победное очко.

Когда t=2, двум игрокам необходимо нажать на семь кнопок одновременно. Но это не под силу ребятам: каждый может нажать только по три кнопки. Победное очко не начисляется.

При t=3, каждому игроку нужно нажать на одну кнопку. Успех! Теперь на счету Гоши и Тимофея целых два победных очка.

Других цифр на поле нет. Поэтому в следующих раундах, где t=4...t=9, победные очки начисляться не будут. Таким образом, Гоша и Тимофей заработают два балла.

Найдите число баллов, которое смогут заработать Гоша и Тимофей, если будут нажимать на клавиши вдвоём.

Формат ввода

В первой строке дано целое число k (1 ≤ k ≤ 5).

В четырёх следующих строках задан вид тренажёра -— по 4 символа в каждой строке. Каждый символ – либо точка, либо цифра от 1 до 9. Символы одной строки идут подряд и не разделены пробелами.
Формат вывода

Выведите единственное число -— максимальное количество баллов, которое смогут набрать Гоша и Тимофей.
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
        const int playersCount = 2;
        const int maximumNumbers = 9;
        const int playFieldHeight = 4;

        var k = int.Parse(_reader.ReadLine()!);

        var totals = new int[maximumNumbers];
        for (int i = 0; i < playFieldHeight; i++)
        {
            foreach (char ch in _reader.ReadLine()!.Where(char.IsDigit))
            {
                int value = (int)char.GetNumericValue(ch);
                totals[value - 1]++;
            }
        }

        int result = totals.Where(x => x > 0 && x <= k * playersCount).Count();
        _writer.WriteLine(result);
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