using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using SAG = SortingAlgorithmDictionary;

public partial class SortingAlgorithm : Node2D
{
    [Export]
    public int Count { get; set; } = 100;

    [Export]
    public bool UseViewPortSize { get; set; } = true;

    [Export]
    public Vector2 Size { get; set; } = new Vector2 { X = 300, Y = 300 };

    [Export]
    public Label StatLabel { get; set; }

    [Export]
    public bool CompareSwaps { get; set; }

    [Export]
    public double DelayInSeconds { get; set; } = .25;

    [Export]
    public Button SpeedDownButton { get; set; }

    [Export]
    public Button SpeedUpButton { get; set; }

    [Export]
    public OptionButton AlgorithmList { get; set; }

    [Export]
    public OptionButton SwapsOrComparisons { get; set; }

    private float _width;
    private float _height;
    private int[] _data = new int[0];
    private List<int> _comparrisons = new List<int>();
    private List<int> _swaps = new List<int>();
    private double _timeSinceLastDelay = 0;
    private int _currentSwap = 0;
    private int _currentComparrison = 0;
    private int _pointer1 = 0;
    private int _pointer2 = 0;

    public override void _Ready()
    {
        Engine.MaxFps = 0;
        if (UseViewPortSize)
        {
            Size = GetViewportRect().Size;
        }

        _width = Size.X;
        _height = Size.Y;

        SpeedDownButton.Pressed += () =>
        {
            if (DelayInSeconds <= 0) SpeedUpButton.Disabled = false;

            DelayInSeconds = DelayInSeconds + .05 > 1 ? 1 : DelayInSeconds + .05;

            if (DelayInSeconds >= 1) SpeedDownButton.Disabled = true;
        };

        SpeedUpButton.Pressed += () =>
        {
            if (DelayInSeconds >= 1) SpeedDownButton.Disabled = false;

            DelayInSeconds = DelayInSeconds - .05 < 0 ? 0 : DelayInSeconds - .05;

            if (DelayInSeconds <= 0) SpeedUpButton.Disabled = true;
        };

        foreach (var algo in Enum.GetValues(typeof(SortingAlgorithms)))
        {
            AlgorithmList.AddItem(algo.ToString());
        }

        AlgorithmList.ItemSelected += (long index) =>
        {
            SortingAlgorithms algorithmType = (SortingAlgorithms)index;
            Reset(algorithmType);
        };

        SwapsOrComparisons.AddItem("Swaps");
        SwapsOrComparisons.AddItem("Comparisons");
        SwapsOrComparisons.Selected = CompareSwaps ? 0 : 1;

        SwapsOrComparisons.ItemSelected += (long index) =>
        {
            CompareSwaps = index == 0;
            Reset(AlgorithmList.Selected == -1 ? SortingAlgorithms.None : (SortingAlgorithms)AlgorithmList.Selected);
        };
    }

    private void Reset(SortingAlgorithms algorithmType)
    {
        InitializeDataAndShuffle();

        if (algorithmType != SortingAlgorithms.None)
        {
            SAG.Instance[algorithmType].Sort(_data.Clone() as int[], _data.Length, out _comparrisons, out _swaps);
            StatLabel.Text = $"{algorithmType.ToString()} {Count}ct "
            + $"Swaps: {_swaps.Count / 2} "
            + $"Comparrisons: {_comparrisons.Count / 2}";
        }
        else
        {
            StatLabel.Text = "Select an algorithm";
        }
    }


    private void InitializeDataAndShuffle()
    {
        _comparrisons.Clear();
        _swaps.Clear();
        _currentSwap = 0;
        _currentComparrison = 0;
        _data = Enumerable.Range(0, Count).ToArray();
        var random = new Random();
        for (int i = 0; i < Count; i++)
        {
            var j = random.Next(0, Count);
            var temp = _data[i];
            _data[i] = _data[j];
            _data[j] = temp;
        }
    }

    public override void _Process(double delta)
    {
        _timeSinceLastDelay += delta;
        if (_timeSinceLastDelay >= DelayInSeconds && _currentSwap < _swaps.Count && _currentComparrison < _comparrisons.Count)
        {
            _timeSinceLastDelay = 0;
            QueueRedraw();

            var i = _swaps[_currentSwap];
            var j = _swaps[_currentSwap + 1];

            _pointer1 = CompareSwaps ? i : _comparrisons[_currentComparrison];
            _pointer2 = CompareSwaps ? j : _comparrisons[_currentComparrison + 1];

            if (CompareSwaps)
            {
                //No need to keep up with comparrisons if we only care about swaps
                _currentSwap += 2;
                this.Swap(_data, i, j);
                return;
            }

            if ((_comparrisons[_currentComparrison + 1] == i && _comparrisons[_currentComparrison] == j)
                || (_comparrisons[_currentComparrison + 1] == j && _comparrisons[_currentComparrison] == i))
            {
                _currentSwap += 2;
                this.Swap(_data, i, j);
            }
            _currentComparrison += 2;

        }
    }

    public override void _Draw()
    {
        if (_data.Length == 0 || _data == null) return;
        var width = _width / Count * .8;
        var spacer = _width / Count * .2;

        for (int i = 0; i < Count; i++)
        {
            var height = (_data[i] + 1) / ((float)Count + 1) * _height;
            var x = i * (width + spacer);
            var y = _height - height;

            if (i > 2 && (i == _pointer1 || i == _pointer2))
            {
                DrawRect(new Rect2((float)x - (float)spacer, 0, (float)width + (float)(2 * spacer), _height), Colors.Red);
            }

            DrawRect(new Rect2((float)x, y, (float)width, height), Colors.White);
        }
    }

    public void Swap(int[] arr, int i, int j)
    {
        if (i != j)
        {
            arr[i] ^= arr[j];
            arr[j] ^= arr[i];
            arr[i] ^= arr[j];
        }
    }
}

