using System.IO;
using System.Text.Json;
using Discord.WebSocket;
using DiscordBot.Utils;

namespace DiscordBot.Handler_Classes;

public static class RoleManager
{
    public static async Task GiveRole(SocketGuildUser user, SocketMessage message)
    {
        string json = File.ReadAllText("config.json");
        using JsonDocument doc = JsonDocument.Parse(json);
        
        // navigate to rolesId
        string? roleIdString = doc.RootElement
            .GetProperty("rolesId")
            .GetProperty("Test")
            .GetString();

        if (ulong.TryParse(roleIdString, out ulong testRoleId))
        {
            var role = user.Guild.GetRole(testRoleId);
            if (role == null) return;

            // check if user has the role already
            if (user.Roles.Any(r => r.Id == testRoleId))
            {
                await message.Channel.SendMessageAsync($"You already have the {role.Mention} role, you stupid. LOL");
            }
            else
            {
                await user.AddRoleAsync(role);
                await message.Channel.SendMessageAsync($"Gave role {role.Mention} to {user.Mention}");
            }

        }
        else { Debug.Log("Invalid role ID in config file"); }

    }
}