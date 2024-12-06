using GameBrain;
using static MenuSystem.MenuBuilder;

namespace Console_App
{
    internal class GameController
    {
        private readonly TicTacTwoBrain brain;

        public GameController(TicTacTwoBrain brain)
        {
            this.brain = brain;
        }

        internal void gameLoop()
        {
            subMenu("Tic-Tac-Two - game: " + brain.NextMove,
                menuItem("M", "makeMove to [x y]", makeMove)
            )
            .BeforeDraw(() => ConsoleUI.Visualizer.DrawBoard(brain))
            .RunUntilReturnOrExit();
        }

        private void makeMove(string input)
        {
            int x = 0, y = 0;
            try
            {
                var tokens = input.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                x = int.Parse(tokens[0]);
                y = int.Parse(tokens[1]);
            } catch(Exception)
            {
                throw new InvalidDataException("Bad input");
            }            
            brain.MakeMove(x, y);
        }
    }
}
