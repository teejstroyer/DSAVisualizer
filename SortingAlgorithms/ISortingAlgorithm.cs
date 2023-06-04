using System.Collections.Generic;

public interface ISortingAlgorithm
{
    public void Sort(int[] arr, int n, out List<int> comparrisons, out List<int> swaps);
}

