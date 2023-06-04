public class InsertionSort : ISortingAlgorithm
{
    public event Notify Swapped;
    public event Notify Compared;

    public void Sort(int[] arr, int n)
    {
        for(int i = 1; i < n; i++)
        {
            for(int j = i; j>0 && arr[j] < arr[j-1]; j--)
            {
                arr.Swap(j, j-1);
                Compared?.Invoke(j, j-1);
                Swapped?.Invoke(j, j-1);
            }
        }
    }
}
