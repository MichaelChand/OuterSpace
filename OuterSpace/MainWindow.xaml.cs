using OuterSpace.Game;
using OuterSpace.Game.Input;
using OuterSpace.RenderSystem;
using ReConInvaders.Inputsystem;
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
        private GameMain _gameMain;
        private KeyboardInput _keyboardInput;

        public MainWindow()
        {
            PreComponentInitialise();
            InitializeComponent();            
        }

        private void PostLoadInitialise()
        {
            _keyboardInput = new KeyboardInput( new MenuKeyManager());
            _keyboardInput.KBPreviewEventInitialise();
            _gameMain = new GameMain(this);
            _gameMain.Initialise(GameGrid);
        }

        private void PreComponentInitialise()
        {
            this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //_gameMain?.Dispose();
            //_gameMain = new GameMain(this);
            //_gameMain.Initialise(GameGrid);
            //_gameMain.StartGame();
            _gameMain.Run();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _gameMain.StopGame();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PostLoadInitialise();
        }
    }
}
