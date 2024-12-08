using System.Drawing;

namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    public int BoardSizeWidth => BoardSize.Width;
    public int BoardSizeHeight => BoardSize.Height;

    public Size BoardSize { get; set; } = new Size(5, 5);

    public Size GridSize { get; set; } = new Size(3, 3);

    public Point GridPos { get; set; } = new Point(1, 1);

    public int PlayerPieceCount { get; set; } = 4;
 
    
    //How many pieces in a row to win
    public int WinSequence { get; set; } = 3;
    
    public int MovePieceAfterNMoves { get; set; } = 2;
}
