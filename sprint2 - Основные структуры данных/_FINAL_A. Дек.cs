/*
Гоша реализовал структуру данных Дек, максимальный размер которого определяется заданным числом. Методы push_back(x), push_front(x), pop_back(), pop_front() работали корректно. Но, если в деке было много элементов, программа работала очень долго. Дело в том, что не все операции выполнялись за O(1). Помогите Гоше! Напишите эффективную реализацию.

Внимание: при реализации используйте кольцевой буфер.
Формат ввода

В первой строке записано количество команд n — целое число, не превосходящее 100000. Во второй строке записано число m — максимальный размер дека. Он не превосходит 50000. В следующих n строках записана одна из команд:

    push_back(value) – добавить элемент в конец дека. Если в деке уже находится максимальное число элементов, вывести «error».
    push_front(value) – добавить элемент в начало дека. Если в деке уже находится максимальное число элементов, вывести «error».
    pop_front() – вывести первый элемент дека и удалить его. Если дек был пуст, то вывести «error».
    pop_back() – вывести последний элемент дека и удалить его. Если дек был пуст, то вывести «error».

Value — целое число, по модулю не превосходящее 1000.

Формат вывода

Выведите результат выполнения каждой команды на отдельной строке. Для успешных запросов push_back(x) и push_front(x) ничего выводить не надо. 
*/

/*
https://contest.yandex.ru/contest/22781/run-report/107003964/
-- ПРИНЦИП РАБОТЫ --
Я реализовал дек на кольцевом буфере, т.к. это требовалось в задаче. Отличительной сложностью от кольцевого буфера для реализации очереди было требования реализовать двустороннюю очередь (дек) на одном единственном буфере.
Буфер реализован в виде массива фиксированной длины, которая подаётся на вход программы.
Работа дека осуществляется за счёт двух указателей _front (для "головы") и _back (для "хвоста"). На протяжении всей работы дека, отслеживается показатель _size - кол-во элементов в деке.
Для обеспечения корректной работы, чтобы указатели не конфликтовали, было принято решение сделать их поведение зеркальным:
_front:
    1. при добавлении двигается вперед (+1)
    2. всегда указывает на следующее свободное место в буфере (по направлению front)
    3. при вставке - сначала устанавливает значение, потом сдвигается
    4. при изъятии - сначала сдвигается, потом отдаёт значение с новой позиции
_back:
    1. при добавлении двигается назад (-1)
    2. всегда указывает на последний, добавленный в конец, элемент
    3. при вставке - сначала сдвигается, потом устанавливает значение
    4. при изъятии - сначала отдаёт значение, потом  сдвигается на новую позицию

Визуализацию размышлений приложил в файле source1.png.

На мысли натолкнула подсказка из постановки задачи и удобная теория с визуализацией из материалов обучения.

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Исходя из описания структуры "дек" в нём есть как будто бы два стека. При реализации через кольцевой буфер, можно представить, что эти два стека "начинаются" в едином месте (_front = _back при старте), но потом разрастаются в противоположные стороны. При достижении макс. размера буфера, указатели сталкиваются\пересекаются (_front = _back) и эти два "стека" запрещают наполнение дека. При изъятии элементов, один из "стеков" уменьшается, т.о. освобождая место.
Можно сказать по-другому, что это два стека, расположенные в одном участке памяти и память одного "стека" не может "залезть" на память второго "стека".

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
все операции над деком будут выполняться за О(1), т.к. в процессе работы используются вспомогательные указатели, благодаря которым известно в каком месте буфера будет производиться операция изъятия\вставки
сама операция изъятия\вставки в массиве выполняется за константное время и равно времени обращения к ячейке массива по конкретному индексу

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
С учётом реализации, память будут занимать буфер, вспомогательные указатели и maxSize.

Пространственная сложность = O(N), где N - максимальный размер дека. + константная память на вспомогательные структуры, которая не учитывается при оценке данной сложности

*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

public sealed class Deck<T>
{
    private readonly T[] _buffer;
    private readonly int _maxSize;
    private int _size;
    private int _front;
    private int _back;

    public Deck(int maxSize)
    {
        if (maxSize < 0)
            throw new ArgumentException("Maximum size must be 0 or positive");

        _maxSize = maxSize;
        _buffer = new T[_maxSize];
    }

    public bool TryPushBack(T value)
    {
        if (IsFull())
            return false;

        _back = MovePointerBackward(_back);
        _buffer[_back] = value;
        _size++;
        return true;
    }

    public bool TryPushFront(T value)
    {
        if (IsFull())
            return false;

        _buffer[_front] = value;
        _front = MovePointerForward(_front);
        _size++;
        return true;
    }

    public bool TryPopFront(out T? value)
    {
        value = default;
        if (IsEmpty())
            return false;

        _front = MovePointerBackward(_front);
        value = _buffer[_front];
        _size--;
        return true;
    }

    public bool TryPopBack(out T? value)
    {
        value = default;
        if (IsEmpty())
            return false;

        value = _buffer[_back];
        _back = MovePointerForward(_back);
        _size--;
        return true;
    }

    private bool IsFull() => _size == _maxSize;

    private bool IsEmpty() => _size == 0;

    private int MovePointerForward(int pointer) => (pointer + _maxSize + 1) % _maxSize;

    private int MovePointerBackward(int pointer) => (pointer + _maxSize - 1) % _maxSize;
}

public class Program : IDisposable
{
    private const string PopBackCommand = "pop_back";
    private const string PopFrontCommand = "pop_front";
    private const string PushFrontCommand = "push_front";
    private const string PushBackCommand = "push_back";
    private const RegexOptions DefaultRegexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
    private static readonly Regex _pushBackRegex = new(@"push_back (-?\d+)", DefaultRegexOptions);
    private static readonly Regex _pushFrontRegex = new(@"push_front (-?\d+)", DefaultRegexOptions);
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;
    private readonly Deck<int> _deck;
    private readonly int _commandsCount;

    public Program()
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        (_commandsCount, int deckSize) = ReadInput();
        _deck = new(deckSize);
    }

    public void Run()
    {
        Match match;
        for (int i = 0; i < _commandsCount; i++)
        {
            string command = _reader.ReadLine()!;
            switch (command)
            {
                case PopFrontCommand:
                    Output(_deck.TryPopFront(out int frontValue), frontValue);
                    break;
                case PopBackCommand:
                    Output(_deck.TryPopBack(out int backValue), backValue);
                    break;
                case var x when x.StartsWith(PushFrontCommand) && (match = _pushFrontRegex.Match(x)).Success:
                    var pushFrontValue = int.Parse(match.Groups[1].Value);
                    OutputIfError(_deck.TryPushFront(pushFrontValue));
                    break;
                case var x when x.StartsWith(PushBackCommand) && (match = _pushBackRegex.Match(x)).Success:
                    var pushBackValue = int.Parse(match.Groups[1].Value);
                    OutputIfError(_deck.TryPushBack(pushBackValue));
                    break;
            }
        }
    }

    private void OutputIfError(bool isSuccess)
    {
        if (!isSuccess)
            _writer.WriteLine("error");
    }

    private void Output(bool isSuccess, int value)
        => _writer.WriteLine(isSuccess ? value : "error");

    private (int commandsCount, int deckSize) ReadInput()
        => (int.Parse(_reader.ReadLine()!), int.Parse(_reader.ReadLine()!));

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