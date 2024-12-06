namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    public int BoardSizeWidth { get; set; } = 5;
    public int BoardSizeHeight { get; set; } = 5;
    
    //How many pieces in a row to win
    public int WinSequence { get; set; } = 3;
    
   
    public int MovePieceAfterNMoves { get; set; } = 2;
    
    public override string ToString() => 
        $"Board: {BoardSizeWidth}x{BoardSizeHeight}," + 
        "\nto get {WinSequence} in a row inside the grid," + 
        "\ncan move piece or grid after {MovePieceAfterNMoves} moves";
}