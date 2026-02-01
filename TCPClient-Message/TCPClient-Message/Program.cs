using System.Net.Sockets;
using System.Text;

try
{
    using TcpClient client = new TcpClient("127.0.0.1", 8080);
    using NetworkStream stream = client.GetStream();

    Console.WriteLine("Connected via TCP. Sending burst...");

    int messageCount = 100;
    byte[] data = Encoding.UTF8.GetBytes("Msg"); // 3 bytes

    // BURST SEND (no framing, no delays)
    for (int i = 0; i < messageCount; i++)
    {
        await stream.WriteAsync(data, 0, data.Length);
    }

    Console.WriteLine("Burst sent (100 writes, stream-based).");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}