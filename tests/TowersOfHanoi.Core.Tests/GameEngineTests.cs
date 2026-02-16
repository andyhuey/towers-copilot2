namespace TowersOfHanoi.Core.Tests;

public class GameEngineTests
{
    private GameEngine CreateGame(int disks = 4)
    {
        var engine = new GameEngine();
        engine.NewGame(disks);
        return engine;
    }

    // --- Initialisation ---

    [Fact]
    public void NewGame_PlacesAllDisksOnFirstTower()
    {
        var engine = CreateGame(4);

        Assert.Equal(4, engine.Towers[0].Count);
        Assert.Equal(0, engine.Towers[1].Count);
        Assert.Equal(0, engine.Towers[2].Count);
    }

    [Fact]
    public void NewGame_DisksAreOrderedSmallestOnTop()
    {
        var engine = CreateGame(4);
        var disks = engine.Towers[0].Disks;

        Assert.Equal(new Disk(4), disks[0]); // bottom
        Assert.Equal(new Disk(1), disks[3]); // top
    }

    [Fact]
    public void NewGame_ResetsState()
    {
        var engine = CreateGame(4);
        engine.MoveCursorRight();
        engine.NewGame(3);

        Assert.Equal(0, engine.CursorIndex);
        Assert.Equal(0, engine.MoveCount);
        Assert.Null(engine.SelectedTowerIndex);
        Assert.Equal(3, engine.DiskCount);
        Assert.False(engine.IsQuit);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(0)]
    [InlineData(-1)]
    public void NewGame_InvalidDiskCount_Throws(int diskCount)
    {
        var engine = new GameEngine();
        Assert.Throws<ArgumentOutOfRangeException>(() => engine.NewGame(diskCount));
    }

    [Theory]
    [InlineData(3)]
    [InlineData(9)]
    public void NewGame_BoundaryDiskCounts_Succeed(int diskCount)
    {
        var engine = CreateGame(diskCount);
        Assert.Equal(diskCount, engine.Towers[0].Count);
    }

    // --- Cursor movement ---

    [Fact]
    public void MoveCursorRight_IncrementsIndex()
    {
        var engine = CreateGame();
        engine.MoveCursorRight();
        Assert.Equal(1, engine.CursorIndex);
    }

    [Fact]
    public void MoveCursorRight_ClampsAtMax()
    {
        var engine = CreateGame();
        engine.MoveCursorRight();
        engine.MoveCursorRight();
        engine.MoveCursorRight(); // beyond last tower
        Assert.Equal(2, engine.CursorIndex);
    }

    [Fact]
    public void MoveCursorLeft_DecrementsIndex()
    {
        var engine = CreateGame();
        engine.MoveCursorRight();
        engine.MoveCursorLeft();
        Assert.Equal(0, engine.CursorIndex);
    }

    [Fact]
    public void MoveCursorLeft_ClampsAtZero()
    {
        var engine = CreateGame();
        engine.MoveCursorLeft();
        Assert.Equal(0, engine.CursorIndex);
    }

    // --- Selection toggle ---

    [Fact]
    public void ToggleSelect_SelectsDiskFromCurrentTower()
    {
        var engine = CreateGame();
        var result = engine.ToggleSelect();

        Assert.True(result);
        Assert.Equal(0, engine.SelectedTowerIndex);
    }

    [Fact]
    public void ToggleSelect_EmptyTower_ReturnsFalse()
    {
        var engine = CreateGame();
        engine.MoveCursorRight(); // tower 1 is empty
        var result = engine.ToggleSelect();

        Assert.False(result);
        Assert.Null(engine.SelectedTowerIndex);
    }

    [Fact]
    public void ToggleSelect_ReleasesOntoValidTower()
    {
        var engine = CreateGame(3);

        // Select from tower 0
        engine.ToggleSelect();
        // Move cursor to tower 1 and release
        engine.MoveCursorRight();
        var result = engine.ToggleSelect();

        Assert.True(result);
        Assert.Null(engine.SelectedTowerIndex);
        Assert.Equal(1, engine.MoveCount);
        Assert.Equal(2, engine.Towers[0].Count);
        Assert.Equal(1, engine.Towers[1].Count);
    }

    // --- Valid moves ---

    [Fact]
    public void TryMoveDisk_ValidMove_Succeeds()
    {
        var engine = CreateGame(3);
        var result = engine.TryMoveDisk(0, 1);

        Assert.True(result);
        Assert.Equal(1, engine.MoveCount);
        Assert.Equal(2, engine.Towers[0].Count);
        Assert.Equal(1, engine.Towers[1].Count);
    }

    [Fact]
    public void TryMoveDisk_SmallerOnLarger_Succeeds()
    {
        var engine = CreateGame(3);
        engine.TryMoveDisk(0, 2); // move disk 1 to tower 2
        engine.TryMoveDisk(0, 1); // move disk 2 to tower 1
        var result = engine.TryMoveDisk(2, 1); // move disk 1 onto disk 2

        Assert.True(result);
        Assert.Equal(2, engine.Towers[1].Count);
    }

    // --- Invalid moves ---

    [Fact]
    public void TryMoveDisk_LargerOnSmaller_Fails()
    {
        var engine = CreateGame(3);
        engine.TryMoveDisk(0, 1); // move disk 1 to tower 1
        var result = engine.TryMoveDisk(0, 1); // try disk 2 onto disk 1

        Assert.False(result);
        Assert.Equal(1, engine.MoveCount); // count didn't increase
    }

    [Fact]
    public void TryMoveDisk_FromEmptyTower_Fails()
    {
        var engine = CreateGame(3);
        var result = engine.TryMoveDisk(1, 2); // tower 1 is empty

        Assert.False(result);
        Assert.Equal(0, engine.MoveCount);
    }

    [Fact]
    public void TryMoveDisk_InvalidIndex_Throws()
    {
        var engine = CreateGame(3);
        Assert.Throws<ArgumentOutOfRangeException>(() => engine.TryMoveDisk(-1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => engine.TryMoveDisk(0, 3));
    }

    // --- Win condition ---

    [Fact]
    public void IsComplete_AllDisksOnRightTower_ReturnsTrue()
    {
        var engine = CreateGame(3);
        // Solve 3-disk puzzle: classic 7-move solution
        engine.TryMoveDisk(0, 2);
        engine.TryMoveDisk(0, 1);
        engine.TryMoveDisk(2, 1);
        engine.TryMoveDisk(0, 2);
        engine.TryMoveDisk(1, 0);
        engine.TryMoveDisk(1, 2);
        engine.TryMoveDisk(0, 2);

        Assert.True(engine.IsComplete());
        Assert.Equal(7, engine.MoveCount);
    }

    [Fact]
    public void IsComplete_GameInProgress_ReturnsFalse()
    {
        var engine = CreateGame(3);
        engine.TryMoveDisk(0, 1);

        Assert.False(engine.IsComplete());
    }

    [Fact]
    public void IsComplete_NewGame_ReturnsFalse()
    {
        var engine = CreateGame(3);
        Assert.False(engine.IsComplete());
    }

    // --- Game result ---

    [Fact]
    public void GetGameResult_ReportsCorrectValues_OnCompletion()
    {
        var engine = CreateGame(3);
        engine.TryMoveDisk(0, 2);
        engine.TryMoveDisk(0, 1);
        engine.TryMoveDisk(2, 1);
        engine.TryMoveDisk(0, 2);
        engine.TryMoveDisk(1, 0);
        engine.TryMoveDisk(1, 2);
        engine.TryMoveDisk(0, 2);

        var result = engine.GetGameResult();

        Assert.Equal(3, result.DiskCount);
        Assert.Equal(7, result.MoveCount);
        Assert.True(result.IsComplete);
        Assert.True(result.Elapsed >= TimeSpan.Zero);
    }

    [Fact]
    public void GetGameResult_ReportsIncomplete_OnQuit()
    {
        var engine = CreateGame(4);
        engine.TryMoveDisk(0, 1);
        engine.Quit();

        var result = engine.GetGameResult();

        Assert.Equal(4, result.DiskCount);
        Assert.Equal(1, result.MoveCount);
        Assert.False(result.IsComplete);
        Assert.True(engine.IsQuit);
    }
}
