using System.Text;

namespace ConsoleUI;
using GameBrain;


public class Visualizer(TicTacTwoBrain gameInstance)
{
    private static readonly string[] Frame =
    [
        "    X   X  ",
        "  ┌───┬───┐",
        "Y │ ? │ ? │",
        "  ├───┼───┤",
        "Y │ ? │ ? │",
        "  └───┴───┘"
    ];
    
    static readonly string[] GridFrame =
    [
        "    X   X  ",
        "  ╔═══╦═══╗",
        "Y ║ ? ║ ? ║",
        "  ╠═══╬═══╣",
        "Y ║ ? ║ ? ║",
        "  ╚═══╩═══╝"
    ];
    
    private const char PlaceholderPiece = '?';
    private const char PlaceholderRow = 'Y';
    private const char PlaceholderCol = 'X';

    private const int CellOffsetX = 2;
    private const int CellOffsetY = 2;
    private const int CellWidth = 4;
    private const int CellHeight = 2;

    private int W => gameInstance.DimX;
    private int H => gameInstance.DimY;

    readonly StringBuilder _sb = new();

    public string Render()
    {
        _sb.Clear();
        for (var y = 0; y < H; y++)
        {
            RenderRow(y);
        }
        return _sb.ToString();
    }
    
    private void RenderRow(int y)
    {
        var firstRow = y == 0;
        if (firstRow)
        {
            RenderRowLines(y, 0, CellOffsetY);
        }
        var lastRow = y == H - 1;
        var dcy = lastRow ? CellHeight : 0;
        RenderRowLines(y, CellOffsetY + dcy, CellOffsetY + CellHeight + dcy);
    }

    private void RenderRowLines(int y, int lFrom, int lTo)
    {
        for (var l = lFrom; l < lTo; l++)
        {
            RenderRowLine(y, l);    
        }
    }

    private void RenderRowLine(int y, int l)
    {
        for (var x = 0; x < W; x++)
        {
            RenderRowLineCell(y, l, x);
        }
        _sb.AppendLine();
    }

    private void RenderRowLineCell(int y, int l, int x)
    {
        var firstCol = x == 0;
        var source = gameInstance.IsGridCell(x, y) ? GridFrame[l] : Frame[l];
        if (firstCol)
        {
            RenderRowLineCellChars(x, y, source, 0, CellOffsetX);
        }
        var lastCol = x == W - 1;
        var dcx = lastCol ? CellWidth : 0;
        RenderRowLineCellChars(x, y, source, CellOffsetX + dcx, CellOffsetX + CellWidth + dcx);
        if (lastCol)
        {
            RenderRowLineCellChars(x, y, source, CellOffsetX + CellWidth + dcx, source.Length);
        }
    }

    private void RenderRowLineCellChars(int x, int y, string source, int cFrom, int cTo)
    {
        for (var c = cFrom; c < cTo; c++)
        {
            Append(source, c, x, y);
        }
    }

    private void Append(string source, int charPos, int x, int y)
    {
        var c = source[charPos];
        c = c switch
        {
            //PlaceholderPiece => PieceToChar(gameInstance.GameBoard[x, y]),
            PlaceholderPiece => gameInstance.IsGridCell(x, y) ? '+' : ' ',
            PlaceholderCol => Index2Title(x),
            PlaceholderRow => Index2Title(y),
            _ => c
        };
        _sb.Append(c);
    }

    private static char PieceToChar(EGamePiece eGamePiece) =>
        eGamePiece switch
        {
            EGamePiece.Empty => ' ',
            EGamePiece.X => 'x',
            EGamePiece.O => 'o',
            _ => throw new ArgumentOutOfRangeException(nameof(eGamePiece), eGamePiece, null)
        };

    private static char Index2Title(int xy) => (char) ('1' + xy);

    public static int Title2Index(char t) => t - '1';
}