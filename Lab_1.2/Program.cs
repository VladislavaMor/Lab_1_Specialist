using System.Threading;

namespace Lab_1._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread0 = new Thread(Work) { Name = "Поток 1" };
            Thread thread1 = new Thread(Work) { Name = "Поток 2" };

            thread1.Start(thread0);
            Thread.Sleep(5000);
            thread0.Start();
            
            Console.ReadLine();
        }

        static void Work(object? parameter)
        {
            if (parameter is Thread t && t.IsAlive)
                t.Join(); 
            for (int i = 1; i <= 100; i++)
                Console.WriteLine($"Thread {Thread.CurrentThread.Name} : {i}");
        }
    }
}
