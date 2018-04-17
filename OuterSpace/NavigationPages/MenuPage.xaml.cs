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
            ButtonGameStopStartManager((DataContext as MainWindow).btnStart_Click, sender, e);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            ButtonGameStopStartManager((DataContext as MainWindow).btnExit_Click, sender, e);
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow).btnPause_Click(sender, e);
        }

        private void ButtonGameStopStartManager(Action<object, RoutedEventArgs> f, object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow).btnStop_Click(sender, e);
            f.Invoke(sender, e);
        }

        private void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            //MenuPageFrame.DataContext = DataContext;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            //This is basic text. Proper instruction screen to be built once other components are done and levels created.
            string Title = "HELP:";
            string Movement = "Use LEFT and RIGHT ARROW keys on your keyboard to move the player ship left or right.";
            string Fire = "Press the SPACE BAR to fire. Hold down the space bar for continuous fire.";
            string MenuControl = "Press ESC to get to GAME MENU. Press \"P\" to pause the game ";
            string helpContent = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", Title, Movement, Fire, MenuControl);
            MessageBox.Show(helpContent);

            //ClassBase cb = new ClassBase();
            //ClassDerived cd = new ClassDerived();
            //ClassOverrideDerived cod = new ClassOverrideDerived();

            //cb = cd;

            //cb.Display();
            //cd.Display();
            //cod.Display();
        }

        //private class ClassBase
        //{
        //    public virtual void Display()
        //    {
        //        Console.WriteLine("Base Class");
        //    }
        //}

        //private class ClassDerived : ClassBase
        //{
        //    public void Display()
        //    {
        //        Console.WriteLine("Derived Class");
        //    }
        //}

        //private class ClassOverrideDerived : ClassBase
        //{
        //    public override void Display()
        //    {
        //        Console.WriteLine("Override Derived Class");
        //    }
        //}
    }
}
