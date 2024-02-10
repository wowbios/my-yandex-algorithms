/*
 Вася решил запутать маму —– делать дела в обратном порядке. Список его дел теперь хранится в двусвязном списке. Напишите функцию, которая вернёт список в обратном порядке.

Внимание: в этой задаче не нужно считывать входные данные. Нужно написать только функцию, которая принимает на вход голову двусвязного списка и возвращает голову перевёрнутого списка.

Используйте заготовки кода для данной задачи, расположенные по ссылкам:

    c++
    Java
    js
    Python
    C#
    go
    Kotlin
    Swift

Формат ввода
Функция принимает на вход единственный аргумент — голову двусвязного списка.

Длина списка не превосходит 1000 элементов. Список не бывает пустым.

Инструкцию по работе с Make вы можете найти в конце этого урока
Формат вывода
Функция должна вернуть голову развернутого списка. 
*/
public class Node<TValue>
{
    public TValue Value { get; private set; }
    public Node<TValue> Next { get; set; }
    public Node<TValue> Prev { get; set; }

    public Node(TValue value, Node<TValue> next, Node<TValue> prev)
    {
        Value = value;
        Next = next;
        Prev = prev;
    }
}

public static Node<string> Solve(Node<string> head)
{
    var node = head;
    do
    {
        (node.Next, node.Prev) = (node.Prev, node = node.Next);
    }
    while (node.Next is not null);
    (node.Next, node.Prev) = (node.Prev, node.Next);
    return node;
}