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

### Phase 2 – Core library (TowersOfHanoi.Core)
- [ ] `Disk` model (size, colour mapping)
- [ ] `Tower` model (stack of disks, push/pop with validation)
- [ ] `GameState` model (three towers, selected tower index, selected disk, move count, timer tracking, completion status)
- [ ] `GameEngine` class:
  - [ ] `NewGame(int diskCount)` — initialise towers with disks on leftmost peg
  - [ ] `SelectDisk(int towerIndex)` — pick up top disk (toggle highlight)
  - [ ] `MoveDisk(int fromTower, int toTower)` — validate & execute move
  - [ ] `IsComplete()` — all disks on rightmost peg
  - [ ] `GetGameResult()` — returns disk count, move count, elapsed time, complete vs quit
- [ ] Move-validation logic (can't place larger on smaller)

### Phase 3 – Unit tests (TowersOfHanoi.Core.Tests)
- [ ] Test game initialisation (correct disk count on first tower)
- [ ] Test valid moves
- [ ] Test invalid moves (larger on smaller, empty tower)
- [ ] Test win condition detection
- [ ] Test game result reporting
- [ ] Verify `dotnet test` passes

### Phase 4 – Console UI (TowersOfHanoi.Console)
- [ ] **Start screen**
  - [ ] Display game summary / rules
  - [ ] Display control instructions
  - [ ] Prompt for number of disks (default 4, min 3, max 9)
- [ ] **Game screen**
  - [ ] Render three towers with ANSI box-drawing characters
  - [ ] Colour-code each disk size with a distinct `ConsoleColor`
  - [ ] Highlight selected disk
  - [ ] Show current tower cursor position
  - [ ] Show move count and elapsed time
- [ ] **Input handling**
  - [ ] Left/Right arrow keys: move cursor between towers (no disk selected) or move disk (disk selected)
  - [ ] Space bar: toggle disk selection / release
  - [ ] Esc: quit game
- [ ] **End screen**
  - [ ] Display number of disks
  - [ ] Display move count
  - [ ] Display elapsed time
  - [ ] Display complete vs quit status

### Phase 5 – Integration & polish
- [ ] End-to-end manual smoke test
- [ ] Verify cross-platform: `dotnet run` works on Linux
- [ ] Final `dotnet build` + `dotnet test` green

---

## Notes

- Disk colours: map disk sizes 1–9 to distinct `ConsoleColor` values (e.g. Red, Green, Yellow, Blue, Magenta, Cyan, White, DarkYellow, DarkCyan).
- Box-drawing rendering: use `║` for tower poles, `═` for disk bodies, and `─` / `╨` for the base.
- The cursor indicator (which tower is currently focused) can be an arrow `▼` or bracket above the tower.
- Timer uses `System.Diagnostics.Stopwatch` in the Core library (no Console dependency).
- The game loop in the Console app reads keys, dispatches to `GameEngine`, and re-renders after each action.
