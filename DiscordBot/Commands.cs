using System.Threading.Channels;
using Discord.Interactions;
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
        ["!aboutme"] = AboutMe,
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
                       3. !hello - bot will respond with 'Hello! @<Username>'. 
                       4. !aboutme - bot will tell who he is!```
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

    public static async Task AboutMe(SocketMessage message)
    {
        var about = $"""
                       ```Hi! I am a bot developed by an amazing, charismatic, awesome, smart, humble, nonchalant 'ImAstroth'.
                       I am the first ever bot developed by him, the programming genuis, i can do almost nothing, but you can suggest what can i add
                       in the general channel! That is it! im going to kill you lol, get good nerd```
                       """;

        await message.Channel.SendMessageAsync(about);
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