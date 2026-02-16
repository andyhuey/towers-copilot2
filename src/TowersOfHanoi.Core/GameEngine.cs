using System.Diagnostics;

namespace TowersOfHanoi.Core;

public class GameEngine
{
    public const int MinDisks = 3;
    public const int MaxDisks = 9;
    public const int DefaultDisks = 4;
    public const int TowerCount = 3;

    private readonly Stopwatch _stopwatch = new();

    public Tower[] Towers { get; private set; } = [];
    public int DiskCount { get; private set; }
    public int MoveCount { get; private set; }
    public int CursorIndex { get; private set; }
    public int? SelectedTowerIndex { get; private set; }
    public bool IsQuit { get; private set; }

    public void NewGame(int diskCount)
    {
        if (diskCount < MinDisks || diskCount > MaxDisks)
            throw new ArgumentOutOfRangeException(nameof(diskCount),
                $"Disk count must be between {MinDisks} and {MaxDisks}.");

        DiskCount = diskCount;
        MoveCount = 0;
        CursorIndex = 0;
        SelectedTowerIndex = null;
        IsQuit = false;

        Towers = new Tower[TowerCount];
        for (int i = 0; i < TowerCount; i++)
            Towers[i] = new Tower();

        // Place disks largest-first on the leftmost tower
        for (int size = diskCount; size >= 1; size--)
            Towers[0].Push(new Disk(size));

        _stopwatch.Restart();
    }

    public void MoveCursorLeft()
    {
        if (CursorIndex > 0)
            CursorIndex--;
    }

    public void MoveCursorRight()
    {
        if (CursorIndex < TowerCount - 1)
            CursorIndex++;
    }

    /// <summary>
    /// Toggles disk selection. If no disk is selected, picks up the top disk
    /// from the current tower. If a disk is already selected, attempts to
    /// place it on the current tower.
    /// </summary>
    /// <returns>True if the action succeeded, false if invalid.</returns>
    public bool ToggleSelect()
    {
        if (SelectedTowerIndex == null)
        {
            // Pick up from current tower
            if (Towers[CursorIndex].Count == 0)
                return false;

            SelectedTowerIndex = CursorIndex;
            return true;
        }
        else
        {
            // Release onto current tower
            return TryMoveDisk(SelectedTowerIndex.Value, CursorIndex);
        }
    }

    /// <summary>
    /// Attempts to move the top disk from one tower to another.
    /// </summary>
    /// <returns>True if the move was valid and executed.</returns>
    public bool TryMoveDisk(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || fromIndex >= TowerCount)
            throw new ArgumentOutOfRangeException(nameof(fromIndex));
        if (toIndex < 0 || toIndex >= TowerCount)
            throw new ArgumentOutOfRangeException(nameof(toIndex));

        var fromTower = Towers[fromIndex];
        var toTower = Towers[toIndex];

        if (fromTower.Count == 0)
            return false;

        var disk = fromTower.Peek()!;
        if (!toTower.CanPlace(disk))
            return false;

        fromTower.Pop();
        toTower.Push(disk);
        MoveCount++;
        SelectedTowerIndex = null;
        CursorIndex = toIndex;
        return true;
    }

    public bool IsComplete()
    {
        return Towers.Length == TowerCount
            && Towers[TowerCount - 1].Count == DiskCount;
    }

    public void Quit()
    {
        IsQuit = true;
        _stopwatch.Stop();
    }

    public GameResult GetGameResult()
    {
        _stopwatch.Stop();
        return new GameResult
        {
            DiskCount = DiskCount,
            MoveCount = MoveCount,
            Elapsed = _stopwatch.Elapsed,
            IsComplete = IsComplete()
        };
    }
}
