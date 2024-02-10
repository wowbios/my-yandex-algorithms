/*
Формат ввода
В первой строке задано первое число, во второй – второе. Оба числа лежат в диапазоне от −109 до 109.
Формат вывода
Выведите единственное число – результат сложения двух чисел. 
*/
using System;

public class Solution
{
	public static void Main()
    {
    	var a = int.Parse(Console.ReadLine());
		var b = int.Parse(Console.ReadLine());
		Console.WriteLine(a+b);
    }
}

