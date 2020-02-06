using _2048.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _2048.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {

        private Game game;
        public Game Game
        {
            get { return game; }
            set
            {
                game = value;
                OnPropertyChanged("Game");
            }
        }

        Action<string> messageAction; 

        private List<CellItem> list;
        public List<CellItem> List
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged("List");
            }
        }

        public GameViewModel(Action<string> mess)
        {
            messageAction = mess;
            Game = new Game(messageAction);
        }

        internal void KeyUp()
        {
            Game.MoveUp();
        }

        internal void KeyDown()
        {
            Game.MoveDown();
        }

        internal void KeyLeft()
        {
            Game.MoveLeft();
        }

        internal void KeyRight()
        {
            Game.MoveRight();
        }

        internal void Save()
        {
            Game.Save();
        }

        internal void Restart()
        {
            Game.Restart();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        
    }
}
