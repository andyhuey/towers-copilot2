namespace TowersOfHanoi.Core.Tests;

public class DiskTests
{
    [Theory]
    [InlineData(1, ConsoleColor.Red)]
    [InlineData(2, ConsoleColor.Green)]
    [InlineData(3, ConsoleColor.Yellow)]
    [InlineData(9, ConsoleColor.DarkCyan)]
    public void Disk_HasCorrectColor(int size, ConsoleColor expected)
    {
        var disk = new Disk(size);
        Assert.Equal(expected, disk.Color);
    }

    [Fact]
    public void Disk_EqualityBySize()
    {
        Assert.Equal(new Disk(3), new Disk(3));
        Assert.NotEqual(new Disk(3), new Disk(4));
    }
}
