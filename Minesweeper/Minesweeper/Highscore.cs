using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minesweeper
{
    class Highscore
    {
        static Highscore _Highscore_Small_Instance;
        public static Highscore Small
        {
            get
            {
                if (_Highscore_Small_Instance == null)
                {
                    _Highscore_Small_Instance = new Highscore(0);
                }

                return _Highscore_Small_Instance;
            }
        }

        static Highscore _Highscore_Middle_Instance;
        public static Highscore Middle
        {
            get
            {
                if (_Highscore_Middle_Instance == null)
                {
                    _Highscore_Middle_Instance = new Highscore(1);
                }

                return _Highscore_Middle_Instance;
            }
        }

        static Highscore _Highscore_Big_Instance;
        public static Highscore Big
        {
            get
            {
                if (_Highscore_Big_Instance == null)
                {
                    _Highscore_Big_Instance = new Highscore(2);
                }

                return _Highscore_Big_Instance;
            }
        }

        public static Highscore ByID(int id)
        {
            switch (id)
            {
                case 0:
                    return Highscore.Small;
                case 1:
                    return Highscore.Middle;
                case 2:
                    return Highscore.Big;
            }
            return null;
        }

        class Entry
        {
            public string Name;
            public string Time;

            public Entry(string n, string t)
            {
                Name = n;
                Time = t;
            }

            public string ToEntry()
            {
                return string.Format("{0};{1};" + Environment.NewLine, Name, Time);
            }

            public string AsString()
            {
                return string.Format("{0}\t{1}" + Environment.NewLine, Name, Time);
            }

            public static Entry Load(string s)
            {
                string[] ary = s.Split(';');

                return new Entry(ary[0], ary[1]);
            }
        }

        List<Entry> _Highscore;

        ~Highscore()
        {
            File.WriteAllText("./highscore/" + Level, string.Join("", _Highscore.Select(entry => entry.ToEntry())));
        }

        string Level;
        public Highscore(int lvl)
        {
            switch (lvl)
            {
                case 0:
                    Level = "small";
                    break;
                case 1:
                    Level = "middle";
                    break;
                case 2:
                    Level = "big";
                    break;
                default:
                    break;
            }

            if (!File.Exists("./highscore/" + Level))
                File.Create("./highscore/" + Level);
            
            _Highscore = File.ReadAllLines("highscore/" + Level).Select(line => Entry.Load(line)).ToList();
        }

        public string All()
        {
            return string.Join("", _Highscore.Select(entry => entry.AsString()));
        }

        public string MinTime()
        {
            return _Highscore.Count >= 10 ?
                _Highscore.Last().Time :
                "60:60:99";
        }

        public void Add(string name, string time)
        {
            if (_Highscore.Count > 10)
            {
                _Highscore = _Highscore.OrderBy(entry =>
                {
                    int[] ary = entry.Time.Split(';').Select(s => int.Parse(s)).ToArray();

                    // mins to ms, secs to ms add ms, ms * 10 because x,10 = 100ms
                    return ary[0] * 60 * 1000 + ary[1] * 1000 + ary[2] * 10;
                }).ToList();
            }
            else
            {
                _Highscore.Add(new Entry(name, time));
            }
        }
    }
}
