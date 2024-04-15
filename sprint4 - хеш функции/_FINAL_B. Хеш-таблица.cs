
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

/// <summary>
/// Запись хеш-таблицы в виде ключ-значение. Является элементом односвязного списка.
/// </summary>
/// <typeparam name="TKey">Тип ключа</typeparam>
/// <typeparam name="TValue">Тип значения</typeparam>
public record HashRecord<TKey, TValue>
{
    /// <summary>
    /// Создаёт новый элемент списка - пару ключ-значение.
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="value">Значение</param>
    /// <param name="next">Следующий элемент списка</param>
    public HashRecord(TKey key, TValue value, HashRecord<TKey, TValue>? next)
    {
        Key = key;
        Value = value;
        Next = next;
    }

    /// <summary>
    /// Ключ
    /// </summary>
    public TKey Key { get; }

    /// <summary>
    /// Значение
    /// </summary>
    public TValue Value { get; private set; }

    /// <summary>
    /// Следующий элемент списка
    /// </summary>
    public HashRecord<TKey, TValue>? Next { get; private set; }

    /// <summary>
    /// Установить новое значение
    /// </summary>
    /// <param name="value"></param>
    public void Update(TValue value) => Value = value;

    /// <summary>
    /// Создать следующий элемент списка
    /// </summary>
    /// <param name="key">Значение ключа следующего элемента</param>
    /// <param name="value">Значение следующего элемента</param>
    public void SetNext(TKey key, TValue value) => Next = new HashRecord<TKey, TValue>(key, value, null);

    /// <summary>
    /// Установить следующий элемент списка
    /// </summary>
    /// <param name="next">Следующий элемент</param>
    public void SetNext(HashRecord<TKey, TValue>? next) => Next = next;
}

/// <summary>
/// Хеш-таблица зарплат сотрудников
/// </summary>
public sealed class SalaryHashTable
{
    /// <summary>
    /// Размер таблицы
    /// </summary>
    private const int Size = 100_000 + 7;

    /// <summary>
    /// Значение, обратное золотому сечению
    /// </summary>
    private const double Alpha = 0.6180339887;

    /// <summary>
    /// Бакеты для размещения значений таблицы
    /// </summary>
    private readonly HashRecord<int, int>?[] _buckets = new HashRecord<int, int>?[Size];

    /// <summary>
    /// Получить значение
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <returns>Если найдено - значение, иначе null</returns>
    public int? Get(int key)
    {
        int bucket = GetBucket(key);
        HashRecord<int, int>? record = _buckets[bucket];
        while (record is not null && record.Key != key)
            record = record.Next;

        return record?.Value;
    }

    /// <summary>
    /// Вставить значение
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="value">Значение</param>
    public void Put(int key, int value)
    {
        int bucket = GetBucket(key);
        HashRecord<int, int>? record = _buckets[bucket];
        if (record is null)
            _buckets[bucket] = new HashRecord<int, int>(key, value, null);
        else
        {
            while (record.Next is not null && record.Key != key)
                record = record.Next;

            if (record.Key == key)
                record.Update(value);
            else
                record.SetNext(key, value);
        }
    }

    /// <summary>
    /// Удалить значение
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <returns>Если элемент найден - возвращает значение удалённого элемента, иначе null</returns>
    public int? Delete(int key)
    {
        int bucket = GetBucket(key);
        HashRecord<int, int>? record = _buckets[bucket];
        int? result = null;
        if (record?.Key == key)
        {
            result = record.Value;
            _buckets[bucket] = record.Next;
        }
        else
        {
            while (record is not null && record.Next?.Key != key)
                record = record.Next;
            
            if (record is not null)
            {
                result = record.Next?.Value;
                record.SetNext(record.Next?.Next);
            }
        }

        return result;
    }

    /// <summary>
    /// Возвращает номер бакета в таблице по ключу
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <returns>Номер бакета</returns>
    private static int GetBucket(int key)
    {
        int hash = GetHash(key);
        double alphaHash = Alpha * hash;
        double relativePosition = alphaHash  - Math.Floor(alphaHash);
        int absolutePosition = (int)Math.Floor(Size * relativePosition);
        return absolutePosition;
    }

    /// <summary>
    /// Возвращает хеш по ключу
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <returns>Хеш</returns>
    private static int GetHash(int key) => key + 1_000_000_000;
}

public class Program : IDisposable
{
    private const string GetVerb = "get";
    private const string PutVerb = "put";
    private const string DeleteVerb = "delete";

    private static readonly Regex CommandRegex = new (
        @"([a-z]+) (-?\d+)(?: (-?\d+))?",
        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public Program()
    {
        _reader = new StreamReader("input.txt");
        _writer = new StreamWriter("output.txt", false);
    }

    public void Run()
    {
        var table = new SalaryHashTable();

        foreach (string command in ReadCommands())
        {
            Match match = CommandRegex.Match(command);
            if (!match.Success)
                continue;

            string verb = match.Groups[1].Value;
            int key = int.Parse(match.Groups[2].Value);

            switch (verb)
            {
                case GetVerb:
                    DisplayValue(table.Get(key));
                    break;
                case PutVerb:
                    int value = int.Parse(match.Groups[3].Value);
                    table.Put(key, value);
                    break;
                case DeleteVerb:
                    DisplayValue(table.Delete(key));
                    break;
            }
        }
    }

    private void DisplayValue(int? value) => _writer.WriteLine(value?.ToString() ?? "None");

    private IEnumerable<string> ReadCommands()
    {
        int commandsCount = int.Parse(_reader.ReadLine()!);
        for (int i = 0; i < commandsCount; i++)
            yield return _reader.ReadLine()!;
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
