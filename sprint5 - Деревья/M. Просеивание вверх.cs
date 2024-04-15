using System.Collections.Generic;

public class Solution
{
    public static int SiftUp(List<int> array, int idx)
    {
        if (idx == 1)
            return 1;
        int parentIndex = idx / 2;
        if (array[idx] > array[parentIndex])
        {
            (array[idx], array[parentIndex]) = (array[parentIndex], array[idx]);
            return SiftUp(array, parentIndex);
        }
        return idx;
    }

    #if !REMOTE_JUDGE
    public static void M()
    {
        var sample = new List<int> {-1, 12, 6, 8, 3, 15, 7};
        System.Console.WriteLine(SiftUp(sample, 5) == 1);
    }
    #endif
}
