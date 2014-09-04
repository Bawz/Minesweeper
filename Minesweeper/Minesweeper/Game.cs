using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Game : Form
    {
        #region Constants
        // PIXEL SPACE AROUND BUTTONS
        const int SPACE_AROUND = 5;
        // PIXEL SPACE BETWEEN BUTTONS
        const int SPACE_BETWEEN = -2;
        // PIXEL SIZE BUTTON
        const int SIZE_BUTTON = 21;
        // BOMB COUNT PERCENT [100 = 100%]
        const int BOMB_COUNT_PERCENT = 10;
        // BUTTON MOVE HORIZONTAL PIXEL
        const int MOVE_BUTTON_HORIZONTAL = 0;
        // BUTTON MOVE VERTICAL
        const int MOVE_BUTTON_VERTICAL = 50;
        // POSSIBLE COMBINATIONS
        readonly int[][] combinations = new int[][] { new int[2] { 1, 1 }, new int[2] { 1, 0 }, new int[2] { 1, -1 }, new int[2] { 0, 1 }, new int[2] { 0, -1 }, new int[2] { -1, 1 }, new int[2] { -1, 0 }, new int[2] { -1, -1 } };
        #endregion

        #region GameVars

        bool[,] _Field;
        int[,] _BombsAround;
        Button[,] _Buttons;

        bool _Retry = false; /* *-*-* TODO *-*-* */
        int _BombCount;
        System.Diagnostics.Stopwatch _Timer = new System.Diagnostics.Stopwatch();
        Thread _TimersThread;
        Label _TimersLabel;

        Label _BombsLeft;

        #endregion

        // only read
        public bool Retry
        {
            get
            {
                return _Retry;
            }
        }

        ~Game()
        {
            if (_TimersThread != null)
            {
                _TimersThread.Abort();
            }

            _Timer.Stop();
        }

        int Level;
        public Game(Point gameSize)
        {
            InitializeComponent();

            #region InitializeBombBombsAroundButtons_Field

            _Field = new bool[gameSize.X, gameSize.Y];
            _BombsAround = new int[gameSize.X, gameSize.Y];
            _Buttons = new Button[gameSize.X, gameSize.Y];

            switch (gameSize.X * gameSize.Y)
            {
                case 64:
                    Level = 0;
                    break;
                case 256:
                    Level = 1;
                    break;
                case 400:
                    Level = 2;
                    break;
                default:
                    break;
            }

            #endregion

            #region WindowSetup
            // initialize size
            this.Size = new Size(
                2 * SPACE_AROUND + gameSize.X * SIZE_BUTTON + gameSize.X * SPACE_BETWEEN + SIZE_BUTTON + MOVE_BUTTON_HORIZONTAL,
                2 * SPACE_AROUND + gameSize.Y * SIZE_BUTTON + gameSize.Y * SPACE_BETWEEN + SIZE_BUTTON * 2 + MOVE_BUTTON_VERTICAL// why?
                );

            // disable resize
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            #endregion

            #region InitialiteBombOrNotButtons

            Image buttonBackgroundImage = Image.FromFile("Untitled.bmp");
            for (int r = 0; r < gameSize.Y; r++)
            {
                for (int e = 0; e < gameSize.X; e++)
                {
                    Button b = new Button()
                    {
                        Tag = new Point(r, e),
                        Location = new Point(SPACE_AROUND + r * SIZE_BUTTON + r * SPACE_BETWEEN + MOVE_BUTTON_HORIZONTAL, SPACE_AROUND + e * SIZE_BUTTON + e * SPACE_BETWEEN + MOVE_BUTTON_VERTICAL),
                        Size = new Size(SIZE_BUTTON, SIZE_BUTTON),
                        BackColor = Color.LightGray
                    };
                    //b.FlatAppearance.BorderSize = 0;
                    b.FlatStyle = FlatStyle.Flat;

                    // ADD CONTROLS
                    this.Controls.Add(b);

                    #region LeftClickEvent
                    // LEFT CLICK EVENT
                    b.Click += (object sender, EventArgs _e) =>
                    {
                        #region TimerCheckStart
                        if (!_Timer.IsRunning)
                        {
                            _Timer.Start();

                            _TimersThread = new Thread(() =>
                            {
                                while (true)
                                {
                                    if (_TimersLabel.InvokeRequired)
                                    {
                                        _TimersLabel.BeginInvoke((Action)(() =>
                                        {
                                            TimeSpan t = TimeSpan.FromMilliseconds(_Timer.ElapsedMilliseconds);

                                            _TimersLabel.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                                t.Minutes,
                                                t.Seconds,
                                                t.Milliseconds);
                                        }));
                                    }
                                    else
                                    {
                                        TimeSpan t = TimeSpan.FromMilliseconds(_Timer.ElapsedMilliseconds);

                                        _TimersLabel.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                            t.Minutes, 
                                            t.Seconds, 
                                            t.Milliseconds);
                                    }
                                    
                                    Thread.Sleep(100);
                                }
                            });

                            _TimersThread.Start();
                        }
                        #endregion

                        Button button = sender as Button;
                        Point postion = (Point)button.Tag;

                        _Buttons[postion.X, postion.Y].BackColor = Color.White;

                        int r_ = postion.X, // false but works, needs fix
                            e_ = postion.Y;

                        if (_Field[r_, e_])
                        {
                            _Timer.Stop();
                            _TimersThread.Abort();

                            this.Hide();

                            MessageBox.Show("Du bist leider auf eine Bombe getreten!");

                            this.Close();
                        }

                        #region DiscoverAlgorithm
                        List<int[]> pathTodoList = new List<int[]>();
                        List<int[]> pathTodoListTmp = new List<int[]>();
                        pathTodoList.Add(new int[] { r_, e_ });

                        do
                        {
                            foreach (int[] pos in pathTodoList)
                            {
                                if (_Buttons[pos[0], pos[1]].Enabled)
                                {
                                    foreach (int[] cb in combinations)
                                    {
                                        int row = cb[0] + pos[0], ele = cb[1] + pos[1];
                                        if (row >= 0 && ele >= 0 && row < gameSize.Y && ele < gameSize.X && _BombsAround[pos[0], pos[1]] == 0)
                                        {
                                            pathTodoListTmp.Add(new int[] { row, ele });
                                        }
                                    }

                                    _Buttons[pos[0], pos[1]].Enabled = false;
                                    _Buttons[pos[0], pos[1]].BackColor = Color.White;

                                    if (_BombsAround[pos[0], pos[1]] > 0)
                                    {
                                        _Buttons[pos[0], pos[1]].Text = _BombsAround[pos[0], pos[1]].ToString();

                                        switch (_BombsAround[pos[0], pos[1]] - 1)
                                        {
                                            case 0:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.Blue;
                                                break;
                                            case 1:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.Green;
                                                break;
                                            case 2:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.Red;
                                                break;
                                            case 3:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.DarkBlue;
                                                break;
                                            case 4:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.Brown;
                                                break;
                                            case 5:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.Cyan;
                                                break;
                                            case 6:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.Black;
                                                break;
                                            case 7:
                                                _Buttons[pos[0], pos[1]].ForeColor = Color.Gray;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }

                            pathTodoList = new List<int[]>(pathTodoListTmp);
                            pathTodoListTmp.Clear();
                        }
                        while (pathTodoList.Count > 0);
                        #endregion
                    };
                    #endregion

                    #region MouseClickRigthEvent
                    b.MouseUp += (object sender, MouseEventArgs _e) =>
                    {
                        if (_e.Button == System.Windows.Forms.MouseButtons.Right)
                        {
                            if (((Button)sender).BackgroundImage == null)
                            {
                                ((Button)sender).BackgroundImage = buttonBackgroundImage;

                                if (--_BombCount == 0)
                                {
                                    string min = Highscore.ByID(Level).MinTime();

                                    string[] sAry = min.Split(':');
                                    int[] ary = sAry.Select(s => int.Parse(s)).ToArray();

                                    long eTime = _Timer.ElapsedMilliseconds;
                                    int x = ary[0] * 60 * 1000 + ary[1] * 1000 + ary[2] * 10;

                                    bool bTmp = true;
                                    for (int r_ = 0; r_ < gameSize.Y; r_++)
                                    {
                                        for (int e_ = 0; e_ < gameSize.X; e_++)
                                        {
                                            if (_Buttons[r_, e_].BackgroundImage != null)
                                            {
                                                bTmp = bTmp && _Field[r_, e_];
                                            }
                                        }
                                    }

                                    if (bTmp)
                                    {
                                        _Retry = true;

                                        if (eTime < x)
                                        {
                                            this.Hide();

                                            var ts = TimeSpan.FromMilliseconds(eTime);
                                            string s = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Minutes, ts.Seconds, ts.Milliseconds);

                                            Bestenliste bl = new Bestenliste(Level, s);

                                            Thread t = new Thread(new ThreadStart(() =>
                                                Application.Run(bl)
                                            ));

                                            t.Start();
                                            t.Join();

                                            this.Close();
                                        }
                                        else
                                        {
                                            this.Close();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ((Button)sender).BackgroundImage = null;
                                _BombCount++;
                            }

                            _BombsLeft.Text = _BombCount.ToString();
                        }
                    };
                    #endregion

                    _Buttons[r, e] = b;
                }
            }
            #endregion

            #region BombCount

            // how many bombs in field
            int bombCount = _BombCount = ((gameSize.X * gameSize.Y) * BOMB_COUNT_PERCENT) / 100;
            
            //bombCount--;
            #endregion

            #region InitializeTimeAndBombLeftLabel
            // initialize bombs left and time label
            Label l1 = new Label()
            {
                Text = "Zeit: ",
                Location = new Point(SPACE_AROUND, SPACE_AROUND)
            };

            _TimersLabel = new Label()
            {
                Text = "Es geht noch nicht los!",
                Location = new Point(SPACE_AROUND + l1.Width, SPACE_AROUND)
            };

            Label l2 = new Label()
            {
                Text = "Bomben: ",
                Location = new Point(SPACE_AROUND, SPACE_AROUND + l1.Height)
            };

            _BombsLeft = new Label()
            {
                Text = bombCount.ToString(),
                Location = new Point(SPACE_AROUND + _TimersLabel.Width, SPACE_AROUND + l1.Height)
            };

            this.Controls.Add(l2);
            this.Controls.Add(l1);
            this.Controls.Add(_TimersLabel);
            this.Controls.Add(_BombsLeft);

            #endregion

            #region GenerateRandomBombs

            Random rnd = new Random();
            do 
            {
                for (int r = 0; r < gameSize.Y && bombCount > 0; r++)
                {
                    for (int e = 0; e < gameSize.X && bombCount > 0; e++)
                    {
                        if (rnd.Next(0, 100) < BOMB_COUNT_PERCENT && !_Field[r, e] &&
                            !combinations.Any(cb =>
                            {
                                int r_ = cb[0] + r,
                                    e_ = cb[1] + e;

                                // Before validate that prevents 3x3 boxes check that the center is inside the field
                                return r_ >= 0 && r_ < gameSize.Y && e_ >= 0 && e_ < gameSize.X && combinations.All(_cb =>
                                {
                                    int sum_r = r_ + _cb[0],
                                        sum_e = e_ + _cb[1];

                                    // first validate and _Field(true) check (bomb)
                                    bool isBomb = sum_r != r && sum_e != e && sum_e >= 0 && sum_e < gameSize.X && sum_r >= 0 && sum_r < gameSize.Y &&
                                        _Field[sum_r, sum_e];

                                    return isBomb;
                                });
                            }))
                        {
                            _Field[r, e] = true;
                            bombCount--;
                        }
                    }
                }
            }
            while (bombCount > 0);
            #endregion

            #region SetBombsAroundInteger
            // set integer of bombs around (for show when field clicked)
            for (int r = 0; r < gameSize.Y; r++)
            {
                for (int e = 0; e < gameSize.X; e++)
                {
                    if (!_Field[r, e])
                    {
                        int _bombCount = 0;
                        foreach (int[] cb in combinations)
                        {
                            int r_ = cb[0] + r,
                                e_ = cb[1] + e;

                            if (r_ >= 0 && r_ < gameSize.Y && e_ >= 0 && e_ < gameSize.X && _Field[r_, e_])
                            {
                                _bombCount++;
                            }
                        }
                        _BombsAround[r, e] = _bombCount;
                    }
                }
            }
            #endregion
        }
    }
}
