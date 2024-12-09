using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GameBrain;

public class TicTacTwoBrain
{
    private EGamePiece[,] _gameBoard;
    public EGamePiece NextMove { get; private set; } = EGamePiece.X;

    private readonly GameConfiguration _gameConfiguration;

    private Rectangle _gridRect = Rectangle.Empty;

    public readonly PlayerState PlayerX = new(EGamePiece.X);
    
    public readonly PlayerState PlayerO = new(EGamePiece.O);

    public PlayerState CurrentPlayer => NextMove == EGamePiece.X ? PlayerX : PlayerO;
    
    public TicTacTwoBrain(GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        _gameBoard = new EGamePiece[_gameConfiguration.BoardWidth, _gameConfiguration.BoardHeight];
        _gridRect.X = _gameConfiguration.GridX;
        _gridRect.Y = _gameConfiguration.GridY;
        _gridRect.Width = _gameConfiguration.GridWidth;
        _gridRect.Height = _gameConfiguration.GridHeight;
        PlayerX.PiecesLeft = PlayerO.PiecesLeft = _gameConfiguration.PlayerPieceCount;
        PlayerX.MovesMade = PlayerO.MovesMade = 0;
    }

    public EGamePiece[,] GameBoard => GetBoard();

    public int Width => _gameBoard.GetLength(0);
    
    public int Height => _gameBoard.GetLength(1);

    private EGamePiece[,] GetBoard()
    {
        var copyOfBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++){
                copyOfBoard[x, y] = _gameBoard[x, y];
            }
        }
        return copyOfBoard;
    }

    public void PutPiece(int x, int y)
    {
        validate(CurrentPlayer.PiecesLeft > 0, "Current player have no pieces left");
        validate(IsGridCell(x, y), "Invalid position");
        validate(IsEmpty(x, y), "Cell is not empty");
        validate(IsGridCell(x, y), "Piece should be placed inside grid");
        
        _gameBoard[x, y] = NextMove;
        CurrentPlayer.PiecesLeft--;
        SwapPlayer();
    }

    public void MovePiece(int fromX, int fromY, int toX, int toY)
    {
        validate(CanMovePieceOrGrid(), "Can not move piece at this moment");
        validate(IsGridCell(fromX, fromY), "Invalid source position");
        validate(IsGridCell(toX, toY), "Invalid destination position");
        validate(IsPieceAt(fromX, fromY, NextMove), "Invalid piece at source position");
        validate(IsEmpty(toX, toY), "Destination position is not empty");
        var piece = _gameBoard[fromX, fromY];
        validate(piece == NextMove, "Source position have no piece of current player: " + NextMove);
        
        _gameBoard[toX, toY] = piece; 
        _gameBoard[fromX, fromY] = EGamePiece.Empty;
        SwapPlayer();
    }

    public void MoveGridTo(int x, int y)
    {
        validate(CanMovePieceOrGrid(), "Can not move grid at this moment");
        validate(IsValidPos(x, y) &&
            IsValidPos(x + _gridRect.Width - 1, y + _gridRect.Height - 1), "Invalid position");
        
        _gridRect.X = x;
        _gridRect.Y = y;
        SwapPlayer();
    }

    private void SwapPlayer()
    {
        CheckWin(NextMove);
        CurrentPlayer.MovesMade++;
        NextMove = NextMove == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
    }

    private void CheckWin(EGamePiece piece)
    {
        int[] dxs = [1];
        int[] dys = [0];
        for (int i = 0; i < dxs.Length; i++)
        {
            var dx = dxs[i];
            var dy = dys[i];
            
        }
    }

    private bool IsPieceAt(int x, int y, EGamePiece piece)
    {
        return _gameBoard[x, y] == piece;
    }

    private bool IsValidPos(int x, int y)
    {
        return x >= 0 && y >= 0 && x < Width && y < Height;
    }

    public bool IsEmpty(int x, int y)
    {
        return IsPieceAt(x, y, EGamePiece.Empty);
    }
    
    private bool CanMovePieceOrGrid()
    {
        return PlayerX.MovesMade >= _gameConfiguration.MovePieceAfterNMoves &&
               PlayerO.MovesMade >= _gameConfiguration.MovePieceAfterNMoves;
    }

    private void validate(bool condition, string error)
    {
        if (!condition)
        {
            throw new ValidationException(error);
        }
    }

    public void Reset()
    {
        _gameBoard = new EGamePiece[_gameBoard.GetLength(0),_gameBoard.GetLength(1)];
        NextMove = EGamePiece.X;
    }

    public bool IsGridCell(int x, int y)
    {
        return _gridRect.Contains(x, y);
    }
}
