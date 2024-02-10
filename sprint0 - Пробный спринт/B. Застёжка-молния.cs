/*
Даны два массива чисел длины n. Составьте из них один массив длины 2n, в котором числа из входных массивов чередуются (первый — второй — первый — второй — ...). При этом относительный порядок следования чисел из одного массива должен быть сохранён.
Формат ввода

В первой строке записано целое число n –— длина каждого из массивов, 1 ≤ n ≤ 1000.

Во второй строке записано n чисел из первого массива, через пробел.

В третьей строке –— n чисел из второго массива.

Значения всех чисел –— натуральные и не превосходят 1000.
Формат вывода

Выведите 2n чисел из объединённого массива через пробел. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        _ = Console.ReadLine();
        var fst = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var snd = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        for (int i = 0; i < fst.Length; i++)
        {
            Console.Write(fst[i]);
            Console.Write(" ");
            Console.Write(snd[i]);
            if (i != fst.Length - 1)
                Console.Write(" ");
        }
    }
}