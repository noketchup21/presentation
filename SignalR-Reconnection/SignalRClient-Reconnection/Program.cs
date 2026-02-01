// SignalR_Ping_Client.cs
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/hub")
    .WithAutomaticReconnect()
    .Build();

// Lifecycle events (main demo)
connection.Reconnecting += _ =>
{
    Console.WriteLine(">>> Connection lost. Reconnecting...");
    return Task.CompletedTask;
};

connection.Reconnected += id =>
{
    Console.WriteLine($">>> Reconnected. New ConnectionId: {id}");
    return Task.CompletedTask;
};

connection.Closed += _ =>
{
    Console.WriteLine(">>> Connection closed permanently.");
    return Task.CompletedTask;
};

await connection.StartAsync();
Console.WriteLine("Connected. Kill the server to test reconnection.");

while (true)
{
    try
    {
        await connection.InvokeAsync("Ping", "Ping");
        Console.WriteLine("Ping sent");
    }
    catch
    {
        Console.WriteLine("Ping failed (server down or reconnecting)");
    }

    await Task.Delay(2000);
}
