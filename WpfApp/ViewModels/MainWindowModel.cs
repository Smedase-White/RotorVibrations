using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;

using RotorVibrations;

using SkiaSharp;

namespace WpfApp.ViewModels;

public class MainWindowModel : ViewModelBase
{
    private ConfigurationModel _configuration;
    private ChartModel _chart;

    private readonly SKColor RotorColor = new(175, 200, 200);
    private readonly SKColor DamperColor = new(200, 200, 100);
    private readonly SKColor GapColor = new(200, 50, 50);

    public MainWindowModel() 
    {
        _configuration = new();
        _chart = new();
        _chart.CalculateCommand.AddExecute(obj => Calculate());
    }

    public ConfigurationModel Configuration
    {
        get => _configuration;
    }

    public ChartModel Chart
    {
        get => _chart;
        set => ChangeProperty(ref _chart, value);
    }

    public void Calculate()
    {
        List<ObservablePoint> rotorPositions = new(), damperPositions = new(), gapValues = new();
        RotorTask task = new()
        {
            M1 = Configuration.RotorWeight,
            M2 = Configuration.DamperWeight,
            C1 = Configuration.RotorSpringStiffness,
            C2 = Configuration.DamperSpringStiffness,
            C = Configuration.DamperStiffness,
            Delta = Configuration.Gap,
            EndTime = Chart.EndTime,
            StepTime = Chart.StepTime,
        };
        RungeKuttaMethod method = new();
        method.Init(task.InitTime, task.InitPos, task.Func);
        for (; method.Time <= task.EndTime; method.NextStep(task.StepTime))
        {
            double x1 = method.Y![0, 0], x2 = method.Y![1, 0];
            rotorPositions.Add(new(method.Time, x1));
            damperPositions.Add(new(method.Time, x2 + task.Delta));
            gapValues.Add(new(method.Time, x1 - x2 > task.Delta ? x1 - x2 - task.Delta: null));
        }

        _chart.Series.Clear();
        AddLineSeries(rotorPositions, "Ротор", RotorColor);
        AddLineSeries(damperPositions, "Демпфер", DamperColor);
        AddLineSeries(gapValues, "Заход в  демфер", GapColor);
    }

    private void AddLineSeries(IEnumerable<ObservablePoint> values, string title, SKColor color)
    {
        _chart.Series.Add(new LineSeries<ObservablePoint>()
        {
            Name = title,
            Values = values,
            Stroke = new SolidColorPaint(color, 3),
            Fill = null,
            GeometryStroke = null,
            GeometryFill = null,
            LineSmoothness = 0,
        });
    }
}
