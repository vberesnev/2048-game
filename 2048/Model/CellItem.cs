using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2048.Model
{
    public class CellItem : INotifyPropertyChanged
    {
        private Dictionary<int, Brush> colorDict = new Dictionary<int, Brush>()
        {
            { 0, Brushes.LemonChiffon},
            { 1, Brushes.Wheat },
            { 2, Brushes.SandyBrown },
            { 3, Brushes.Gold },
            { 4, Brushes.Goldenrod },
            { 5, Brushes.Orange},
            { 6, Brushes.DarkOrange },
            { 7, Brushes.LightCoral },
            { 8, Brushes.IndianRed },
            { 9, Brushes.Salmon},
            { 10, Brushes.Coral},
            { 11, Brushes.Tomato},
            { 12, Brushes.Orange },
            { 13, Brushes.Red },
            { 14, Brushes.Crimson },
            { 15, Brushes.Firebrick },
            { 16, Brushes.DarkRed },
            { 17, Brushes.Sienna },
            { 18, Brushes.Brown },
            { 19, Brushes.Black },
            { 20, Brushes.Black },
            { 21, Brushes.Black },
            { 22, Brushes.Black },
            { 23, Brushes.Black },
            { 24, Brushes.Black },
            { 25, Brushes.Black },
            { 26, Brushes.Black },
            { 27, Brushes.Black }
        };


        public int value;
        public int Value
        {
            get { return Convert.ToInt32(Math.Pow(2, value)); }
            private set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public Brush BackColor
        {
            get { return colorDict[value]; }
        }


        public int Xcoord { get; private set; }
        public int Ycoord { get; private set; }


        public CellItem(int x, int y)
        {
            Xcoord = x;
            Ycoord = y;
        }

        public CellItem()
        {
        }

        public bool IsEmpty
        {
            get { return value == 0; }
        }

        public void Plus()
        {
            value++;
        }

        public void Merge(CellItem[] items, Action action, Action<int> counter)
        {
            if (this.IsEmpty) return;
            CellItem tempItem = null;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].IsEmpty)
                {
                    tempItem = items[i];
                    continue;
                }
                else
                {
                    if (items[i].value == this.value)
                    {
                        items[i].Plus();
                        this.Value = 0;
                        counter(items[i].Value);
                        action();
                        return;
                    }
                    break;
                }
            }
            if (tempItem != null)
            {
                tempItem.Value = this.value;
                this.Value = 0;
                action();
            }
        }

        internal void SetRandomValue()
        {
            Thread.Sleep(100);
            var random = new Random();
            int[] arr = new int[] { 1, 2 };
            value = arr[random.Next(arr.Length)];
        }

        public override string ToString()
        {
            if (IsEmpty) return string.Empty;
            else return Value.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
