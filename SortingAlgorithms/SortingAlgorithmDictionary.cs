using System.Collections.Generic;
using System.Linq;

public enum SortingAlgorithmType
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
    private readonly Dictionary<SortingAlgorithmType, ISortingAlgorithm> Dictionary = new Dictionary<SortingAlgorithmType, ISortingAlgorithm>()
    {
        {SortingAlgorithmType.BubbleSort, new BubbleSort()},
        {SortingAlgorithmType.InsertionSort, new InsertionSort()},
        {SortingAlgorithmType.SelectionSort, new SelectionSort()},
    };

    private static readonly SortingAlgorithmDictionary _instance = new SortingAlgorithmDictionary();

    public static SortingAlgorithmDictionary Instance { get => _instance; }

    public ISortingAlgorithm this[SortingAlgorithmType key]
    {
        get
        {
            return Dictionary[key];
        }
    }
    public IEnumerable<SortingAlgorithmType> Keys { get => Dictionary.Keys.OrderBy(i => i); }
}

