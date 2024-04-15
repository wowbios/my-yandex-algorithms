using System.Collections.Generic;

public class Solution
{
    public static int SiftDown(List<int> array, int idx)
    {
        int leftChild = idx * 2;
        int rightChild = idx * 2 + 1;

        if (leftChild >= array.Count)
            return idx;
        
        int largestIndex = (rightChild < array.Count && array[rightChild] > array[leftChild]) ? rightChild : leftChild;
        if (array[largestIndex] > array[idx])
        {
            (array[largestIndex], array[idx]) = (array[idx], array[largestIndex]);
            return SiftDown(array, largestIndex);
        }
        return idx;
    }

    #if !REMOTE_JUDGE
    private static void M()
    {
        var sample = new List<int> {-1, 12, 1, 8, 3, 4, 7};
        System.Console.WriteLine(SiftDown(sample, 2) == 5);
    }
    #endif
}
