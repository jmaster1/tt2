namespace GameBrain;

public class PlayerState(EGamePiece type)
{
    public EGamePiece Type { get; } = type;
    
    public int PiecesLeft { get; internal set; }
    
    public int MovesMade { get; internal set; }

    internal PlayerSnapshot CreateSnapshot()
    {
        return new PlayerSnapshot
        {
            MovesMade = MovesMade,
            PiecesLeft = PiecesLeft
        };
    }

    internal void LoadSnapshot(PlayerSnapshot snapshot)
    {
        PiecesLeft = snapshot.PiecesLeft;
        MovesMade = snapshot.MovesMade;
    }
}
