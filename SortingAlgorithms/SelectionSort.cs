
using System.Collections.Generic;

public class SelectionSort : ISortingAlgorithm
{

    public void Sort(int[] arr, int n, List<int> comparrisons, List<int> swaps)
    {
    }

    public void Sort(int[] arr, int n, out List<int> comparrisons, out List<int> swaps)
    {
        comparrisons = new List<int>();
        swaps = new List<int>();

        for(int i = 0; i < n-1; i++)
        {
            int min = i;

            for(int j = i+1; j < n; j++)
            {
                comparrisons.Add(i);
                comparrisons.Add(j);
                if(arr[j] < arr[min]) min = j;
            }

            comparrisons.Add(i);
            comparrisons.Add(min);
            if(min != i)
            {
                arr.Swap(i, min);
                swaps.Add(i);
                swaps.Add(min);
            }
        }
    }
}
