using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorVibrations;

public class RotorTask
{
    public RotorTask() { }

    public double M1 { get; set; }
    public double M2 { get; set; }

    public double C1 { get; set; }
    public double C2 { get; set; }
    public double C { get; set; }

    public double Delta { get; set; }

    public double InitTime { get; set; } = 0;
    public double StepTime { get; set; } = 10e-2;
    public double EndTime { get; set; } = 60;
    public double[,] InitPos { get; set; } = new double[,] { { 0, 1 }, { 0, 0 } };

    public double[,] Func(double t, double[,] Y)
    {
        double x1 = Y[0, 0], x2 = Y[1, 0];
        return new double[,]
        {
            { Y[0, 1], -(C1 * x1 + DepreciationDerivate(x1 - x2)) / M1 },
            { Y[1, 1], -(C2 * x2 - DepreciationDerivate(x1 - x2)) / M2 }
        };
    }

    private double Depreciation(double dx)
    {
        if (dx <= Delta)
            return 0;
        return C * Math.Pow(dx - Delta, 2) / 2;
    }

    private double DepreciationDerivate(double dx)
    {
        if (dx <= Delta)
            return 0;
        return C * (dx - Delta);
    }

}
