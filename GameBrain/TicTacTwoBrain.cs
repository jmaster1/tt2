namespace GameBrain;

public class TicTacTwoBrain
{
    private EGamePiece[,] _gameBoard;
    public EGamePiece NextMove { get; private set; } = EGamePiece.X;

    private GameConfiguration _gameConfiguration;

    public TicTacTwoBrain(GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        _gameBoard = new EGamePiece[_gameConfiguration.BoardWidth, _gameConfiguration.BoardHeight];
    }

    public EGamePiece[,] GameBoard
    {
        get => GetBoard();
        private set => _gameBoard = value;
    }

    public int DimX => _gameBoard.GetLength(0);
    public int DimY => _gameBoard.GetLength(1);

    private EGamePiece[,] GetBoard()
    {
        var copyOfBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        for (var x = 0; x < _gameBoard.GetLength(0); x++)
        {
            for (var y = 0; y < _gameBoard.GetLength(1); y++){
                copyOfBoard[x, y] = _gameBoard[x, y];
            }
            
        }
        return copyOfBoard;
    }

    public bool MakeMove(int x, int y)
    {
        if (_gameBoard[x, y] != EGamePiece.Empty)
        {
            return false;
        }

        _gameBoard[x, y] = NextMove;
 
        // flip the next piece
        NextMove = NextMove == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return true;
    }

    public void Reset()
    {
        _gameBoard = new EGamePiece[_gameBoard.GetLength(0),_gameBoard.GetLength(1)];
        NextMove = EGamePiece.X;
    }
}