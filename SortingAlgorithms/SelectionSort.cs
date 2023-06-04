public class SelectionSort : ISortingAlgorithm
{
    public event Notify Swapped;
    public event Notify Compared;

    public void Sort(int[] arr, int n)
    {
        for (int i = 0; i < n - 1; i++)
        {
            int min = i;

            for (int j = i + 1; j < n; j++)
            {
                Compared?.Invoke(j, min);
                if (arr[j] < arr[min]) min = j;
            }

            Compared?.Invoke(i, min);
            if (min != i)
            {
                arr.Swap(i, min);
                Swapped?.Invoke(i, min);
            }
        }
    }
}
