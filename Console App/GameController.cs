using ConsoleUI;
using GameBrain;
using MenuSystem;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class GameController(TicTacTwoBrain brain)
{
    private Visualizer _visualizer = new(brain);
    internal void GameLoop()
    {
        MenuItem[] items = [MenuItem("M", "makeMove to [x y]", MakeMove)];
        new Menu("Tic-Tac-Two - game: " + brain.NextMove, [.. items])
            .BeforeDraw(() =>
            {
                var str = _visualizer.Render();
                Console.WriteLine(str);
            })
            .RunUntilExit();
    }

    private void MakeMove(MenuSelection input)
    {
        int x, y;
        try
        {
            x = input.GetInt(0);
            y = input.GetInt(1);
        } catch(Exception)
        {
            throw new InvalidDataException("Bad input");
        }            
        brain.MakeMove(x, y);
    }
}
