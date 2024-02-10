/*
Чтобы подготовиться к семинару, Гоше надо прочитать статью по эффективному менеджменту. Так как Гоша хочет спланировать день заранее, ему необходимо оценить сложность статьи.

Он придумал такой метод оценки: берётся случайное предложение из текста и в нём ищется самое длинное слово. Его длина и будет условной сложностью статьи.

Помогите Гоше справиться с этой задачей.
Формат ввода

В первой строке дана длина текста L (1 ≤ L ≤ 105).

В следующей строке записан текст, состоящий из строчных латинских букв и пробелов. Слово —– последовательность букв, не разделённых пробелами. Пробелы могут стоять в самом начале строки и в самом её конце. Текст заканчивается переносом строки, этот символ не включается в число остальных L символов.
Формат вывода

В первой строке выведите самое длинное слово. Во второй строке выведите его длину. Если подходящих слов несколько, выведите то, которое встречается раньше. 
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
        _ = _reader.ReadLine();

        string maxWord = "";
        StringBuilder tempWord = new ();
        char ch;
        do
        {            
            ch = (char)_reader.Read();
            if (ch is ' ' or '\n' or '\r')
            {
                if (tempWord.Length > maxWord.Length)
                    maxWord = tempWord.ToString();

                tempWord.Clear();
            }
            else
                tempWord.Append(ch);
        }
        while (ch is not '\n' and not '\r');

        _writer.Write($"{maxWord}\n{maxWord.Length}");
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