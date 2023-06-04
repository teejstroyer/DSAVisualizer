
public class BubbleSort : ISortingAlgorithm
{
    public event Notify Swapped;
    public event Notify Compared;

    public void Sort(int[] arr, int n)
    {
        int i, j;
        bool swapped;
        for (i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (j = 0; j < n - i - 1; j++)
            {
                Compared?.Invoke(j, j + 1);

                if (arr[j] > arr[j + 1])
                {

                    arr.Swap(j, j + 1);
                    Swapped?.Invoke(j, j + 1);
                    swapped = true;
                }
            }
            if (swapped == false) break;
        }

    }
}
