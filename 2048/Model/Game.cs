using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace _2048.Model
{
    public class Game : INotifyPropertyChanged
    {
        private readonly string SAVE_PATH = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "save.json");

        private int count;
        public int Count { get { return count; } set { count = value; OnPropertyChanged("Count"); } }

        private int record;
        public int Record { get { return record; } set { record = value; OnPropertyChanged("Record"); } }

        private bool IsMoveble = false;

        Action moveAction;
        Action<int> counterAction;
        Action<string> messageAction;

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

        public Game(Action<string> message)
        {
            messageAction = message;
            PlayField = new CellItem[16];

            Load();
          
            moveAction = () => IsMoveble = true;

            counterAction = (x) =>
            {
                Count = Count + x;
                if (Count > Record)
                    Record = Count;
            }; 
        }

        public Game() { }
        
        private void Load()
        {
            if (File.Exists(SAVE_PATH))
            {
                try
                {
                    string result = File.ReadAllText(SAVE_PATH);
                    var loadGame = JsonConvert.DeserializeObject<Game>(result);
                    this.Count = loadGame.Count;
                    this.Record = loadGame.Record;
                    this.PlayField = loadGame.PlayField;
                }
                catch
                {
                    NewGame();
                }
            }
            else
            {
                NewGame();
            }
        }

        private void NewGame()
        {
            Count = 0;
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
            OnPropertyChanged("PlayField");
        }

        internal void Save()
        {
            try
            {
                string saveInfo = JsonConvert.SerializeObject(this);
                File.WriteAllText(SAVE_PATH, saveInfo);
            }
            catch (Exception ex)
            {
                messageAction(ex.Message);
            }
        }

        internal void Restart()
        {
            NewGame();
        }

        internal void MoveUp()
        {
            IsMoveble = false;
            for (int i = 1; i < 4; i++)
            {
                List<CellItem> list = playField.Where(x => x.Ycoord == i).ToList();
                foreach(var item in list)
                {
                    item.Merge(playField.Where(x => x.Xcoord == item.Xcoord && x.Ycoord < item.Ycoord).Reverse().ToArray(), moveAction, counterAction, messageAction);
                }
            }
            MoveFinish();
        }

        internal void MoveDown()
        {
            IsMoveble = false;
            for (int i = 2; i >= 0; i--)
            {
                List<CellItem> list = playField.Where(x => x.Ycoord == i).ToList();
                foreach (var item in list)
                {
                    item.Merge(playField.Where(x => x.Xcoord == item.Xcoord && x.Ycoord > item.Ycoord).ToArray(), moveAction, counterAction, messageAction);
                }
            }
            MoveFinish();
        }

        internal void MoveLeft()
        {
            IsMoveble = false;
            for (int i = 1; i < 4; i++)
            {
                List<CellItem> list = playField.Where(x => x.Xcoord == i).ToList();
                foreach (var item in list)
                {
                    item.Merge(playField.Where(x => x.Ycoord == item.Ycoord && x.Xcoord < item.Xcoord).Reverse().ToArray(), moveAction, counterAction, messageAction);
                }
            }
            MoveFinish();
        }

        internal void MoveRight()
        {
            IsMoveble = false;
            for (int i = 2; i >= 0; i--)
            {
                List<CellItem> list = playField.Where(x => x.Xcoord == i).ToList();
                foreach (var item in list)
                {
                    item.Merge(playField.Where(x => x.Ycoord == item.Ycoord && x.Xcoord > item.Xcoord).ToArray(), moveAction, counterAction, messageAction);
                }
            }
            MoveFinish();
        }

        private void MoveFinish()
        {
            if (IsMoveble)
                SetRandomCell();
            if (!IsMoveble && PlayField.Where(x => x.IsEmpty).Count() == 0)
                messageAction($"Игра закончена, вы заработали {Count} очков");

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
