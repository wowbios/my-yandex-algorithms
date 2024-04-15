using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Token = System.String;
using DocumentNumber = System.Int32;

public class SearchEngine
{
    /// <summary>
    /// Максимальное количество документов в ответе
    /// </summary>
    private const int MaximumDocumentsInResult = 5;

    /// <summary>
    /// Поисковый индекс. Каждому токену сопоставлен словарь [номер документа; количество повторений].
    /// </summary>
    private Dictionary<Token, Dictionary<DocumentNumber, int>> _index = new ();

    /// <summary>
    /// Загружает документы в поисковый индекс.
    /// </summary>
    /// <param name="documents">Массив документов</param>
    public void LoadDocuments(string[] documents)
    {
        ArgumentNullException.ThrowIfNull(documents);

        for (int i = 0; i < documents.Length; i++)
        {
            string document = documents[i];
            if (string.IsNullOrWhiteSpace(document))
                continue;
            
            LoadDocument(i + 1, document);
        }
    }

    /// <summary>
    /// Осуществляет поиск по запросу. Выводит <see cref="MaximumDocumentsInResult"/> релевантных документов.
    /// </summary>
    /// <param name="query">Поисковый запрос</param>
    /// <returns>Массив релеватных документов, отсортированный по релевантности и порядку загрузки</returns>
    public int[] Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Array.Empty<int>();
        
        var result = new Dictionary<DocumentNumber, int>();
        Token[] tokens = GetTokens(query);
        foreach (string token in tokens.Distinct())
        {
            if (!_index.TryGetValue(token, out Dictionary<int, int>? occurencies))
                continue;
            
            foreach ((DocumentNumber num, int occurenciesInDocument) in occurencies)
            {
                result.TryGetValue(num, out int total);
                result[num] = total + occurenciesInDocument;
            }
        }

        return (
                from pair in result
                where pair.Value > 0
                orderby pair.Key ascending
                orderby pair.Value descending
                select pair.Key
            )
            .Take(MaximumDocumentsInResult)
            .ToArray();
    }

    /// <summary>
    /// Загружает документ <paramref name="document"/> под номером <paramref name="documentNumber"/> в поисковый индекс.
    /// </summary>
    /// <param name="documentNumber">Номер документа</param>
    /// <param name="document">Докуент</param>
    private void LoadDocument(DocumentNumber documentNumber, string document)
    {
        Token[] tokens = GetTokens(document);

        foreach (Token token in tokens)
        {
            if (_index.TryGetValue(token, out Dictionary<int, int>? occurencies))
            {
                occurencies.TryGetValue(documentNumber, out int occurenciesInDocument);
                occurencies[documentNumber] = occurenciesInDocument + 1;
            }
            else
            {
                occurencies = new Dictionary<DocumentNumber, DocumentNumber>
                {
                    {documentNumber, 1}
                };
                
                _index[token] = occurencies;
            }
        }
    }

    /// <summary>
    /// Разбивает документ на токены.
    /// </summary>
    /// <param name="document">Документ</param>
    /// <returns>Массив токенов документа <paramref name="document"/></returns>
    private static Token[] GetTokens(string document) => document.Split(' ').ToArray();
}

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
        string[] documents = ReadDocuments();
        var engine = new SearchEngine();
        engine.LoadDocuments(documents);

        foreach (string query in ReadQueries())
        {
            int[] relatedDocuments = engine.Search(query);
            _writer.WriteLine(string.Join(" ", relatedDocuments));
        }
    }

    private IEnumerable<string> ReadQueries()
    {
        int queriesCount = int.Parse(_reader.ReadLine()!);
        for (int i = 0; i < queriesCount; i++)
            yield return _reader.ReadLine()!;
    }

    private string[] ReadDocuments()
    {
        int documentsCount = int.Parse(_reader.ReadLine()!);
        string[] documents = new string[documentsCount];
        for (int i = 0; i < documentsCount; i++)
            documents[i] = _reader.ReadLine()!;

        return documents;
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
