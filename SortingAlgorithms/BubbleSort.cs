using System.Collections.Generic;

public  class BubbleSort : ISortingAlgorithm
{
    public void Sort(int[] arr, int n, out List<int> comparrisons, out List<int> swaps)
    {
        swaps = new List<int>();
        comparrisons = new List<int>();

        int i, j;
        bool swapped;
        for (i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (j = 0; j < n - i - 1; j++)
            {
                comparrisons.Add(j);
                comparrisons.Add(j+1);
                if (arr[j] > arr[j + 1])
                {

                    arr.Swap(j, j + 1);
                    swaps.Add(j+1);
                    swaps.Add(j);
                    swapped = true;
                }
            }
            if (swapped == false) break;
        }

    }
}
