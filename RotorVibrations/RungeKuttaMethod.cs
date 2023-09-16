using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorVibrations;

public class RungeKuttaMethod
{
    private double[,]? TempY, K1, K2, K3, K4;

    public RungeKuttaMethod() { }

    public double Time { get; set; } = 0;

    public double[,]? Y { get; set; }

    public Func<double, double[,], double[,]>? Func { get; set; }

    private void Init(uint N, uint K)
    {
        Y = new double[N, K];
        TempY = new double[N, K];
        K1 = new double[N, K];
        K2 = new double[N, K];
        K3 = new double[N, K];
        K4 = new double[N, K];
    }

    public void Init(double t0, double[,] Y0, Func<double, double[,], double[,]> func)
    {
        Time = t0;
        uint n = (uint)Y0.GetLength(0), k = (uint)Y0.GetLength(1);
        Init(n, k);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < k; j++)
                Y![i, j] = Y0[i, j];
        Func = func;
    }

    public void NextStep(double dt)
    {
        uint n = (uint)Y!.GetLength(0), k = (uint)Y!.GetLength(1);

        K1 = Func!(Time, Y!);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < k; j++)
                TempY![i, j] = Y[i, j] + K1[i, j] * (dt / 2.0);

        K2 = Func(Time + dt / 2.0, TempY!);

        for (int i = 0; i < n; i++)
            for (int j = 0; j < k; j++)
                TempY![i, j] = Y[i, j] + K2[i, j] * (dt / 2.0);

        K3 = Func(Time + dt / 2.0, TempY!);

        for (int i = 0; i < n; i++)
            for (int j = 0; j < k; j++)
                TempY![i, j] = Y[i, j] + K3[i, j] * dt;

        K4 = Func(Time + dt, TempY!);

        for (int i = 0; i < n; i++)
            for (int j = 0; j < k; j++)
                Y[i, j] = Y[i, j] + dt / 6.0 * (K1[i, j] + 2.0 * K2[i, j] + 2.0 * K3[i, j] + K4[i, j]);

        Time += dt;
    }
}
