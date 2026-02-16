namespace TowersOfHanoi.Core;

public class Tower
{
    private readonly Stack<Disk> _disks = new();

    public int Count => _disks.Count;

    public IReadOnlyList<Disk> Disks => _disks.Reverse().ToList();

    public Disk? Peek() => _disks.Count > 0 ? _disks.Peek() : null;

    public bool CanPlace(Disk disk)
    {
        return _disks.Count == 0 || _disks.Peek().Size > disk.Size;
    }

    public void Push(Disk disk)
    {
        if (!CanPlace(disk))
            throw new InvalidOperationException(
                $"Cannot place disk of size {disk.Size} on disk of size {_disks.Peek().Size}.");

        _disks.Push(disk);
    }

    public Disk Pop()
    {
        if (_disks.Count == 0)
            throw new InvalidOperationException("Cannot pop from an empty tower.");

        return _disks.Pop();
    }
}
