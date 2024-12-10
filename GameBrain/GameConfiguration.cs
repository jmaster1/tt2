using System.Text;

namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    
    public int BoardWidth { get; set; } = 5;
    
    public int BoardHeight { get; set; } = 5;
    
    public int GridWidth { get; set; } = 3;
    
    public int GridHeight { get; set; } = 3;
    
    public int GridX { get; set; } = 1;
    
    public int GridY { get; set; } = 1;
    
    public int PlayerPieceCount { get; set; } = 4;
 
    public int WinSequence { get; set; } = 3;
    
    public int MovePieceAfterNMoves { get; set; } = 2;

    public readonly void Validate()
    {
        var errors = new StringBuilder();
        if (Name == null || Name.Trim().Length == 0)
        {
            errors.Append("Empty name\n");
        }
        if (BoardWidth < 3 || BoardHeight < 3 || BoardWidth > 99 || BoardHeight > 99)
        {
            errors.Append("Invalid board size\n");
        }
        if (GridWidth < 3 || GridHeight < 3 || GridWidth > 99 || GridHeight > 99 || 
            GridWidth > BoardWidth - 2 || GridHeight > BoardHeight - 2)
        {
            errors.Append("Invalid grid size\n");
        }
        if (GridX < 0 || GridX + GridWidth >= BoardWidth ||
            GridY < 0 || GridY + GridHeight >= BoardHeight)
        {
            errors.Append("Invalid grid position\n");
        }
        if (PlayerPieceCount < 3 || PlayerPieceCount < WinSequence)
        {
            errors.Append("Invalid player piece count\n");
        }
        if (WinSequence < 0 || WinSequence > PlayerPieceCount)
        {
            errors.Append("Invalid win sequence\n");
        }
        if (errors.Length > 0)
        {
            throw new InvalidDataException(errors.ToString());
        }
    }
}
