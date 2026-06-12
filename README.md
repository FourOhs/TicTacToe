# TicTacToe

A simple single-player Tic-Tac-Toe game written in C# for macOS.

## What’s in the project

- Shared core logic: `src/TicTacToe.Core/TicTacToe.Core.csproj`
- Desktop GUI: `src/TicTacToe.Gui/TicTacToe.Gui.csproj` (Avalonia)
- Unit tests: `Tests/TicTacToe.Tests/TicTacToe.Tests.csproj`
- Solution: `TicTacToe.slnx`

## Run the desktop GUI

```sh
dotnet run --project src/TicTacToe.Gui/TicTacToe.Gui.csproj
```

## Run the tests

```sh
dotnet test TicTacToe.slnx
```

## Notes

- The GUI is now the main app entry point.
- Shared logic lives in `src/TicTacToe.Core` so sibling projects can be added under `src/` later.
- The computer uses a minimax opponent.
- The GUI highlights the board in different colors at the end of the game depending on the result.
- The tests cover win, draw, invalid move, and AI blocking behavior.

