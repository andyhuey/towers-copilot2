using TowersOfHanoi.Core;
using TowersOfHanoi.ConsoleApp;

// Start screen
int diskCount = Screens.ShowStartScreen();

// Initialise game
var engine = new GameEngine();
engine.NewGame(diskCount);

// Game loop
while (!engine.IsComplete() && !engine.IsQuit)
{
    GameRenderer.DrawGame(engine);

    var key = Console.ReadKey(true);

    switch (key.Key)
    {
        case ConsoleKey.LeftArrow:
            engine.MoveCursorLeft();
            break;

        case ConsoleKey.RightArrow:
            engine.MoveCursorRight();
            break;

        case ConsoleKey.Spacebar:
            engine.ToggleSelect();
            break;

        case ConsoleKey.Escape:
            if (engine.SelectedTowerIndex != null)
                engine.CancelSelect();
            else
                engine.Quit();
            break;
    }
}

// Final render before end screen
if (engine.IsComplete())
    GameRenderer.DrawGame(engine);

// End screen
var result = engine.GetGameResult();
Screens.ShowEndScreen(result);
