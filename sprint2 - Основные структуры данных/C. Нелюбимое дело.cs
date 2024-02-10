/*
 Вася размышляет, что ему можно не делать из того списка дел, который он составил. Но, кажется, все пункты очень важные! Вася решает загадать число и удалить дело, которое идёт под этим номером. Список дел представлен в виде односвязного списка. Напишите функцию solution, которая принимает на вход голову списка и номер удаляемого дела и возвращает голову обновлённого списка.

Внимание: в этой задаче не нужно считывать входные данные. Нужно написать только функцию, которая принимает на вход голову списка и номер удаляемого элемента и возвращает голову обновлённого списка.

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
Функция принимает голову списка и индекс элемента, который надо удалить (нумерация с нуля). Список содержит не более 5000 элементов. Список не бывает пустым.

Инструкцию по работе с Make вы можете найти в конце этого урока
Формат вывода
Верните голову списка, в котором удален нужный элемент. 
*/
public class Node<TValue>
{
    public TValue Value { get; private set; }
    public Node<TValue> Next { get; set; }

    public Node(TValue value, Node<TValue> next)
    {
        Value = value;
        Next = next;
    }
}

public static Node<TValue> Solve(Node<TValue> head, int idx)
{
    if (idx == 0)
        return head.Next;
    var node = head;
    while (idx > 1)
    {
        node = node.Next;
        idx --;
    }
    node.Next = node.Next.Next;
    return head;
}