using Newtonsoft.Json;
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
            { 19, Brushes.Black }
        };

        [JsonRequired]
        [JsonProperty("val")]
        private int value;
        [JsonIgnore]
        public int Value
        {
            get { return Convert.ToInt32(Math.Pow(2, value)); }
            private set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        [JsonIgnore]
        public Brush BackColor
        {
            get { return colorDict[value]; }
        }

        [JsonProperty("x")]
        public int Xcoord { get; private set; }
        [JsonProperty("y")]
        public int Ycoord { get; private set; }

        [JsonIgnore]
        public bool IsEmpty
        {
            get { return value == 0; }
        }

        public CellItem(int x, int y)
        {
            Xcoord = x;
            Ycoord = y;
        }

        [JsonConstructor]
        public CellItem(int val, int x, int y)
        {
            this.Value = val;
            this.Xcoord = x;
            this.Ycoord = y;
        }

        public void Plus()
        {
            value++;
        }

        public void Merge(CellItem[] items, Action moveAction, Action<int> counterAction, Action<string> messageAction)
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
                        counterAction(items[i].Value);
                        moveAction();
                        return;
                    }
                    break;
                }
            }
            if (tempItem != null)
            {
                tempItem.Value = this.value;
                this.Value = 0;
                moveAction();
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
