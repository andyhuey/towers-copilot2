# Towers of Hanoi – Implementation Plan

## Problem Statement

Build a cross-platform (Windows/Linux) text-mode Towers of Hanoi game in C# / .NET 8. The solution has three projects: a console app (UI), a core library (game logic), and an xUnit test project. The display uses ANSI box-drawing characters and colour-coded disks.

## Solution Structure

```
TowersOfHanoi.sln
├── src/
│   ├── TowersOfHanoi.Console/   # Console app (entry point + UI)
│   └── TowersOfHanoi.Core/      # Class library (game logic)
└── tests/
    └── TowersOfHanoi.Core.Tests/ # xUnit tests
```

## Approach

- **Core library** is purely logic — no `System.Console` dependency. Exposes a `Game` model and move-validation engine.
- **Console app** owns all rendering and keyboard input. Consumes the Core library.
- Unit tests cover move validation, win detection, and game state transitions.

---

## Work Plan

### Phase 1 – Project scaffolding ✅
- [x] Create .NET 8 solution (`dotnet new sln`)
- [x] Create `TowersOfHanoi.Core` class library project
- [x] Create `TowersOfHanoi.Console` console app project
- [x] Create `TowersOfHanoi.Core.Tests` xUnit project
- [x] Wire up project references (Console → Core, Tests → Core)
- [x] Verify `dotnet build` succeeds

### Phase 2 – Core library (TowersOfHanoi.Core) ✅
- [x] `Disk` model (size, colour mapping)
- [x] `Tower` model (stack of disks, push/pop with validation)
- [x] `GameEngine` class (towers, cursor, selection, move count, timer, completion)
  - [x] `NewGame(int diskCount)` — initialise towers with disks on leftmost peg
  - [x] `ToggleSelect()` — pick up / release top disk (toggle highlight)
  - [x] `TryMoveDisk(int fromTower, int toTower)` — validate & execute move
  - [x] `IsComplete()` — all disks on rightmost peg
  - [x] `GetGameResult()` — returns disk count, move count, elapsed time, complete vs quit
- [x] Move-validation logic (can't place larger on smaller)

### Phase 3 – Unit tests (TowersOfHanoi.Core.Tests) ✅
- [x] Test game initialisation (correct disk count on first tower)
- [x] Test valid moves
- [x] Test invalid moves (larger on smaller, empty tower)
- [x] Test win condition detection
- [x] Test game result reporting
- [x] Verify `dotnet test` passes (41 tests)

### Phase 4 – Console UI (TowersOfHanoi.Console) ✅
- [x] **Start screen**
  - [x] Display game summary / rules
  - [x] Display control instructions
  - [x] Prompt for number of disks (default 4, min 3, max 9)
- [x] **Game screen**
  - [x] Render three towers with ANSI box-drawing characters
  - [x] Colour-code each disk size with a distinct `ConsoleColor`
  - [x] Highlight selected disk
  - [x] Show current tower cursor position
  - [x] Show move count and elapsed time
- [x] **Input handling**
  - [x] Left/Right arrow keys: move cursor between towers
  - [x] Space bar: toggle disk selection / release
  - [x] Esc: cancel selection (if selected) or quit game
- [x] **End screen**
  - [x] Display number of disks
  - [x] Display move count
  - [x] Display elapsed time
  - [x] Display complete vs quit status

### Phase 5 – Integration & polish ✅
- [x] End-to-end manual smoke test
- [x] Verify cross-platform: `dotnet run` works on Linux
- [x] Final `dotnet build` + `dotnet test` green

---

## Notes

- Disk colours: map disk sizes 1–9 to distinct `ConsoleColor` values (e.g. Red, Green, Yellow, Blue, Magenta, Cyan, White, DarkYellow, DarkCyan).
- Box-drawing rendering: use `║` for tower poles, `═` for disk bodies, and `─` / `╨` for the base.
- The cursor indicator (which tower is currently focused) can be an arrow `▼` or bracket above the tower.
- Timer uses `System.Diagnostics.Stopwatch` in the Core library (no Console dependency).
- The game loop in the Console app reads keys, dispatches to `GameEngine`, and re-renders after each action.
