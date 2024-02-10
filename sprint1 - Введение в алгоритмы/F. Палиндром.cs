/*
Помогите Васе понять, будет ли фраза палиндромом. Учитываются только буквы и цифры, заглавные и строчные буквы считаются одинаковыми.

Решение должно работать за O(N), где N — длина строки на входе.
Формат ввода

В единственной строке записана фраза или слово. Буквы могут быть только латинские. Длина текста не превосходит 20000 символов.

Фраза может состоять из строчных и прописных латинских букв, цифр, знаков препинания.
Формат вывода

Выведите «True», если фраза является палиндромом, и «False», если не является.
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
        var text = _reader.ReadLine()!
            .Where(char.IsLetterOrDigit)
            .Select(x => char.ToLowerInvariant(x))
            .ToArray();

        bool isPalindrome = true;
        for (int i = 0;i<text.Length;i++)
            if (text[i] != text[text.Length - i - 1])
            {
                isPalindrome = false;
                break;
            }
            
        _writer.WriteLine(isPalindrome);
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