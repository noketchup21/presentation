using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/hub")
    .Build();

Console.WriteLine("Connecting to SignalR Hub...");
await connection.StartAsync();
Console.WriteLine("Connected. Sending burst...");

int messageCount = 100;
var tasks = new List<Task>();

// BURST SEND (SignalR message-based)
for (int i = 0; i < messageCount; i++)
{
    // Each InvokeAsync is a distinct framed message
    tasks.Add(connection.SendAsync("Ping", $"Msg-{i}"));
}

// Wait until all messages are sent
await Task.WhenAll(tasks);

Console.WriteLine("Burst sent (100 framed SignalR messages).");
Console.ReadLine();
