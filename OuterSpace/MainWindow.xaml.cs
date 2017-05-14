using OuterSpace.Game;
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

        private void PostComponentInitialise()
        {
            _keyboardInput = new KeyboardInput();
        }

        private void PreComponentInitialise()
        {
            this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight;
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent, new KeyEventHandler(KeyboardEvent_KeyUp), true);
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyDownEvent, new KeyEventHandler(KeyboardEvent_KeyDown), true);
        }

        public KeyboardInput GetKeyboardObject()
        {
            return _keyboardInput;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _gameMain?.Dispose();
            _gameMain = new GameMain(this);
            _gameMain.Initialise(GameGrid);
            _gameMain.StartGame();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _gameMain.StopGame();
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
            _gameMain.InitialisePage(MenuFrame.Content as Page);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PostComponentInitialise();
        }

        //private void PreviewKeyTrap_KeyDown(object sender, KeyEventArgs keyEventArgs)
        //{
        //    _keyboardInput.SetKeyHandled(true, keyEventArgs);
        //}

        //private void PreviewKeyTrap_KeyUp(object sender, KeyEventArgs keyEventArgs)
        //{
        //    _keyboardInput.SetKeyHandled(true, keyEventArgs);
        //}

        private void KeyboardEvent_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            _keyboardInput.KeyboardEvent_KeyDown(sender, keyEventArgs);
        }

        private void KeyboardEvent_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            _keyboardInput.KeyboardEvent_KeyUp(sender, keyEventArgs);
        }
    }
}
