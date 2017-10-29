using OuterSpace.Game;
using OuterSpace.Game.Input;
using ReConInvaders.Inputsystem;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using ConsoleOutput;
using System.Windows.Controls;

namespace OuterSpace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal GameMain _gameMain;
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

        internal void GameStateCallback()
        {
            switch(_gameMain._gameState)
            {
                case GameState.InMenu:
                    GameGrid.Visibility = Visibility.Hidden;
                    MenuGrid.Visibility = Visibility.Visible;
                    break;
                case GameState.Paused:
                    GameGrid.Visibility = Visibility.Hidden;
                    MenuGrid.Visibility = Visibility.Visible;
                    break;
                case GameState.Running:
                    GameGrid.Visibility = Visibility.Visible;
                    MenuGrid.Visibility = Visibility.Hidden;
                    break;
                case GameState.Stopped:
                    GameGrid.Visibility = Visibility.Hidden;
                    MenuGrid.Visibility = Visibility.Visible;
                    break;

            }
        }

        private void PostLoadInitialise()
        {
            _keyboardInput = new KeyboardInput(new MenuKeyManager());
            //_keyboardInput.KBPreviewEventInitialise();
            _keyboardInput.KBEventInitialise();
            _gameMain = new GameMain(this);
            _gameMain.Initialise(GameGrid);
            SetupDataContext();
        }

        private void SetupDataContext()
        {
            (MenuFrame.Content as Page).DataContext = this;
        }

        private void PreComponentInitialise()
        {     
             _outputConsole = new OutputConsole();
            ConsoleWrite = WriteToConsole;
            this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight;
            _outputConsole.Show();
        }

        private void BootGameMain()
        {
            _gameMain = new GameMain(this);
            _gameMain.Initialise(GameGrid);
        }

        internal void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (_gameMain == null)
                BootGameMain();

            _gameMain.Run();
        }

        internal void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _gameMain.StopGame();
            _gameMain = null;
        }

        internal void btnPause_Click(object sender, RoutedEventArgs e)
        {
            _gameMain.PauseGame();
        }

        internal void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PostLoadInitialise();
            
        }

        #region Key trap hook for main window

        /// <summary>
        /// Manage responses based on keys pressed for main menu and overall game state management
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="kea"></param>
        private void KeyUp_Pressed(object sender, KeyEventArgs kea)
        {
            List<Key?> keys = _keyboardInput.GetActiveKeys();
            Action<Key?> ManageAction = (key) =>
            {
                switch (key)
                {
                    case Key.P:
                        btnPause_Click(sender, null);
                        break;
                    case Key.Escape:
                        btnPause_Click(sender, null);
                        break;
                }
            };

            for (int i = keys.Count - 1; i >= 0; i--)
                ManageAction(keys[i]);

            _keyboardInput.ClearKeys();
        }

        /// <summary>
        /// Deactivate certain windows modifier keys such as "ALT"
        /// </summary>
        /// <param name="kea"></param>
        //protected override void OnKeyDown(KeyEventArgs kea)
        //{
        //    switch (Keyboard.Modifiers)
        //    {
        //        case ModifierKeys.Alt:
        //            kea.Handled = true;
        //            break;
        //        default:
        //            base.OnKeyDown(kea);
        //            break;
        //    }
        //}
        #endregion
    }
}
