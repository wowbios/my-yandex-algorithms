/*
Задание связано с обратной польской нотацией. Она используется для парсинга арифметических выражений. Еще её иногда называют постфиксной нотацией.

В постфиксной нотации операнды расположены перед знаками операций.

Пример 1:
3 4 +
означает 3 + 4 и равно 7

Пример 2:
12 5 /
Так как деление целочисленное, то в результате получим 2.

Пример 3:
10 2 4 * -
означает 10 - 2 * 4 и равно 2

Разберём последний пример подробнее:

Знак * стоит сразу после чисел 2 и 4, значит к ним нужно применить операцию, которую этот знак обозначает, то есть перемножить эти два числа. В результате получим 8.

После этого выражение приобретёт вид:

10 8 -

Операцию «минус» нужно применить к двум идущим перед ней числам, то есть 10 и 8. В итоге получаем 2.

Рассмотрим алгоритм более подробно. Для его реализации будем использовать стек.

Для вычисления значения выражения, записанного в обратной польской нотации, нужно считывать выражение слева направо и придерживаться следующих шагов:

    Обработка входного символа:
        Если на вход подан операнд, он помещается на вершину стека.
        Если на вход подан знак операции, то эта операция выполняется над требуемым количеством значений, взятых из стека в порядке добавления. Результат выполненной операции помещается на вершину стека.

    Если входной набор символов обработан не полностью, перейти к шагу 1.
    После полной обработки входного набора символов результат вычисления выражения находится в вершине стека. Если в стеке осталось несколько чисел, то надо вывести только верхний элемент.

Замечание про отрицательные числа и деление: в этой задаче под делением понимается математическое целочисленное деление. Это значит, что округление всегда происходит вниз. А именно: если a / b = c, то b ⋅ c — это наибольшее число, которое не превосходит a и одновременно делится без остатка на b.

Например, -1 / 3 = -1. Будьте осторожны: в C++, Java и Go, например, деление чисел работает иначе.

В текущей задаче гарантируется, что деления на отрицательное число нет.
Формат ввода

В единственной строке дано выражение, записанное в обратной польской нотации. Числа и арифметические операции записаны через пробел.

На вход могут подаваться операции: +, -, *, / и числа, по модулю не превосходящие 10000.

Гарантируется, что значение промежуточных выражений в тестовых данных по модулю не больше 50000.
Формат вывода

Выведите единственное число — значение выражения.
*/
/*
https://contest.yandex.ru/contest/22781/run-report/107041651/
-- ПРИНЦИП РАБОТЫ --
Я уже сталкивался с такой нотацией в работе (в программе заводилась формула по разным полям документа, с арифм. операциями и поддержкой скобок).
Пример для поля "Всего листов": значение = "{Кол-во листов документа} + {Кол-во листов приложений}". Более сложные формулы обычно касались финансовых подсчётов.
Я раскрывал скобки с помощью перевода в постфиксную нотацию (Алгоритм Дейкстра) и потом считал результат через стек (популярное решение). Когда встречал "поле документа", заменял его на конкретное значение.
В данном случае у нас есть запись в обратной польской нотации, считать её можно слева направо, нам не нужные будущие элементы, когда встречаем какую-либо операцию.
Алгоритм такой:
1. если на входе число, помещаем его в стек
2. если операция - извлекаем последовательно два числа из стека и применяем операцию к этим числам. Важно учесть порядок чисел, т.к. не все арифметические операции коммутативны (свойства операций из алгебры) - поэтому в алгоритме сначала изымается второе число, потом первое (TryPop(second) && TryPop(first)).

В конце работы алгоритма в стеке останется один элемент - он и будет являться результатом работы/ответом.

Теорию для объяснения еще подсмотрел тут: https://habr.com/ru/articles/596925/

UPD: Структура стек организована через массив с указателем. Обычно принято делать через односвязный список, но такое решение я уже делал в задачах спринта. В данном случае решил попробовать реализовать через массив, который может расширяться при необходимости (аллокация нового, в 2 раза больше. Как например сделано у List<T>). При реализации через массив  Цитата со StackOverflow (https://stackoverflow.com/questions/2247773/is-it-worthwhile-to-initialize-the-collection-size-of-a-listt-if-its-size-rea): ... the array is full and needs to be reallocated again. It doubles the size. The previous array is garbage.
Список причин, почему в .NET, например очередь, сделана не на двусвязном списке, а на массиве: https://stackoverflow.com/questions/18366318/why-does-the-underlying-implementation-of-the-queue-in-net-use-an-array 

Одна из ключевых причин - потребление памяти, затрата на ссылки по сравнению со структурами: It's not just a bit more compact, a doubly-linked list of reference types of common value types (int) is a whole three times larger than the equivalent array, without even taking per-object allocation overhead into account. If we are honest and takes those into account, we must add a per-node header of one or two words, so it's more like 4x or 5x larger. This dwarfs any over-allocation the array may have.

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Обратная польская нотация - вариант постфиксной нотации и формируется из привычной нам записи простым перемещением членов выражения:
число1 операция число2 => число1 число2 операция

Работу алгоритма можно воспринимать как растущую кучу чисел, которая уменьшается на 1 число, когда на вход подается оператор, т.к. он осуществляет "слияние" двух чисел в одно.
для записи "N K M operator" будет следующий алгоритм
[N] => [N,K] => [N,K,M] => (operator) => [N,P], где P = K operator M

Отсюда можно сделать вывод, что кол-во операторов (O) всегда равно N-1, где N - кол-во чисел (D).
И тогда можно выявить такую закономерность и сразу понять кол-во операторов и чисел исходя из длины последовательности:
X = D + O = (N) + (N - 1) = 2N - 1,
D = (X + 1) / 2
O = (( X + 1 ) / 2) - 1
где 
X - кол-во элементов последовательности
D, N - кол-во чисел
O - кол-во операторов

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Операции со стеком занимают O(1). В худшем случае, при необходимости расширения буфера, O(N), т.к. надо будет переместить все элементы предыдущего буфера в новый. Несмотря на то, что принято считать Array.Copy быстрой функцией, она в общем случае выполняется за O(N). 
Кол-во операций со стеком равно кол-ву операций в нотации. Выше я уже посчитал как это можно выразить, тогда можно сделать такой вывод:
Временную сложность можно выразить так O(((N+1)/2)-1) => O(N/2) => O(N), где N - кол-во выражений(чисел и операций) в нотации.

UPD:
Кол-во реаллокаций массива будет значительно меньше N, поэтому можно всё еще считать операции со стеком за O(1), тогда, считаю что временная сложность остаётся O(N).
+ в зада

Справка на MSDN по поводу Array.Copy с рассмотрением разных кейсов:
https://learn.microsoft.com/en-us/dotnet/api/system.array.copy?view=net-8.0
This method is equivalent to the standard C/C++ function memmove, not memcpy.
А сложность memmove зависит от кол-ва перемещанмых байт

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Память в данном случае варьируется только для стека и в худшем случае может равняться кол-ву подряд идущих чисел. В данной нотации их может быть очень много, просто в конце обязано быть много операторов:
1 1 1 1 1 ... - - - + * * -

В отличие от временной сложности, пространственная здесь будет зависеть от кол-ва чисел в нотации. С учётом выведенных формул выше, можно выразить так O((N + 1) / 2) => O(N/2) => O(N), где N - кол-во элементов(операций и чисел) в нотации

P.S.: спасибо, что требуете и проверяете такое объяснение. Действительно расставляет на места мысли и, порой интуитивные, выводы.
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

public sealed class MyStack<T> where T : struct
{
    private const int NoValuePointer = -1;
    private const int DefaultCapacity = 10;
    public int _valuePointer = NoValuePointer;
    private T[] _buffer;

    public MyStack() : this(DefaultCapacity) {}

    public MyStack(int capacity)
    {
        if (capacity is <= 0)
            throw new ArgumentException("Capacity must be positive");

        Capacity = capacity;
        _buffer = new T[capacity];
    }

    public int Capacity {get; private set;} = DefaultCapacity;

    public void Push(T value)
    {
        if (_valuePointer == _buffer.Length - 1)
            ExpandBuffer();
        
        _buffer[++_valuePointer] = value;
    }

    public T Pop()
    {
        if (!TryPop(out T value))
            throw new InvalidOperationException("No more values in stack");
        
        return value;
    }

    public bool TryPop(out T value)
    {
        value = default;
        if (_valuePointer is NoValuePointer)
            return false;
        
        value = _buffer[_valuePointer--];
        return true;
    }

    private void ExpandBuffer()
    {
        Capacity *= 2;
        T[] newBuffer = new T[Capacity];
        Array.Copy(_buffer, 0, newBuffer, 0, _buffer.Length);
        _buffer = newBuffer;
    }
}

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
        string[] expressions = ReadInput();
        var stack = new MyStack<int>(expressions.Length);

        int first = 0, second = 0;

        bool TryPopOperands() => stack.TryPop(out second) && stack.TryPop(out first);

        foreach (var expression in expressions)
        {
            int result = expression switch 
            {
                "+" when TryPopOperands() => first + second,
                "-" when TryPopOperands() => first - second,
                "*" when TryPopOperands() => first * second,
                "/" when TryPopOperands() => (int)Math.Floor(first / (decimal)second),
                _ => int.Parse(expression)
            };
            stack.Push(result);
        }

        _writer.WriteLine(stack.Pop());
    }

    private string[] ReadInput()
        => _reader.ReadLine()!.Split(' ').ToArray();

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