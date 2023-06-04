using System.Collections.Generic;

public class InsertionSort : ISortingAlgorithm
{
    public void Sort(int[] arr, int n, out List<int> comparrisons, out List<int> swaps)
    {
        comparrisons = new List<int>();
        swaps = new List<int>();

        for(int i = 1; i < n; i++)
        {
            for(int j = i; j>0 && arr[j] < arr[j-1]; j--)
            {
                arr.Swap(j, j-1);
                comparrisons.Add(j);
                comparrisons.Add(j-1);
                swaps.Add(j);
                swaps.Add(j-1);
            }
        }
    }
}
