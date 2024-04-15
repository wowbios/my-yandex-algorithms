using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading;

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
        ulong n = (ulong)int.Parse(_reader.ReadLine()!);

        var result = Factor(2 *n) / (Factor(n) * Factor(n+1));
        _writer.WriteLine(result);

        static BigInteger Factor(BigInteger n ){
            BigInteger r = 1;
            for (int i = 2; i <= n; i++)
                r *= i;
                
            return r;
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
    }
}
