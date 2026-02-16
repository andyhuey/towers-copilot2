# requirements for Towers of Hanoi game

I want to create a simple Towers of Hanoi game, with a text-mode user interface.
It should be written in C#, under .NET 8, and should be runnable from Windows or Linux.

Description of canonical game: https://en.wikipedia.org/wiki/Tower_of_Hanoi

The soluution should have three projects:
1. A C# console app, executable, containing the main application shell and any user-interface logic.
2. A library DLL with all interface-independent program logic.
3. A unit test project, for testing the program logic.

The program should display a start screen when launched, with:
1. A summary of the canonical Towers of Hanoi game.
2. A summary of the controls used in this application.
3. A text box to enter the number of disks to play with. (Default value of 4. minimun value 3, maximum 9.)

The game play screen should be a simple representation of the three towers and the disks. Controls should be as follows:
1. Use left and right arrow keys to move between towers.
2. Use space bar to highlight a disk to move. (Toggle)
3. When highlighted, left and right arrow keys move the disk (within allowed moves).
4. After a move, the space bar "releases" the disk and removes the highlight.
5. The game ends when all disks have been moved from left to right.
6. The Esc key can be pressed at any time to end the game.

At end of game, a final screen should be shown, displaying:
1. Number of disks moved.
2. Number of moves made by player.
3. Total time elapsed.
4. Whether or not the game was complete or quit early.
