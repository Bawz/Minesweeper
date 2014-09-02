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
    public partial class ChoiceLevel : Form
    {
        Point _GameSize;

        public Point GameSize
        {
            get
            {
                return _GameSize;
            }
        }

        public ChoiceLevel()
        {
            InitializeComponent();
        }

        private void small_field_Click(object sender, EventArgs e)
        {
            _GameSize = new Point(8, 8);
            Close();
        }

        private void middle_size_Click(object sender, EventArgs e)
        {
            _GameSize = new Point(16, 16);
            Close();
        }

        private void large_size_Click(object sender, EventArgs e)
        {
            _GameSize = new Point(20, 20);
            Close();
        }
    }
}
