using OuterSpace.Game;
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

namespace OuterSpace.NavigationPages
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            //New Game, pause, resume button. Type should be checked via button label.
            (DataContext as MainWindow).btnStop_Click(sender, e);
            (DataContext as MainWindow).btnStart_Click(sender, e);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow).btnStop_Click(sender, e);
            (DataContext as MainWindow).btnExit_Click(sender, e);
        }

        private void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            //MenuPageFrame.DataContext = DataContext;
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow).btnPause_Click(sender, e);
        }
    }
}
