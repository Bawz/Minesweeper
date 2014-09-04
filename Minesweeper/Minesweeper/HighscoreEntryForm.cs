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
    public partial class Bestenliste : Form
    {
        int Level;
        string Time;

        public Bestenliste(int lvl, string time)
        {
            Level = lvl;

            InitializeComponent();

            CancelButton.Click += CancelButton_Click;
            ApplyButton.Click += ApplyButton_Click;
        }

        void ApplyButton_Click(object sender, EventArgs e)
        {
            if (NameBox.Text != "")
            {
                switch (Level)
                {
                    case 0:
                        Highscore.Small.Add(NameBox.Text, Time);
                        break;
                    case 1:
                        Highscore.Middle.Add(NameBox.Text, Time);
                        break;
                    case 2:
                        Highscore.Big.Add(NameBox.Text, Time);
                        break;
                    default:
                        break;
                }
            }
            this.Hide();

            System.Threading.Thread t = new System.Threading.Thread(
                new System.Threading.ThreadStart(
                    () => Application.Run(new HighscoreForm(Level))));

            t.Start();
            t.Join();

            this.Close();
        }

        void CancelButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            this.Close();
        }
    }
}
