/*
Обратите внимание на ограничения в этой задаче.

Рита и Гоша играют в игру. У Риты есть n фишек, на каждой из которых написано количество очков. Фишки лежат на столе в порядке неубывания очков на них. Сначала Гоша называет число k, затем Рита должна выбрать две фишки, сумма очков на которых равна заданному числу.

Рите надоело искать фишки самой, и она решила применить свои навыки программирования для решения этой задачи. Помогите ей написать программу для поиска нужных фишек.
Формат ввода

В первой строке записано количество фишек n, 2 ≤ n ≤ 105.

Во второй строке записано n целых чисел в порядке неубывания —– очки на фишках Риты в диапазоне от -105 до 105.

В третьей строке —– загаданное Гошей целое число k, -105 ≤ k ≤ 105.
Формат вывода

Нужно вывести два числа —– очки на двух фишках, в сумме дающие k.

Если таких пар несколько, то можно вывести любую из них.

Если таких пар не существует, то вывести «None».
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

public class Program
{
    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        _ = Console.ReadLine();
        var array = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        int k = int.Parse(Console.ReadLine());
        var set = new HashSet<int>();
        for (int i=0;i<array.Length;i++)
        {
            var e = array[i];
            var other = k - e;
            if (set.Contains(other))
            {
                Console.WriteLine($"{other} {e}");
                return;
            }
            else
                set.Add(e);
        }
                
        Console.WriteLine("None");
    }
}