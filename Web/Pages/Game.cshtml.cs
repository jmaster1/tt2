using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web2.Pages;

public class GameModel(
    IGameRepository gameRepository, 
    IPlayerTokenRepository playerTokenRepository) : PageModel
{
    public class Dir(char arrow, int dx, int dy)
    {
        public readonly char Arrow = arrow;
        public readonly int Dx = dx;
        public readonly int Dy = dy;
        public string Id => $"Dir_{Dx + 1}{Dy + 1}";
        public string Value => $"{Dx} {Dy}";
    }

    public static readonly Dir[] Dirs =
    [
        new('←', -1, 0),
        new('↑', 0, -1),
        new('→', 1, 0),
        new('↓', 0, 1),
        new('↖', -1, -1),
        new('↗', 1, -1),
        new('↘', 1, 1),
        new('↙', -1, 1)
    ];

    public string? Error { get; set; }
    
    [BindProperty(SupportsGet = true)] 
    public string Token { get; set; } = null!;
    
    [BindProperty]
    public string CellRefEmpty { get; set; } = null!;
    
    [BindProperty]
    public string CellRefPiece { get; set; } = null!;
    
    [BindProperty]
    public string GridMoveDir { get; set; } = null!;
    
    public TicTacTwoBrain Brain = new();
    
    public bool Loaded { get; set; }
    
    public PlayerToken? PlayerToken { get; set; }

    private static (int x, int y)? ParseXy(string? input)
    {
        if (input == null)
        {
            return null;
        }
        var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var x = int.Parse(tokens[0]);
        var y = int.Parse(tokens[1]);
        return (x, y);
    }
    
    private void Load()
    {
        try
        {
            PlayerToken = playerTokenRepository.Load(Token);
        }
        catch (Exception)
        {
            throw new InvalidDataException("Invalid token");
        }
        
        var snapshot = gameRepository.Load(PlayerToken!.GameId);
        Brain.LoadSnapshot(snapshot!);
        Loaded = true;
    }

    public void OnGet()
    {
        try
        {
            Load();
        }
        catch (Exception any)
        {
            Error = any.Message;
        }
    }
    
    public IActionResult OnPostMakeMove()
    {
        try
        {
            Load();
            if (Brain.NextMove != PlayerToken!.Type)
            {
                throw new Exception("That's not your turn to make move");
            }
            var piecePos = ParseXy(CellRefPiece);
            var emptyPos = ParseXy(CellRefEmpty);
            var gridMoveDir = ParseXy(GridMoveDir);
            if (gridMoveDir != null)
            {
                Brain.MoveGridTo(gridMoveDir.Value.x + Brain.GridX, gridMoveDir.Value.y + Brain.GridY);
            } else if (piecePos != null && emptyPos != null)
            {
                Brain.MovePiece(piecePos.Value.x, piecePos.Value.y, emptyPos.Value.x, emptyPos.Value.y);
            } else if (emptyPos != null)
            {
                Brain.PutPiece(emptyPos.Value.x, emptyPos.Value.y);    
            }
            else
            {
                throw new Exception("No input provided to make move");
            }
            
            var snapshot = Brain.CreateSnapshot(PlayerToken!.GameId);
            gameRepository.Save(snapshot);
        }
        catch (Exception any)
        {
            Error = any.Message;
        }
        return Page();
    }

    public char GetPieceText(EGamePiece piece)
    {
        return piece switch
        {
            EGamePiece.X => 'x',
            EGamePiece.O => 'o',
            _ => ' '
        };
    }
}
