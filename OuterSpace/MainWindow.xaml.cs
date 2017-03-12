using OuterSpace.Game;
using OuterSpace.RenderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OuterSpace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameEngine _gameEngine;
         public MainWindow()
        {
            this.Width = SystemParameters.FullPrimaryScreenWidth;
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            InitializeComponent();
        }

        private Page InitialisePage(Page page)
        {
            page.DataContext = this;
            return page;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Page renderPage = new RenderPage(Width, Height);
            Frame frame = new Frame();
            frame.Width = Width;
            frame.Height = Height;
            frame.Margin = new Thickness(0, 0, 0, 0);
            frame.HorizontalAlignment = HorizontalAlignment.Left;
            frame.VerticalAlignment = VerticalAlignment.Top;
            frame.Navigate(InitialisePage(renderPage));
            GameGrid.Children.Add(frame);
            _gameEngine = new Game.GameEngine(renderPage);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MyCallbackMethod(object sender, ElapsedEventArgs eea)
        {
            
        }

        private void FrameSetup()
        {
            InitialisePage(MenuFrame.Content as Page);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
