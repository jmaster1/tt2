namespace GameBrain;

public class GameSnapshot
{
    public string Name { get; set; } = default!;
    public GameConfiguration Configuration { get; set; }
    public int GridX { get; set; }
    public int GridY { get; set; }
    public PlayerSnapshot PlayerX { get; set; } = null!;
    public PlayerSnapshot PlayerO { get; set; } = null!;
    public List<PieceSnapshot>? Pieces { get; set; }
    public EGamePiece NextMove { get; set; }
    public EGamePiece Winner { get; set; } = EGamePiece.Empty;
}
