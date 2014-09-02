using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Game game;
            ChoiceLevel choiceLevel;

            do
            {
                choiceLevel = new ChoiceLevel();
                Application.Run(choiceLevel);

                if (choiceLevel.GameSize == null) return;

                game = new Game(choiceLevel.GameSize);
                Application.Run(game);
            }
            while (game.Retry);
        }
    }
}
