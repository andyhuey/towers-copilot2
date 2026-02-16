using TowersOfHanoi.Core;

namespace TowersOfHanoi.ConsoleApp;

public static class Screens
{
    public static int ShowStartScreen()
    {
        Console.Clear();
        Console.CursorVisible = true;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║         TOWERS  OF  HANOI              ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine("The Tower of Hanoi is a classic puzzle. You have three");
        Console.WriteLine("towers and a stack of disks of decreasing size. All disks");
        Console.WriteLine("start on the left tower. The goal is to move all disks to");
        Console.WriteLine("the right tower, one at a time, without ever placing a");
        Console.WriteLine("larger disk on top of a smaller one.");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Controls:");
        Console.ResetColor();
        Console.WriteLine("  ←/→    Move cursor between towers");
        Console.WriteLine("  Space  Pick up / place a disk");
        Console.WriteLine("  Esc    Quit game");
        Console.WriteLine();

        int diskCount = GameEngine.DefaultDisks;
        while (true)
        {
            Console.Write($"Number of disks [{GameEngine.MinDisks}-{GameEngine.MaxDisks}] (default {GameEngine.DefaultDisks}): ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
                return GameEngine.DefaultDisks;

            if (int.TryParse(input, out diskCount)
                && diskCount >= GameEngine.MinDisks
                && diskCount <= GameEngine.MaxDisks)
                return diskCount;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Please enter a number between {GameEngine.MinDisks} and {GameEngine.MaxDisks}.");
            Console.ResetColor();
        }
    }

    public static void ShowEndScreen(GameResult result)
    {
        Console.Clear();
        Console.CursorVisible = true;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║            GAME  OVER                  ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        if (result.IsComplete)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ★ Congratulations — puzzle complete! ★");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Game quit early.");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine($"  Disks:       {result.DiskCount}");
        Console.WriteLine($"  Moves:       {result.MoveCount}");
        Console.WriteLine($"  Time:        {result.Elapsed.Minutes:D2}:{result.Elapsed.Seconds:D2}.{result.Elapsed.Milliseconds / 10:D2}");

        int optimalMoves = (1 << result.DiskCount) - 1;
        Console.WriteLine($"  Optimal:     {optimalMoves} moves");

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
    }
}
