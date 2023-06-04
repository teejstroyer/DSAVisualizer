using System.Collections.Generic;

public enum SortingAlgorithms
{
    None,
    BubbleSort,
    InsertionSort,
    SelectionSort,
    MergeSort,
    BottomUpMergeSort,
    QuickSort,
}


public sealed class SortingAlgorithmDictionary
{
    private Dictionary<SortingAlgorithms,ISortingAlgorithm> Dictionary = new Dictionary<SortingAlgorithms, ISortingAlgorithm>()
    {
        {SortingAlgorithms.BubbleSort, new BubbleSort()},
        {SortingAlgorithms.InsertionSort, new InsertionSort()},
        {SortingAlgorithms.SelectionSort, new SelectionSort()},
    };

    private static readonly SortingAlgorithmDictionary _instance = new SortingAlgorithmDictionary();

    public static SortingAlgorithmDictionary Instance { get  => _instance; }

    public ISortingAlgorithm this[SortingAlgorithms key]
    {
        get
        {
            return Dictionary[key];
        }
    }
}

