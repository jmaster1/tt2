using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web2.Pages;

public class GameModel(IGameRepository gameRepository) : PageModel
{
    public string? Error { get; set; }
    
    [BindProperty(SupportsGet = true)] 
    public string GameId { get; set; } = null!;
    
    [BindProperty]
    public string CellRefEmpty { get; set; } = null!;
    
    [BindProperty]
    public string CellRefPiece { get; set; } = null!;
    
    [BindProperty]
    public string GridMoveDir { get; set; } = null!;
    
    string s = "←	Leftwards Arrow	U+2190	&#8592;
↑	Upwards Arrow	U+2191	&#8593;
→	Rightwards Arrow	U+2192	&#8594;
↓	Downwards Arrow	U+2193	&#8595;
↔	Left Right Arrow	U+2194	&#8596;
↕	Up Down Arrow	U+2195	&#8597;
↖	North West Arrow	U+2196	&#8598;
↗	North East Arrow	U+2197	&#8599;
↘	South East Arrow	U+2198	&#8600;
↙	South West Arrow	U+2199	&#8601;"

    public TicTacTwoBrain Brain = new();
    
    public void OnGet()
    {
        var snapshot = gameRepository.Load(GameId);
        Brain.LoadSnapshot(snapshot!);
    }

    private static (int x, int y)? ParseXY(string? input)
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
    
    public IActionResult OnPostMakeMove()
    {
        try
        {
            var snapshot = gameRepository.Load(GameId);
            Brain.LoadSnapshot(snapshot!);

            var piecePos = ParseXY(CellRefPiece);
            var emptyPos = ParseXY(CellRefEmpty);
            var gridMoveDir = ParseXY(GridMoveDir);
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
            
            snapshot = Brain.CreateSnapshot();
            snapshot.Name = GameId;
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
