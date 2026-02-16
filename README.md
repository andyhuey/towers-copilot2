# Towers of Hanoi

A text-mode Towers of Hanoi game built in C# on .NET 8. Features ANSI box-drawing characters, colour-coded disks, and cross-platform support for Windows and Linux.

## How to Play

```bash
dotnet run --project src/TowersOfHanoi.Console
```

- **←/→** arrow keys to move between towers (or move a selected disk)
- **Space** to pick up / release a disk
- **Esc** to quit

Move all disks from the left tower to the right tower. You can't place a larger disk on a smaller one.

## Building & Testing

```bash
dotnet build
dotnet test
```

## Acknowledgements

This project was built with [GitHub Copilot CLI](https://githubnext.com/projects/copilot-cli) using the **Claude Opus 4.6** model.
