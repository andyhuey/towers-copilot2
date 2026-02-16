# Copilot Instructions for TowersOfHanoi

## Build & Test Commands

```bash
# Build entire solution
dotnet build

# Run all tests
dotnet test

# Run a single test by fully-qualified name
dotnet test --filter "FullyQualifiedName~TowersOfHanoi.Core.Tests.ClassName.MethodName"

# Run tests matching a pattern
dotnet test --filter "DisplayName~MoveDisk"

# Run the console app
dotnet run --project src/TowersOfHanoi.Console
```

## Architecture

This is a .NET 8 Towers of Hanoi game with a strict separation between logic and UI:

- **TowersOfHanoi.Core** (`src/TowersOfHanoi.Core/`) — Class library containing all game logic: models (`Disk`, `Tower`, `GameState`), move validation, and the `GameEngine`. This project must **never** reference `System.Console` or any UI concerns. Timer tracking uses `System.Diagnostics.Stopwatch`.
- **TowersOfHanoi.Console** (`src/TowersOfHanoi.Console/`) — Console app that owns all rendering (ANSI box-drawing characters, colour-coded disks) and keyboard input. Consumes the Core library. References Core via `<ProjectReference>`.
- **TowersOfHanoi.Core.Tests** (`tests/TowersOfHanoi.Core.Tests/`) — xUnit tests for the Core library. Tests cover move validation, win detection, and game state transitions.

The game loop lives in the Console project: read a key → dispatch to `GameEngine` → re-render.

## Key Conventions

- **Nullable reference types** are enabled across all projects (`<Nullable>enable</Nullable>`).
- **Implicit usings** are enabled (`<ImplicitUsings>enable</ImplicitUsings>`).
- The game supports **3–9 disks** (default 4). Validate this range in the Core library.
- Disk sizes 1–9 each map to a distinct `ConsoleColor` for visual differentiation.
- The display uses Unicode box-drawing characters (`║`, `═`, `╨`) — not plain ASCII.
- Must run cross-platform on both Windows and Linux.
