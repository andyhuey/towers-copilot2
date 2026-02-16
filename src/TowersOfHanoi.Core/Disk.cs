namespace TowersOfHanoi.Core;

public record Disk(int Size)
{
    public const int MinSize = 1;
    public const int MaxSize = 9;

    private static readonly ConsoleColor[] Colors =
    [
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Yellow,
        ConsoleColor.Blue,
        ConsoleColor.Magenta,
        ConsoleColor.Cyan,
        ConsoleColor.White,
        ConsoleColor.DarkYellow,
        ConsoleColor.DarkCyan
    ];

    public ConsoleColor Color => Colors[(Size - 1) % Colors.Length];
}
