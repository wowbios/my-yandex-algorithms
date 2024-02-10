/*
Вам дана статистика по числу запросов в секунду к вашему любимому рекомендательному сервису.

Измерения велись n секунд.

В секунду i поступает qi запросов.

Примените метод скользящего среднего с длиной окна k к этим данным и выведите результат.
Формат ввода

В первой строке передаётся натуральное число n, количество секунд, в течение которых велись измерения. 1 ≤ n ≤ 105

Во второй строке через пробел записаны n целых неотрицательных чисел qi, каждое лежит в диапазоне от 0 до 103.

В третьей строке записано натуральное число k (1 ≤ k ≤ n) —– окно сглаживания.

Примечание для Go:

Заметьте, что в данной задаче достаточно большой размер ввода. Поэтому необходимо задавать размер буфера для сканнера хотя бы 600 Кб.
Формат вывода

Выведите через пробел результат применения метода скользящего среднего к серии измерений. Должно быть выведено n - k + 1 элементов, каждый элемент -— вещественное (дробное) число. 
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
        var array = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int k = int.Parse(Console.ReadLine());
        var last = array.Length - k;
        var total = 0;
        for (int i = 0; i <= last; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < k; j++)
                    total += array[i + j];
            }
            else
            {
                total -= array[i - 1];
                total += array[i + k - 1];
            }

            Console.Write(total / (float)k);

            if (i != last)
                Console.Write(' ');
        }
    }
}