using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels;

public class ConfigurationModel : ViewModelBase
{
    private double _rotorWeight = 5, _damperWeight = 1;
    private double _rotorSpringStiffness = 1, _damperSpringStiffness = 5, _damperStiffness = 100;
    private double _gap = 1;

    public ConfigurationModel() { }

    public double RotorWeight 
    { 
        get => _rotorWeight; 
        set => ChangeProperty(ref _rotorWeight, value); 
    }

    public double DamperWeight 
    { 
        get => _damperWeight; 
        set => ChangeProperty(ref _damperWeight, value); 
    }

    public double RotorSpringStiffness 
    { 
        get => _rotorSpringStiffness;
        set => ChangeProperty(ref _rotorSpringStiffness, value); 
    }

    public double DamperSpringStiffness 
    { 
        get => _damperSpringStiffness; 
        set => ChangeProperty(ref _damperSpringStiffness, value); 
    }

    public double DamperStiffness 
    { 
        get => _damperStiffness; 
        set => ChangeProperty(ref _damperStiffness, value); 
    }

    public double Gap 
    { 
        get => _gap; 
        set => ChangeProperty(ref _gap, value);
    }
}
