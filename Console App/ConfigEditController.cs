using DAL;
using GameBrain;
using MenuSystem;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class ConfigEditController(IConfigRepository configRepository, GameConfiguration config) : AbstractController
{
    public void Run()
    {
        Menu(Header,
            MenuItem("N <name>", () => $"Name ({config.Name})", OnUpdateName),
            MenuItem("B <width> <height>", () => $"Board size ({config.BoardWidth} x {config.BoardHeight})", OnUpdateBoardSize),
            MenuItem("G <width> <height>", () => $"Grid size ({config.GridWidth} x {config.GridHeight})", OnUpdateGridSize),
            MenuItem("G <x> <y>", () => $"Grid initial position ({config.GridX} x {config.GridY})", OnUpdateGridPos),
            MenuItem("P <n>", () => $"Piece count for each player ({config.PlayerPieceCount})", OnUpdatePlayerPieceCount),
            MenuItem("M <n>", () => $"Number of moves to make before move pieces or grid available ({config.MovePieceAfterNMoves})", OnUpdateMovePieceAfterNMoves),
            MenuItem("W <n>", () => $"Number of pieces in a row for a win ({config.WinSequence})", OnUpdateWinSequence),
            MenuItem("S", "Save", OnSave)
        )
        .RunUntilExit();
    }

    private void OnSave(MenuSelection input)
    {
        config.Validate();
        configRepository.SaveConfiguration(config);
        input.AddMessage("Configuration saved");
    }

    private void OnUpdateName(MenuSelection input)
    {
        config.Name = input.GetString(0);
    }
        
    private void OnUpdateBoardSize(MenuSelection input)
    {
        config.BoardWidth = input.GetInt(0);
        config.BoardHeight = input.GetInt(1);
    }
    
    private void OnUpdateGridSize(MenuSelection input)
    {
        config.GridWidth = input.GetInt(0);
        config.GridHeight = input.GetInt(1);
    }
    
    private void OnUpdateGridPos(MenuSelection input)
    {
        config.GridX = input.GetInt(0);
        config.GridY = input.GetInt(1);
    }
    
    private void OnUpdatePlayerPieceCount(MenuSelection input)
    {
        config.PlayerPieceCount = input.GetInt(0);
    }
    
    private void OnUpdateWinSequence(MenuSelection input)
    {
        config.WinSequence = input.GetInt(0);
    }
    
    private void OnUpdateMovePieceAfterNMoves(MenuSelection input)
    {
        config.MovePieceAfterNMoves = input.GetInt(0);
    }
}
