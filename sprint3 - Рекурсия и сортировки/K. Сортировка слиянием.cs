/*
Гоше дали задание написать красивую сортировку слиянием. Поэтому Гоше обязательно надо реализовать отдельно функцию merge и функцию merge_sort.

    Функция merge принимает один массив и три целочисленных индекса: left, mid, и right. Функция сливает две отсортированные части одного и того же массива в один отсортированный массив. Первая часть массива определяется полуинтервалом [left,mid) массива array, а вторая часть – полуинтервалом [mid,right) того же массива array. Функция возвращает сливаемый массив.
    Функция merge_sort принимает некоторый подмассив, который нужно отсортировать. Подмассив задаётся полуинтервалом — его началом и концом. Функция должна отсортировать передаваемый в неё подмассив, она ничего не возвращает.
    Функция merge_sort разбивает полуинтервал на две половинки и рекурсивно вызывает сортировку отдельно для каждой. Затем два отсортированных массива сливаются в один с помощью merge.

Заметьте, что в функции передаются именно полуинтервалы [begin,end), то есть правый конец не включается. Например, если вызвать merge_sort(arr, 0, 4), где arr=[4,5,3,0,1,2], то будут отсортированы только первые четыре элемента, изменённый массив будет выглядеть как arr=[0,3,4,5,1,2].

Реализуйте эти две функции. 

Формат ввода
Передаваемый в функции массив состоит из целых чисел, не превосходящих по модулю 109. Длина сортируемого диапазона не превосходит 105. 
*/

using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public static void MergeSort(List<int> array, int left, int right)
    {
        if (right - left is 0 or 1)
            return;

        if (right - left == 2)
        {
            if (array[left+1] < array[left])
                (array[left+1], array[left]) = (array[left], array[left+1]);
            return;
        }

        int mid = (right + left) / 2;
        MergeSort(array, left, mid);
        MergeSort(array, mid, right);
        Merge(array, left, mid, right);
    }

    public static List<int> Merge(List<int> array, int left, int mid, int right)
    {
        var subLeft = new int[mid-left];
        var subRight = new int[right-mid];
        array.CopyTo(left, subLeft, 0, subLeft.Length);
        array.CopyTo(mid, subRight, 0, subRight.Length);
        int l = 0;
        int r = 0;
        
        int i = left;
        while (l < subLeft.Length && r < subRight.Length)
        {
            array[i++] = subLeft[l] > subRight[r]
                ? subRight[r++]
                : subLeft[l++];
        }

        while (l < subLeft.Length)
            array[i++] = subLeft[l++];
        while(r < subRight.Length)
            array[i++] = subRight[r++];
        
        return array;
    }

    #if !REMOTE_JUDGE
    public static void M(string[] args)
    {
        var a = new List<int> { 1, 4, 9, 2, 10, 11 };
        var b = Merge(a, 0, 3, 6);
        var expectedMergeResult = new List<int> {1, 2, 4, 9, 10, 11};
        System.Console.WriteLine(b.SequenceEqual(expectedMergeResult));
        var c = new List<int> {1, 4, 2, 10, 1, 2};
        MergeSort(c, 0, 6);
        var expectedMergeSortResult = new List<int> {1, 1, 2, 2, 4, 10};
        System.Console.WriteLine(c.SequenceEqual(expectedMergeSortResult));
    }
    #endif
}