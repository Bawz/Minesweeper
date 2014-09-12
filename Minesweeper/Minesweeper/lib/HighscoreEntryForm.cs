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
            Time = time;

            InitializeComponent();

            CancelButton.Click += CancelButton_Click;
            ApplyButton.Click += ApplyButton_Click;
            NameBox.KeyDown += NameBox_KeyDown;

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        void NameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                Apply();
        }

        void ApplyButton_Click(object sender, EventArgs e)
        {
            Apply();
        }

        void Apply()
        {
            if (NameBox.Text != "")
            {
                Highscore.ByID(Level).Add(NameBox.Text, Time);
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
