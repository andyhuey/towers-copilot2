# Copilot Instructions for TowersOfHanoi

## Build & Test Commands

```bash
# Build entire solution
dotnet build

# Run all tests
dotnet test

# Run a single test by fully-qualified name
dotnet test --filter "FullyQualifiedName~TowersOfHanoi.Core.Tests.GameEngineTests.IsComplete_AllDisksOnRightTower_ReturnsTrue"

# Run tests matching a pattern
dotnet test --filter "DisplayName~MoveDisk"

# Run the console app
dotnet run --project src/TowersOfHanoi.Console
```

## Architecture

A .NET 8 cross-platform (Windows/Linux) Towers of Hanoi game with strict separation between logic and UI:

- **TowersOfHanoi.Core** — Class library with all game logic. Must **never** reference `System.Console` or any UI concerns.
  - `GameEngine` — Central orchestrator: game state, cursor, selection, move validation, win detection, timing (`System.Diagnostics.Stopwatch`).
  - `Tower` — Stack-based disk container enforcing the Hanoi rule (smaller on larger only) via `CanPlace()`.
  - `Disk` — Immutable record with `Size` (1–9) and a `Color` property mapped to distinct `ConsoleColor` values.
  - `GameResult` — DTO returned by `GameEngine.GetGameResult()` with disk count, move count, elapsed time, completion status.

- **TowersOfHanoi.Console** — Console app owning all rendering and input. References Core via `<ProjectReference>`.
  - `Program.cs` — Entry point and game loop: start screen → key input → `GameEngine` dispatch → re-render → end screen.
  - `GameRenderer` — Draws towers, colour-coded disks, cursor indicator (`▼`), and status bar using Unicode box-drawing characters (`║`, `═`, `╨`).
  - `Screens` — Start screen (rules, controls, disk count prompt with validation) and end screen (results, optimal move comparison).

- **TowersOfHanoi.Core.Tests** — xUnit tests (41 tests) covering move validation, win detection, game state transitions, and boundary conditions.

## Key Conventions

- **Nullable reference types** and **implicit usings** are enabled across all projects.
- Disk count range is **3–9** (default 4), validated in `GameEngine`.
- Input model: arrow keys move the cursor; Space picks up/places a disk (toggle via `ToggleSelect()`); Esc cancels selection first (`CancelSelect()`), then quits.
- The display uses Unicode box-drawing characters — not plain ASCII.
