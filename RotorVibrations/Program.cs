using System.Threading.Tasks;

namespace RotorVibrations;

static class Program
{

    static void Main(string[] args)
    {
        RotorTask task = new();
        RungeKuttaMethod method = new();
        method.Init(task.InitTime, task.InitPos, task.Func);

        while(method.Time <= task.EndTime)
        {
            Console.WriteLine($"Time = {method.Time:F5}; X1 = {method.Y![0, 0]:F8}; X2 = {method.Y![1, 0]:F8}; {method.Y![0, 0] - method.Y![1, 0] > task.Delta}");
            method.NextStep(task.StepTime);
        }
    }
}