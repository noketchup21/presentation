// TCPClient/Program.cs
using System.Net.Sockets;
using System.Text;

try
{
    using TcpClient client = new TcpClient("127.0.0.1", 8080);
    using NetworkStream stream = client.GetStream();

    Console.WriteLine("Connected to TCP Server.");

    byte[] ping = Encoding.UTF8.GetBytes("PING");

    while (true)
    {
        await stream.WriteAsync(ping, 0, ping.Length);
        Console.WriteLine("Sent: PING");

        await Task.Delay(1000);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Connection lost:");
    Console.WriteLine(ex.Message);
}
