using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

const int MAX_CONNECTION_IN_QUEUE = 10;
const int PORT = 1111;
const int MAX_THREADS = 20;

ThreadPool.SetMaxThreads(MAX_THREADS, MAX_THREADS);

IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, PORT);
using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(ipPoint);
socket.Listen(MAX_CONNECTION_IN_QUEUE);
int count = 0;
var lock_object = new object();
while (true)
{
    Socket client = socket.Accept();
    Task task = Task.Run(() =>
    {
        Console.Write($"Remote client: {client.RemoteEndPoint} ");
        using var stream = new NetworkStream(client);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        using var writer = new StreamWriter(stream, Encoding.UTF8);
        string result = reader.ReadLine();
        lock (lock_object) count++;
        Console.WriteLine($"Received: {result}, Requests: {count}");
        Thread.Sleep(100);
        writer.WriteLine(result.ToUpper());
        writer.Flush();
        client.Dispose();
    });
}
