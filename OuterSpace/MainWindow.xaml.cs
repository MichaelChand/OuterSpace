using OuterSpace.Game;
using OuterSpace.Game.Input;
using RenderSystem;
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
using ConsoleOutput;
using CommonRelay.DataObjects;

namespace OuterSpace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameMain _gameMain;
        private IKeyboardInput _keyboardInput;
        private OutputConsole _outputConsole;
        internal Action<string[]> ConsoleWrite;

        public MainWindow()
        {
            PreComponentInitialise();
            InitializeComponent();            
        }

        internal void WriteToConsole(string[] message)
        {
            _outputConsole.WriteOutput(message);
        }

        private void PostLoadInitialise()
        {
            _keyboardInput = new KeyboardInput(new MenuKeyManager());
            _keyboardInput.KBPreviewEventInitialise();
            //_keyboardInput.KBEventInitialise();
            _gameMain = new GameMain(this);
            _gameMain.Initialise(GameGrid);
        }

        private void PreComponentInitialise()
        {     
             _outputConsole = new OutputConsole();
            ConsoleWrite = WriteToConsole;
            this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight;
            _outputConsole.Show();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //_gameMain?.Dispose();
            //_gameMain = new GameMain(this);
            //_gameMain.Initialise(GameGrid);
            //_gameMain.StartGame();
            if(_gameMain == null)
            {
                _gameMain = new GameMain(this);
                _gameMain.Initialise(GameGrid);
            }
            _gameMain.Run();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _gameMain.StopGame();
            _gameMain.DeInitialise();
            _gameMain = null;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            _gameMain.PauseGame();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PostLoadInitialise();
            
        }

        /// <summary>
        /// Deactivate certain windows modifier keys such as "ALT"
        /// </summary>
        /// <param name="kea"></param>
        protected override void OnKeyDown(KeyEventArgs kea)
        {
            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.Alt:
                    kea.Handled = true;
                    break;
                default:
                    base.OnKeyDown(kea);
                    break;
            }
        }

        /// <summary>
        /// Manage responses based on keys pressed for main menu and overall game state management
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown_Pressed(object sender, KeyEventArgs e)
        {
            List<Key?> keys = _keyboardInput.GetActiveKeys();
            Action<Key?> ManageAction = (key) =>
            {
                switch (key)
                {
                    
                    case Key.P:
                        btnPause_Click(sender, null);
                        break;
                }
            };

            for (int i = keys.Count - 1; i >= 0; i--)
                ManageAction(keys[i]);
        }
    }
}
