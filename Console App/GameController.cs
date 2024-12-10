using ConsoleUI;
using DAL;
using GameBrain;
using MenuSystem;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class GameController(TicTacTwoBrain brain) : AbstractController
{
    private readonly Visualizer _visualizer = new(brain);
    
    internal void GameLoop()
    {
        Menu(Header,
            MenuItem("P <x> <y>", "Put piece at specified cell", OnPutPiece),
            MenuItem("M <fromX> <fromY> <toX> <toY>", "Move piece to another cell", OnMovePiece),
            MenuItem("G <x> <y>", "Move grid to specified cell", OnMoveGrid),
            MenuItem("D", "Dump game state", OnDump)
        )
        .BeforeDraw(Render)
        .RunUntilExit();
    }

    private void OnDump(MenuSelection input)
    {
        var json = JsonStringSerializer.ToString(brain.CreateSnapshot());
        input.AddMessage(json);
    }

    private void Render()
    {
        _visualizer.RenderToConsole();
        RenderPlayerState(brain.PlayerX);
        RenderPlayerState(brain.PlayerO);
        Console.WriteLine("Current player: " + brain.NextMove);
    }

    private void RenderPlayerState(PlayerState player)
    {
        Console.WriteLine($"Player {player.Type}: pieces = {player.PiecesLeft}, moves = {player.MovesMade}" );
    }

    private void OnMoveGrid(MenuSelection input)
    {
        var x = ReadIndex(input, 0);
        var y = ReadIndex(input, 1);
        brain.MoveGridTo(x, y);
    }

    private void OnMovePiece(MenuSelection input)
    {
        var fromX = ReadIndex(input, 0);
        var fromY = ReadIndex(input, 1);
        var toX = ReadIndex(input, 2);
        var toY = ReadIndex(input, 3);
        brain.MovePiece(fromX, fromY, toX, toY);
    }

    private void OnPutPiece(MenuSelection input)
    {
        var x = ReadIndex(input, 0);
        var y = ReadIndex(input, 1);
        brain.PutPiece(x, y);
    }

    private static int ReadIndex(MenuSelection input, int i)
    {
        return Visualizer.Title2Index(input.GetChar(i));
    }
}
