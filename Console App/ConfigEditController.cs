using DAL;
using GameBrain;
using MenuSystem;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class ConfigEditController(IConfigRepository configRepository, GameConfiguration config) : AbstractController
{
    private GameConfiguration _config = config;

    public void Run()
    {
        Menu(Header,
            MenuItem("N <name>", () => $"Name ({_config.Name})", OnUpdateName),
            MenuItem("B <width> <height>", () => $"Board size ({_config.BoardWidth} x {_config.BoardHeight})", OnUpdateBoardSize),
            MenuItem("G <width> <height>", () => $"Grid size ({_config.GridWidth} x {_config.GridHeight})", OnUpdateGridSize),
            MenuItem("G <x> <y>", () => $"Grid initial position ({_config.GridX} x {_config.GridY})", OnUpdateGridPos),
            MenuItem("P <n>", () => $"Piece count for each player ({_config.PlayerPieceCount})", OnUpdatePlayerPieceCount),
            MenuItem("M <n>", () => $"Number of moves to make before move pieces or grid available ({_config.MovePieceAfterNMoves})", OnUpdateMovePieceAfterNMoves),
            MenuItem("W <n>", () => $"Number of pieces in a row for a win ({_config.WinSequence})", OnUpdateWinSequence),
            MenuItem("S", "Save", OnSave)
        )
        .RunUntilExit();
    }

    private void OnSave(MenuSelection input)
    {
        _config.Validate();
        configRepository.SaveConfiguration(_config);
        input.AddMessage("Configuration saved");
    }

    private void OnUpdateName(MenuSelection input)
    {
        _config.Name = input.GetString(0);
    }
        
    private void OnUpdateBoardSize(MenuSelection input)
    {
        _config.BoardWidth = input.GetInt(0);
        _config.BoardHeight = input.GetInt(1);
    }
    
    private void OnUpdateGridSize(MenuSelection input)
    {
        _config.GridWidth = input.GetInt(0);
        _config.GridHeight = input.GetInt(1);
    }
    
    private void OnUpdateGridPos(MenuSelection input)
    {
        _config.GridX = input.GetInt(0);
        _config.GridY = input.GetInt(1);
    }
    
    private void OnUpdatePlayerPieceCount(MenuSelection input)
    {
        _config.PlayerPieceCount = input.GetInt(0);
    }
    
    private void OnUpdateWinSequence(MenuSelection input)
    {
        _config.WinSequence = input.GetInt(0);
    }
    
    private void OnUpdateMovePieceAfterNMoves(MenuSelection input)
    {
        _config.MovePieceAfterNMoves = input.GetInt(0);
    }
}
