using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Model
{
    public class Game : INotifyPropertyChanged
    {
        private int count;
        public int Count { get { return count; } set { count = value; OnPropertyChanged("Count"); } }

        private int record;
        public int Record { get { return record; } set { record = value; OnPropertyChanged("Record"); } }


        private bool IsMoveble = false;

        Action action;

        Action<int> counter;
       

        private CellItem[] playField;
        public CellItem[] PlayField
        {
            get { return playField; }
            set
            {
                playField = value;
                OnPropertyChanged("PlayField");
            }
        }

        public Game()
        {
            Count = 0;
            PlayField = new CellItem[16];
            int k = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    PlayField[k] = new CellItem(x, y);
                    k++;
                }
            }
            SetRandomCell();
            SetRandomCell();

            action = () => IsMoveble = true;

            counter = (x) =>
            {
                Count = Count + x;
                if (Count > Record)
                    Record = Count;
            }; 
        }

        internal void MoveUp()
        {
            IsMoveble = false;
            for (int i = 1; i < 4; i++)
            {
                List<CellItem> list = playField.Where(x => x.Ycoord == i).ToList();
                foreach(var item in list)
                {
                    item.Merge(playField.Where(x => x.Xcoord == item.Xcoord && x.Ycoord < item.Ycoord).Reverse().ToArray(), action, counter);
                }
            }
            if (IsMoveble)
                SetRandomCell();
            OnPropertyChanged("PlayField");
        }

        internal void MoveDown()
        {
            IsMoveble = false;
            for (int i = 2; i >= 0; i--)
            {
                List<CellItem> list = playField.Where(x => x.Ycoord == i).ToList();
                foreach (var item in list)
                {
                    item.Merge(playField.Where(x => x.Xcoord == item.Xcoord && x.Ycoord > item.Ycoord).ToArray(), action, counter);
                }
            }
            if (IsMoveble)
                SetRandomCell();
            OnPropertyChanged("PlayField");
        }

        internal void MoveLeft()
        {
            IsMoveble = false;
            for (int i = 1; i < 4; i++)
            {
                List<CellItem> list = playField.Where(x => x.Xcoord == i).ToList();
                foreach (var item in list)
                {
                    item.Merge(playField.Where(x => x.Ycoord == item.Ycoord && x.Xcoord < item.Xcoord).Reverse().ToArray(), action, counter);
                }
            }
            if (IsMoveble)
                SetRandomCell();
            OnPropertyChanged("PlayField");
        }

        internal void MoveRight()
        {
            IsMoveble = false;
            for (int i = 2; i >= 0; i--)
            {
                List<CellItem> list = playField.Where(x => x.Xcoord == i).ToList();
                foreach (var item in list)
                {
                    item.Merge(playField.Where(x => x.Ycoord == item.Ycoord && x.Xcoord > item.Xcoord).ToArray(), action, counter);
                }
            }
            if (IsMoveble)
                SetRandomCell();
            OnPropertyChanged("PlayField");
        }

        private void SetRandomCell()
        {
            var random = new Random();
            CellItem cell = PlayField.Where(x => x.IsEmpty).OrderBy(x => random.Next()).First();
            if (cell != null)
                cell.SetRandomValue();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
