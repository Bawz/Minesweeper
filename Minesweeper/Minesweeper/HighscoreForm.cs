using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class HighscoreForm : Form
    {
        public HighscoreForm(int lvl)
        {
            InitializeComponent();

            switch (lvl)
            {
                case 0:
                    HighscoreLabel.Text = Highscore.Small.All();
                    break;
                case 1:
                    HighscoreLabel.Text = Highscore.Middle.All();
                    break;
                case 2:
                    HighscoreLabel.Text = Highscore.Big.All();
                    break;
                default:
                    break;
            }
        }
    }
}
