using Discord;
using Discord.WebSocket;

namespace DiscordBot.Handler_Classes;

public static class MessageHandler 
{
    private static readonly Commands? _commands = new Commands();
    
    public static async Task SpamMessages(DiscordSocketClient client)
    {
        Random rnd = new Random();
        var messages = new string[]
        {
            "# I am TheTestOne!",
            "-# you are gay",
            "## LOL",
        };

        while (true)
        {
            int numberOfSeconds = 15; // cooldown between random messages
            await Task.Delay(numberOfSeconds * 1000);

            const ulong channelId = 1472901327557230784; // #testing channel
            var channel = client.GetChannel(channelId) as IMessageChannel;
            await channel!.SendMessageAsync(messages[rnd.Next(messages.Length)]);
        }
    }
    
    public static async Task OnMessageReceived(SocketMessage message)
    {
        Console.WriteLine($"Message received event fired from {message.Author.Username}");

        if (message.Author.IsBot) return;
        if (!message.Content.StartsWith('!')) return;
        
        string[] parts = message.Content.Split(' ');
        string command = parts[0];
        
        if (string.IsNullOrWhiteSpace(command)) return;

        string key = command.ToLower();
        if (_commands.CommandsDictionary.TryGetValue(key, out Func<SocketMessage, Task> handler))
        {
            await handler(message);
        }
    }

    public static async Task WelcomeMessage(SocketGuildUser user)
    {
        ulong welcomeChannelId = 1472901244128465032; // just general channel

        var channel = user.Guild.GetChannel(welcomeChannelId) as SocketTextChannel;

        if (channel != null)
        {
            await channel.SendMessageAsync($"Welcome {user.Mention}, i hope you have the worst time of the day lo, lmao, get good, uwu ig idk");
        }
    }
}