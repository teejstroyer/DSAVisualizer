public delegate void Notify(int i, int j);

public interface ISortingAlgorithm
{
    public void Sort(int[] arr, int n);
    public event Notify Swapped;
    public event Notify Compared;
}

