using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAG = SortingAlgorithmDictionary;

public partial class SortingAlgorithm : Node2D
{
    [Export] public SpinBox CountInput { get; set; }
    [Export] public Button ResetButton { get; set; }
    [Export] public bool UseViewPortSize { get; set; } = true;
    [Export] public Vector2 Size { get; set; } = new Vector2 { X = 300, Y = 300 };
    [Export] public Label StatLabel { get; set; }
    [Export] public bool CompareSwapsOnly { get; set; }
    [Export] public double DelayInSeconds { get; set; } = .25;
    [Export] public Button SpeedDownButton { get; set; }
    [Export] public Button SpeedUpButton { get; set; }
    [Export] public OptionButton AlgorithmList { get; set; }
    [Export] public OptionButton SwapsOrComparisons { get; set; }

    private ISortingAlgorithm _algorithm;
    private int N { get => (int)CountInput.Value; }
    private float _width;
    private float _height;
    private int[] _data = new int[0];
    private List<int> _pointers = new List<int>();

    public override void _Ready()
    {
        Engine.MaxFps = 0;
        if (UseViewPortSize)
        {
            Size = GetViewportRect().Size;
        }

        _width = Size.X;
        _height = Size.Y;

        InitializeHandlers();
    }

    private void InitializeHandlers()
    {
        double delayInc = .05;
        SpeedDownButton.Pressed += () =>
        {
            if (DelayInSeconds <= delayInc) SpeedUpButton.Disabled = false;
            DelayInSeconds = DelayInSeconds + delayInc > 1 ? 1 : DelayInSeconds + delayInc;
            if (DelayInSeconds >= 1) SpeedDownButton.Disabled = true;
        };

        SpeedUpButton.Pressed += () =>
        {
            if (DelayInSeconds >= 1) SpeedDownButton.Disabled = false;
            DelayInSeconds = DelayInSeconds - delayInc < delayInc ? delayInc : DelayInSeconds - .02;
            if (DelayInSeconds <= delayInc) SpeedUpButton.Disabled = true;
        };

        SAG.Instance.Keys.ToList().ForEach(i => AlgorithmList.AddItem(i.ToString()));
        AlgorithmList.Selected = -1;
        AlgorithmList.ItemSelected += (long index) =>
        {
            if (Enum.TryParse(AlgorithmList.Text, out SortingAlgorithmType algorithmType))
            {
                Reset(algorithmType);
            }
        };

        SwapsOrComparisons.AddItem("Swaps");
        SwapsOrComparisons.AddItem("Comparisons");
        SwapsOrComparisons.Selected = CompareSwapsOnly ? 0 : 1;
        SwapsOrComparisons.ItemSelected += (long index) =>
        {
            CompareSwapsOnly = index == 0;
            if (Enum.TryParse(AlgorithmList.Text, out SortingAlgorithmType algorithmType))
            {
                Reset(algorithmType);
            }
        };

        ResetButton.Pressed += () =>
        {
            if (Enum.TryParse(AlgorithmList.Text, out SortingAlgorithmType algorithmType))
            {
                Reset(algorithmType);
            }
        };
    }


    private void HandleDraw(int[] i)
    {
        _pointers.Clear();
        _pointers.AddRange(i);
        Task.Delay((int)(DelayInSeconds * 1000)).Wait();
        QueueRedraw();
    }

    private void InitializeArrayAndShuffle()
    {
        _data = Enumerable.Range(0, N).ToArray();
        var random = new Random();
        for (int i = 0; i < N; i++)
        {
            var j = random.Next(0, N);
            var temp = _data[i];
            _data[i] = _data[j];
            _data[j] = temp;
        }
    }

    private void Reset(SortingAlgorithmType algorithmType)
    {
        if (_algorithm != null)
        {
            _algorithm.ShouldDraw -= HandleDraw;
        }

        InitializeArrayAndShuffle();

        _pointers.Clear();

        if (algorithmType != SortingAlgorithmType.None)
        {
            _algorithm = SAG.Instance[algorithmType];
            _algorithm.ShouldDraw += HandleDraw;
            Task.Run(() => _algorithm.Sort(_data, _data.Length, !CompareSwapsOnly)).ConfigureAwait(false);
        }
        else
        {
            StatLabel.Text = "Select an algorithm";
        }

        QueueRedraw();
    }

    public override void _Process(double delta) { }

    public override void _Draw()
    {
        if (_data.Length == 0 || _data == null) return;
        var width = _width / _data.Length * .8;
        var spacer = _width / _data.Length * .2;

        for (int i = 0; i < _data.Length; i++)
        {
            var height = (_data[i] + 1) / ((float)N + 1) * _height;
            var x = i * (width + spacer);
            var y = _height - height;

            if (_pointers.Contains(i))
            {
                DrawRect(new Rect2((float)x - (float)spacer, 0, (float)width + (float)(2 * spacer), _height), Colors.Red);
            }

            DrawRect(new Rect2((float)x, y, (float)width, height), Colors.White);
        }
    }
}

