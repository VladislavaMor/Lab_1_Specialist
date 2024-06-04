namespace Lab_1._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Work) { Name = "Поток 1"};
            Thread thread2 = new Thread(Work) { Name = "Поток 2"};
            thread1.Start(new NewThread(1,10));
            thread2.Start(new NewThread(100,110));
            Console.ReadLine(); 
        }

        record class NewThread (int Start, int End );

        static void Work(object? parameter)
        {
            if (parameter is NewThread thread)
            {
                for (int i = thread.Start; i <= thread.End; i++)
                    Console.WriteLine($"Thread {Thread.CurrentThread.Name} : {i}");
            }
        }
    }
}
