namespace Lab_1._3
{
    class MyData
    {
        public double x = 1;
        public bool phase = true;

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(true);
            MyData s = new MyData();
            new Thread(() => {
                for (int i = 0; i < 10; i++)
                {
                    autoResetEvent.WaitOne();
                    s.x = Math.Cos(s.x);
                    Console.Write($"{s.x}  ");
                    autoResetEvent.Set();
                }
            }).Start();

            new Thread(() => {
                for (int i = 0; i < 10; i++)
                {
                    autoResetEvent.WaitOne();
                    s.x = Math.Acos(s.x);
                    Console.WriteLine($"{s.x}");
                    autoResetEvent.Set();
                }
            }).Start();
        }
    }
}
