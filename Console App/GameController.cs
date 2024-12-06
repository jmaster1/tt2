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

        internal string gameLoop()
        {
            Exception? lastError = null;
            for (; ; )
            {
                try
                {
                    Console.Clear();
                    ConsoleUI.Visualizer.DrawBoard(brain);
                    if (lastError != null)
                    {
                        Console.WriteLine("-------------!!!-------------");
                        Console.WriteLine(lastError.Message);
                        Console.WriteLine("-------------!!!-------------");
                        lastError = null;
                    }
                    subMenu("Tic-Tac-Two - game: " + brain.NextMove,
                        menuItem("M", "makeMove to [x y]", makeMove)
                    )
                    .Run();
                } catch(Exception ex)
                {
                    lastError = ex;
                }
            }
            return "";
        }

        private string makeMove(string input)
        {
            int x = 0, y = 0;
            try
            {
                var tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                x = int.Parse(tokens[0]);
                y = int.Parse(tokens[1]);
            } catch(Exception)
            {
                throw new InvalidDataException("Bad input");
            }            
            brain.MakeMove(x, y);
            return "?";
        }
    }
}
