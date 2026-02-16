namespace TowersOfHanoi.Core;

public class GameResult
{
    public required int DiskCount { get; init; }
    public required int MoveCount { get; init; }
    public required TimeSpan Elapsed { get; init; }
    public required bool IsComplete { get; init; }
}
