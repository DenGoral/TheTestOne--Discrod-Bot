using Discord;
using Discord.WebSocket;
using DiscordBot.Handler_Classes;

namespace DiscordBot;

public class Program
{
    private static DiscordSocketClient _client = null!;
    
    public static async Task Main()
    {
        // create client
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents =
                GatewayIntents.Guilds |
                GatewayIntents.GuildMessages |
                GatewayIntents.MessageContent |
                GatewayIntents.DirectMessages |
                GatewayIntents.GuildMembers
        });
        
        // check
        _client.Ready += () =>
        {
            Console.WriteLine("Bot IS Ready");
            return Task.CompletedTask;
        };

        _client.Log += log =>
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        };

        Console.WriteLine("Connected method");
        _client.MessageReceived += MessageHandler.OnMessageReceived;
        
        var token = File.ReadAllText("token.txt");
        
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        Console.WriteLine("Started");

        // _ = MessageHandler.SpamMessages(_client); just for later
        
        await Task.Delay(-1);
    }
}