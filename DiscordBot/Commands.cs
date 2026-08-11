using Discord.WebSocket;
using DiscordBot.Handler_Classes;

namespace DiscordBot;

public class Commands
{

    // public fields
    public Dictionary<string, Func<SocketMessage, Task>> CommandsDictionary = new Dictionary<string, Func<SocketMessage, Task>>
    {
        ["!hello"] = Hello,
        ["!list"] =  CommandList,
        ["!roll"] = Roll,
        ["!whenupd"] = WhenUpdate,
        ["!giverole"] = async (msg) =>
        {
            if (msg.Author is SocketGuildUser user)
                await RoleManager.GiveRole(user, msg);
        }
            
    };
    
    // private fields
    private static Random _randomNumber = new Random();
    
    private static async Task CommandList(SocketMessage message)
    {
        var list = $"""
                       ```Here is the list of all available commands:
                       1. !roll - bot will pick random number between 1 and 100.
                       2. !list - bot will send list of available all commands.
                       3. !hello - bot will respond with 'Hello! @<Username>'. ```
                       """;
        
        await message.Channel.SendMessageAsync(list);
    }

    public static async Task Hello(SocketMessage message)
    {
        await message.Channel.SendMessageAsync($"Hello! {message.Author.Mention}");
    }
    public static async Task WhenUpdate(SocketMessage message)
    {
        await message.Channel.SendMessageAsync($"# I DON'T KNOW");
    }
    public static async Task Roll(SocketMessage message)
    {
        await message.Channel.SendMessageAsync($"Random number: {_randomNumber.Next(1, 100)}");
    }
}