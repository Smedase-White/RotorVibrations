using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;

using SkiaSharp;

namespace WpfApp.ViewModels;

public class ChartModel : ViewModelBase
{
    private double _endTime = 60;
    private double _stepTime = 10e-3;
    private RelayCommand? _calculateCommand;

    private ObservableCollection<ISeries> _series = new();

    public ChartModel() { }

    public double EndTime
    {
        get => _endTime;
        set => ChangeProperty(ref _endTime, value);
    }

    public double StepTime
    {
        get => _stepTime;
        set => ChangeProperty(ref _stepTime, value);
    }

    public RelayCommand CalculateCommand
    {
        get => _calculateCommand ??= new();
        set => ChangeProperty(ref _calculateCommand, value);
    }

    public ObservableCollection<ISeries> Series 
    { 
        get => _series; 
        set => _series = value; 
    }

    public SolidColorPaint LegendTextPaint { get; set; } = new(new(255, 255, 255));
}
