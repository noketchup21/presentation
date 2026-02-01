// Server.cs
using System.Net;
using System.Net.Sockets;
using System.Text;

var server = new TcpListener(IPAddress.Any, 8080);
server.Start();

Console.WriteLine("TCP Server started on port 8080.");

while (true) // keep server alive
{
    using TcpClient client = await server.AcceptTcpClientAsync();
    using NetworkStream stream = client.GetStream();

    Console.WriteLine("Client connected.");

    byte[] buffer = new byte[1024];

    try
    {
        while (true)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {
                Console.WriteLine("Client disconnected.");
                break;
            }

            string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received: " + msg);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Connection error: " + ex.Message);
    }

    Console.WriteLine("Waiting for next client...");
}
