using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenLocalhost(5000);
});

var app = builder.Build();

app.UseRouting();
app.MapHub<PingHub>("/hub");

Console.WriteLine("SignalR server running on http://localhost:5000");
app.Run();

public class PingHub : Hub
{
    public Task Ping(string message)
    {
        Console.WriteLine($"[{Context.ConnectionId}] {message}");
        return Task.CompletedTask;
    }
}
