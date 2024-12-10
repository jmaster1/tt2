using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GameBrain;

public class TicTacTwoBrain
{
    private EGamePiece[,] _gameBoard = null!;
    
    public int Width => _gameBoard.GetLength(0);
    
    public int Height => _gameBoard.GetLength(1);
    
    public EGamePiece NextMove { get; private set; } = EGamePiece.X;
    
    public EGamePiece Winner { get; private set; } = EGamePiece.Empty;

    private GameConfiguration _gameConfiguration;

    private Rectangle _gridRect = Rectangle.Empty;

    public readonly PlayerState PlayerX = new(EGamePiece.X);
    
    public readonly PlayerState PlayerO = new(EGamePiece.O);

    public PlayerState CurrentPlayer => (NextMove switch
    {
        EGamePiece.X => PlayerX,
        EGamePiece.O => PlayerO,
        _ => null
    })!;

    public int WinSequence => _gameConfiguration.WinSequence;
    
    public int MovePieceAfterNMoves => _gameConfiguration.MovePieceAfterNMoves;

    public void PutPiece(int x, int y)
    {
        ValidateCanMove();
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
        ValidateCanMove();
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
        ValidateCanMove();
        validate(CanMovePieceOrGrid(), "Can not move grid at this moment");
        validate(IsValidPos(x, y) &&
            IsValidPos(x + _gridRect.Width - 1, y + _gridRect.Height - 1), "Invalid position");
        
        _gridRect.X = x;
        _gridRect.Y = y;
        SwapPlayer();
    }

    private void SwapPlayer()
    {
        CurrentPlayer.MovesMade++;
        var opponent = NextMove == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        if (CheckWin(NextMove))
        {
            Winner = NextMove;
        }
        else if (CheckWin(opponent))
        {
            Winner = opponent;
        }
        NextMove = Winner == EGamePiece.Empty ? opponent : EGamePiece.Empty;
    }

    private bool CheckWin(EGamePiece piece)
    {
        var ret = false;
        for (var y = _gridRect.Top; y < _gridRect.Bottom && !ret; y++)
        {
            ret = CheckWin(piece, _gridRect.Left, y, 1, 0);
        }
        for (var x = _gridRect.Left; x < _gridRect.Right && !ret; x++)
        {
            ret = CheckWin(piece, x, _gridRect.Top, 0, 1);
        }
        for (var d = -_gridRect.Height + 1; d < _gridRect.Width && !ret; d++)
        {
            var x = _gridRect.Left + Math.Max(d, 0);
            var y = _gridRect.Top + Math.Max(-d, 0);
            ret = CheckWin(piece, x, y, 1, 1);
        }
        for (var d = -_gridRect.Width + 1; d < _gridRect.Height && !ret; d++)
        {
            var x = _gridRect.Left + Math.Max(-d, 0);
            var y = _gridRect.Top + Math.Max(d, 0);
            ret = CheckWin(piece, x, y, 1, -1);
        }
        return ret;
    }

    private bool CheckWin(EGamePiece piece, int x0, int y0, int dx, int dy)
    {
        var sequence = 0;
        for (int x = x0, y = y0; _gridRect.Contains(x, y); x += dx, y += dy)
        {
            if (GetPieceAt(x, y) == piece)
            {
                if (++sequence == WinSequence)
                {
                    return true;
                }
            }
            else
            {
                sequence = 0;
            }
        }

        return false;
    }
    
    private void ValidateCanMove()
    {
        validate(NextMove != EGamePiece.Empty, "Game is finished, can't move");
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

    public EGamePiece GetPieceAt(int x, int y)
    {
        return _gameBoard[x, y];
    }

    public void LoadConfig(GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        _gameBoard = new EGamePiece[_gameConfiguration.BoardWidth, _gameConfiguration.BoardHeight];
        _gridRect.X = _gameConfiguration.GridX;
        _gridRect.Y = _gameConfiguration.GridY;
        _gridRect.Width = _gameConfiguration.GridWidth;
        _gridRect.Height = _gameConfiguration.GridHeight;
        PlayerX.PiecesLeft = PlayerO.PiecesLeft = _gameConfiguration.PlayerPieceCount;
        PlayerX.MovesMade = PlayerO.MovesMade = 0;
        Winner = EGamePiece.Empty;
    }
    
    public GameSnapshot CreateSnapshot()
    {
        var pieces = 
            from x in Enumerable.Range(0, Width)
            from y in Enumerable.Range(0, Height)
            where GetPieceAt(x, y) != EGamePiece.Empty
            select new PieceSnapshot{ X = x, Y = y, Piece = GetPieceAt(x, y) };
        var snapshot = new GameSnapshot
        {
            Configuration = _gameConfiguration,
            PlayerX = PlayerX.CreateSnapshot(),
            PlayerO = PlayerO.CreateSnapshot(),
            NextMove = NextMove,
            Winner = Winner,
            GridX = _gridRect.X,
            GridY = _gridRect.Y,
            Pieces = pieces.ToList()
        };
        return snapshot;
    }
    
    public void LoadSnapshot(GameSnapshot snapshot)
    {
        LoadConfig(snapshot.Configuration);
        _gridRect.X = snapshot.GridX;
        _gridRect.Y = snapshot.GridY;
        NextMove = snapshot.NextMove;
        Winner = snapshot.Winner;
        PlayerX.LoadSnapshot(snapshot.PlayerX);
        PlayerO.LoadSnapshot(snapshot.PlayerO);
        snapshot.Pieces?.ForEach(pieceSnapshot =>
        {
            _gameBoard[pieceSnapshot.X, pieceSnapshot.Y] = pieceSnapshot.Piece;
        });
    }
}
