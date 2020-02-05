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

        public MainWindow()
        {
            InitializeComponent();
            dataContext = new ViewModel.GameViewModel();
            DataContext = dataContext;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                dataContext.KeyUp();
                return;
            }
            
            if (e.Key == Key.Left)
            {
                dataContext.KeyLeft();
                return;
            }

            if (e.Key == Key.Down)
            {
                dataContext.KeyDown();
                return;
            }

            if (e.Key == Key.Right)
            {
                dataContext.KeyRight();
                return;
            }

            if (e.Key == Key.F5)
            {
                dataContext.Restart();
                return;
            }

            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
