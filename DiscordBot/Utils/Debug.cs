namespace DiscordBot.Utils;

public static class Debug
{
    public static void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}