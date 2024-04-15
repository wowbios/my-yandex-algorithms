using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

record Lesson(int start, int end, string s, string e);

public class Program : IDisposable
{
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public Program()
    {
        _reader = new StreamReader("input.txt");
        _writer = new StreamWriter("output.txt", false);
    }

    public void Run()
    {
        int n = int.Parse(_reader.ReadLine()!);
        var lessons = new Lesson[n];
        for (int i = 0; i < n; i++)
        {
            var a =_reader.ReadLine()!.Split(' ').ToArray();
            lessons[i] = new Lesson(ParseTime(a[0]), ParseTime(a[1]), a[0], a[1]);
        }

        static int ParseTime(string value)
        {
            int dot = value.IndexOf('.');
            if (dot is - 1)
            {
                return int.Parse(value) * 100;
            }
            else
            {
                if (dot is 1)
                    value = '0' + value;
                
                return int.Parse(value.Replace(".", "").PadRight(4, '0'));
            }

        }

        List<Lesson> le = new ();
        // Array.Sort(lessons, (x,y) => y.start - x.start);
        // Array.Sort(lessons, (x,y) => x.end - y.end);
        Lesson current = null;

        foreach (var l in lessons.OrderBy(x=>x.end).ThenBy(x=>x.start))
        {
            if (current is null)
            {
                current = l;
                le.Add(l);
                continue;
            }
            if (current.end <= l.start)
            {
                current= l;
                le.Add(l);
            }
        }
        _writer.WriteLine(le.Count);
        
        foreach (var item in le)
        {
            _writer.WriteLine($"{item.s} {item.e}");
        }
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
        // Solution.M();
    }
}
