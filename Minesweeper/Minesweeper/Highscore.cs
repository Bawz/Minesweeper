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
                    _Highscore_Small_Instance = new Highscore();
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
                    _Highscore_Middle_Instance = new Highscore();
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
                    _Highscore_Big_Instance = new Highscore();
                }

                return _Highscore_Big_Instance;
            }
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
            File.WriteAllText("./highscore", string.Join("", _Highscore.Select(entry => entry.ToEntry())));
        }

        public Highscore()
        {
            if (!File.Exists("./highscore/"))
                File.Create("./highscore/");
            
            _Highscore = File.ReadAllLines("./highscore").Select(line => Entry.Load(line)).ToList();
        }

        public string All()
        {
            return string.Join("", _Highscore.Select(entry => entry.AsString()));
        }

        public string MinTime()
        {
            return _Highscore.Count > 10 ?
                _Highscore.Last().Time :
                "00:00:00";
        }

        public void Add(string name, string time)
        {
            _Highscore.Add(new Entry(name, time));
        }
    }
}
