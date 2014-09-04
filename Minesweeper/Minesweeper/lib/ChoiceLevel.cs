using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Minesweeper
{
    public partial class ChoiceLevel : Form
    {
        public ChoiceLevel()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitHighscoreViewButtons();
        }

        void InitHighscoreViewButtons()
        {
            highscoreBigButton.Enabled = Highscore.Big.Any();
            highscoreMiddleButton.Enabled = Highscore.Middle.Any();
            highscoreSmallButton.Enabled = Highscore.Small.Any();
        }

        private void small_field_Click(object sender, EventArgs e)
        {
            this.Hide();

            Thread t = new Thread(new ThreadStart(() => Application.Run(new Game(new Point(8, 8)))));
            t.Start();
            t.Join();

            InitHighscoreViewButtons();
            this.Show();
        }

        private void middle_size_Click(object sender, EventArgs e)
        {
            this.Hide();

            Thread t = new Thread(new ThreadStart(() => Application.Run(new Game(new Point(16, 16)))));
            t.Start();
            t.Join();

            InitHighscoreViewButtons();
            this.Show();
        }

        private void large_size_Click(object sender, EventArgs e)
        {
            this.Hide();

            Thread t = new Thread(new ThreadStart(() => Application.Run(new Game(new Point(20, 20)))));
            t.Start();
            t.Join();

            InitHighscoreViewButtons();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Thread t = new Thread(new ThreadStart(() => Application.Run(new HighscoreForm(0))));
            t.Start();
            t.Join();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Thread t = new Thread(new ThreadStart(() => Application.Run(new HighscoreForm(1))));
            t.Start();
            t.Join();

            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Thread t = new Thread(new ThreadStart(() => Application.Run(new HighscoreForm(2))));
            t.Start();
            t.Join();

            this.Show();
        }
    }
}
