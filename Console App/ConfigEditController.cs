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
                MenuItem("N <name>", () => $"Name ({config.Name})", UpdateName),
            MenuItem("B <width> <height>", () => $"Board size ({config.BoardWidth} x {config.BoardHeight})", UpdateBoardSize),
            MenuItem("G <width> <height>", () => $"Grid size ({config.GridWidth} x {config.GridHeight})", UpdateGridSize),
            MenuItem("G <x> <y>", () => $"Grid initial position ({config.GridX} x {config.GridY})", UpdateGridPos),
            MenuItem("P <n>", () => $"Piece count for each player ({config.PlayerPieceCount})", UpdatePlayerPieceCount),
            MenuItem("M <n>", () => $"Number of moves to make before move pieces or grid available  ({config.MovePieceAfterNMoves})", UpdateMovePieceAfterNMoves),
            MenuItem("W <n>", () => $"Number of pieces in a row for a win ({config.WinSequence})", UpdateWinSequence),
            MenuItem("S", "Save", Save)
        )
        .RunUntilExit();
    }

    private void Save()
    {
        config.Validate();
        configRepository.SaveConfiguration(config);
    }

    private void UpdateName(MenuSelection input)
    {
        config.Name = input.GetString(0);
    }
        
    private void UpdateBoardSize(MenuSelection input)
    {
        config.BoardWidth = input.GetInt(0);
        config.BoardHeight = input.GetInt(1);
    }
    
    private void UpdateGridSize(MenuSelection input)
    {
        config.GridWidth = input.GetInt(0);
        config.GridHeight = input.GetInt(1);
    }
    
    private void UpdateGridPos(MenuSelection input)
    {
        config.GridX = input.GetInt(0);
        config.GridY = input.GetInt(1);
    }
    
    private void UpdatePlayerPieceCount(MenuSelection input)
    {
        config.PlayerPieceCount = input.GetInt(0);
    }
    
    private void UpdateWinSequence(MenuSelection input)
    {
        config.WinSequence = input.GetInt(0);
    }
    
    private void UpdateMovePieceAfterNMoves(MenuSelection input)
    {
        config.MovePieceAfterNMoves = input.GetInt(0);
    }
}
