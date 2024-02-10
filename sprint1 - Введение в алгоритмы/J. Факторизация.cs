/*
Основная теорема арифметики говорит: любое число раскладывается на произведение простых множителей единственным образом, с точностью до их перестановки. Например:

    Число 8 можно представить как 2 × 2 × 2.
    Число 50 –— как 2 × 5 × 5 (или 5 × 5 × 2, или 5 × 2 × 5). Три варианта отличаются лишь порядком следования множителей.

Разложение числа на простые множители называется факторизацией числа.

Напишите программу, которая производит факторизацию переданного числа.
Формат ввода

В единственной строке дано число n (2 ≤ n ≤ 109), которое нужно факторизовать.
Формат вывода

Выведите в порядке неубывания простые множители, на которые раскладывается число n. 
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
        var n = int.Parse(_reader.ReadLine());
        
        List<int> factors = new ();
        int i = 2;
        while (i * i <= n)
        {
        	while (n % i == 0)
            {
            	factors.Add(i);
            	n /= i;
            }
            i++;
        }
        if (n > 1)
            factors.Add(n);
        
        
        _writer.WriteLine(string.Join(" ", factors));
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