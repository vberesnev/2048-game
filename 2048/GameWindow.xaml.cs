using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.GameViewModel dataContext;

        Action<string> showMessage;

        public MainWindow()
        {
            InitializeComponent();

            showMessage = new Action<string>((x) =>
            {
                MessageBox.Show(x);
            });

            dataContext = new ViewModel.GameViewModel(showMessage);
            DataContext = dataContext;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    dataContext.KeyUp();
                    break;
                case Key.Left:
                    dataContext.KeyLeft();
                    break;
                case Key.Down:
                    dataContext.KeyDown();
                    break;
                case Key.Right:
                    dataContext.KeyRight();
                    break;
                case Key.F5:
                    dataContext.Restart();
                    break;
                case Key.Escape:
                    this.Visibility = Visibility.Hidden;
                    dataContext.Save();
                    this.Close();
                    break;
            }
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
