/*
К Васе в гости пришли одноклассники. Его мама решила угостить ребят печеньем.

Но не всё так просто. Печенья могут быть разного размера. А у каждого ребёнка есть фактор жадности —– минимальный размер печенья, которое он возьмёт. Нужно выяснить, сколько ребят останутся довольными в лучшем случае, когда они действуют оптимально.

Каждый ребёнок может взять не больше одного печенья.
Формат ввода

В первой строке записано n —– количество детей.

Во второй —– n чисел, разделённых пробелом, каждое из которых –— фактор жадности ребёнка. Это натуральные числа, не превосходящие 1000.

В следующей строке записано число m –— количество печенек.

Далее —– m натуральных чисел, разделённых пробелом —– размеры печенек. Размеры печенек не превосходят 1000.

Оба числа n и m не превосходят 10000.
Формат вывода

Нужно вывести одно число –— количество детей, которые останутся довольными 
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
        _ = _reader.ReadLine()!;
        var greed = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
        _ = _reader.ReadLine()!;
        var cookies = _reader.ReadLine()!.Split(' ').Select(int.Parse).ToArray();

        Array.Sort(greed, (x,y) => x-y);
        Array.Sort(cookies, (x,y) => x-y);
        int total = 0;

        int cookieIndex = 0;
        foreach (var greedee in greed)
        {
            bool found = false;
            while (!found && cookieIndex < cookies.Length)
                found = cookies[cookieIndex++] >= greedee;

            if (found)
                total++;

            if (cookieIndex >= cookies.Length)
                break;
        }
        _writer.WriteLine(total);
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

        // Solution.M(Array.Empty<string>());
        using var program = new Program();
        program.Run();
    }
}