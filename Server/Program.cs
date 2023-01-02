using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .WithOrigins("http://127.0.0.1:5500") // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()); // allow credentials

app.MapHub<ChatHub>("/chat");

app.MapGet("/", () => "Hello World!");

app.Run();

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}