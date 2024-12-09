namespace GameBrain;

public class PlayerState(EGamePiece type)
{
    public readonly EGamePiece Type = type;
    public int PiecesLeft { get; internal set; }
    
    public int MovesMade { get; internal set; }
}