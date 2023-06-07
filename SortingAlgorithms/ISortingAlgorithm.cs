public delegate void Notify(int[] i);

public interface ISortingAlgorithm
{
    public void Sort(int[] arr, int n, bool notifyComparisons = false);
    public event Notify ShouldDraw;
}

