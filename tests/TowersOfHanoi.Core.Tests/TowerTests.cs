namespace TowersOfHanoi.Core.Tests;

public class TowerTests
{
    [Fact]
    public void NewTower_IsEmpty()
    {
        var tower = new Tower();
        Assert.Equal(0, tower.Count);
        Assert.Null(tower.Peek());
    }

    [Fact]
    public void Push_AddsDisksInOrder()
    {
        var tower = new Tower();
        tower.Push(new Disk(3));
        tower.Push(new Disk(2));
        tower.Push(new Disk(1));

        Assert.Equal(3, tower.Count);
        Assert.Equal(new Disk(1), tower.Peek());
    }

    [Fact]
    public void Pop_RemovesTopDisk()
    {
        var tower = new Tower();
        tower.Push(new Disk(3));
        tower.Push(new Disk(1));

        var popped = tower.Pop();

        Assert.Equal(new Disk(1), popped);
        Assert.Equal(1, tower.Count);
        Assert.Equal(new Disk(3), tower.Peek());
    }

    [Fact]
    public void Pop_EmptyTower_Throws()
    {
        var tower = new Tower();
        Assert.Throws<InvalidOperationException>(() => tower.Pop());
    }

    [Fact]
    public void CanPlace_AllowsSmallerOnLarger()
    {
        var tower = new Tower();
        tower.Push(new Disk(5));
        Assert.True(tower.CanPlace(new Disk(3)));
    }

    [Fact]
    public void CanPlace_RejectsLargerOnSmaller()
    {
        var tower = new Tower();
        tower.Push(new Disk(2));
        Assert.False(tower.CanPlace(new Disk(5)));
    }

    [Fact]
    public void CanPlace_RejectsEqualSize()
    {
        var tower = new Tower();
        tower.Push(new Disk(3));
        Assert.False(tower.CanPlace(new Disk(3)));
    }

    [Fact]
    public void CanPlace_AllowsAnyOnEmpty()
    {
        var tower = new Tower();
        Assert.True(tower.CanPlace(new Disk(9)));
    }

    [Fact]
    public void Push_LargerOnSmaller_Throws()
    {
        var tower = new Tower();
        tower.Push(new Disk(2));
        Assert.Throws<InvalidOperationException>(() => tower.Push(new Disk(5)));
    }

    [Fact]
    public void Disks_ReturnsBottomToTop()
    {
        var tower = new Tower();
        tower.Push(new Disk(3));
        tower.Push(new Disk(2));
        tower.Push(new Disk(1));

        var disks = tower.Disks;
        Assert.Equal(3, disks.Count);
        Assert.Equal(new Disk(3), disks[0]); // bottom
        Assert.Equal(new Disk(1), disks[2]); // top
    }
}
