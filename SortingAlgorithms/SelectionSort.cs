public class SelectionSort : ISortingAlgorithm
{
    public event Notify ShouldDraw;

    public void Sort(int[] arr, int n, bool notifyComparisons = false)
    {
        for (int i = 0; i < n - 1; i++)
        {
            int min = i;

            for (int j = i + 1; j < n; j++)
            {
                if (notifyComparisons) ShouldDraw?.Invoke(new int[] { j, min });
                if (arr[j] < arr[min]) min = j;
            }

            if (notifyComparisons) ShouldDraw?.Invoke(new int[] { i, min });
            if (min != i)
            {
                arr.Swap(i, min);
                ShouldDraw?.Invoke(new int[] { i, min });
            }
        }
    }
}
