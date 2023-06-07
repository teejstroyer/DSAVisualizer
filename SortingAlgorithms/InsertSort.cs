public class InsertionSort : ISortingAlgorithm
{
    public event Notify ShouldDraw;

    public void Sort(int[] arr, int n, bool notifyComparisons = false)
    {
        for(int i = 1; i < n; i++)
        {
            for(int j = i; j>0 && arr[j] < arr[j-1]; j--)
            {
                arr.Swap(j, j-1);
                ShouldDraw?.Invoke(new int[] { j, j-1 });
            }
        }
    }
}
