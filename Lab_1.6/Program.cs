using System.Diagnostics;

const int STEPS = 100000000;
const int Tasks = 10;
double Single(Func<double, double> f, double a, double b, int steps = STEPS)
{
    double w = (b - a) / steps;
    double summa = 0d;
    for (int i = 0; i < steps; i++)
    {
        double x = a + i * w + w / 2;
        double h = f(x);
        summa += h * w;
    }
    return summa;
}

double SingleAsync(Func<double, double> f, double a, double b, int tasks = Tasks)
{
    double w = (b - a) / tasks;
    double summa = 0d;
    var s = new object();
    Parallel.For(0, tasks, (int i) =>
    {
        double newsum = f(a + i * w + w / 2) * w;
        lock(s) summa += newsum;
    });
    return summa;
}

Stopwatch t1 = new Stopwatch();
t1.Start();
double r1 = Single(Math.Sin, 0, Math.PI / 2);
t1.Stop();

Stopwatch t2 = new Stopwatch();
t2.Start();
double r2 = SingleAsync(Math.Sin, 0, Math.PI / 2);
t2.Stop();

Console.WriteLine($"Single result : {r1} Time: {t1.ElapsedMilliseconds}");
Console.WriteLine($"Multi result : {r2} Time: {t2.ElapsedMilliseconds}");
