using TowersOfHanoi.Core;

namespace TowersOfHanoi.ConsoleApp;

public static class GameRenderer
{
    private const char Pole = '║';
    private const char DiskChar = '═';
    private const char BaseChar = '═';
    private const char BaseJoint = '╨';
    private const char Cursor = '▼';

    // Width of each tower column (must accommodate the largest disk)
    private static int TowerWidth => GameEngine.MaxDisks * 2 + 3;

    public static void DrawGame(GameEngine engine)
    {
        Console.Clear();
        Console.CursorVisible = false;

        int maxHeight = engine.DiskCount;

        // Draw cursor row
        for (int t = 0; t < GameEngine.TowerCount; t++)
        {
            string label = t == engine.CursorIndex
                ? Cursor.ToString()
                : " ";
            Console.Write(CenterInColumn(label));
        }
        Console.WriteLine();

        // Draw tower rows from top to bottom
        for (int row = maxHeight - 1; row >= 0; row--)
        {
            for (int t = 0; t < GameEngine.TowerCount; t++)
            {
                var disks = engine.Towers[t].Disks;
                if (row < disks.Count)
                {
                    var disk = disks[row];
                    bool highlight = engine.SelectedTowerIndex == t
                        && row == disks.Count - 1;
                    DrawDisk(disk, highlight);
                }
                else
                {
                    DrawPole();
                }
            }
            Console.WriteLine();
        }

        // Draw base
        for (int t = 0; t < GameEngine.TowerCount; t++)
        {
            string baseLine = new string(BaseChar, TowerWidth / 2)
                + BaseJoint
                + new string(BaseChar, TowerWidth / 2);
            Console.Write(baseLine);
        }
        Console.WriteLine();

        // Draw tower labels
        for (int t = 0; t < GameEngine.TowerCount; t++)
        {
            Console.Write(CenterInColumn($"Tower {t + 1}"));
        }
        Console.WriteLine();
        Console.WriteLine();

        // Status line
        var elapsed = engine.Elapsed;
        Console.Write($"  Moves: {engine.MoveCount}    ");
        Console.WriteLine($"Time: {elapsed.Minutes:D2}:{elapsed.Seconds:D2}");

        if (engine.SelectedTowerIndex != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Disk selected — use ←/→ then Space to place, Esc to cancel");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("  ←/→ move cursor | Space pick up | Esc quit");
        }
    }

    private static void DrawDisk(Disk disk, bool highlight)
    {
        int halfWidth = TowerWidth / 2;
        int diskHalf = disk.Size;
        int padding = halfWidth - diskHalf;

        string pad = new string(' ', padding);
        string body = new string(DiskChar, diskHalf);

        Console.Write(pad);

        if (highlight)
            Console.BackgroundColor = ConsoleColor.DarkGray;

        Console.ForegroundColor = disk.Color;
        Console.Write(body + Pole + body);
        Console.ResetColor();

        Console.Write(pad);
    }

    private static void DrawPole()
    {
        int halfWidth = TowerWidth / 2;
        string pad = new string(' ', halfWidth);
        Console.Write(pad + Pole + pad);
    }

    private static string CenterInColumn(string text)
    {
        int totalWidth = TowerWidth;
        int leftPad = (totalWidth - text.Length) / 2;
        return text.PadLeft(leftPad + text.Length).PadRight(totalWidth);
    }
}
