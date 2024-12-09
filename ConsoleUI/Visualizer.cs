using System.Text;

namespace ConsoleUI;
using GameBrain;


public static class Visualizer
{
    public static void DrawBoard(TicTacTwoBrain gameInstance)
    {
        string singleFrame = 
            "┌─┬─┐" +
            "│ │ │" +
            "├─┼─┤" +
            "│ │ │" +
            "└─┴─┘";
        
        string doubleFrame = 
            "┌─╥─┐" +
            "│ ║ │" +
            "╞═╬═╡" +
            "│ ║ │" +
            "└─╨─┘";
        
        var sb = new StringBuilder();
        var w = gameInstance.DimX;
        var h = gameInstance.DimX;
        for (var y = 0; y < h; y++)
        {
            if (y == 0)
            {
                var sb0 = new StringBuilder().Append('┌');
                var sb1 = new StringBuilder();
                for (var x = 0; x < w; x++)
                {
                    sb0.Append('╥');
                    Console.Write(x+1);
                }
            }
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
                if (x != gameInstance.DimX - 1)
                {
                    Console.Write("||");
                }
            }

            Console.WriteLine();
            if (y == gameInstance.DimY - 1) break;
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                Console.Write("---");
                if (x != gameInstance.DimX - 1)
                {
                    Console.Write("++");
                }
            }

            Console.WriteLine();
        }
    }
    
    private static string DrawGamePiece(EGamePiece piece) =>
        piece switch
        {
            EGamePiece.X => "X",
            EGamePiece.O => "O",
            _ => " "
        };
}