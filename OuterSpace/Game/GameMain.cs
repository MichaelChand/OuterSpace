using OuterSpace.RenderSystem;
using OuterSpace.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OuterSpace.Game
{
    public class GameMain : IDisposable
    {
        private MainWindow _mainWindow;
        private GameEngine _gameEngine;
        private GameDispatcherTimer _gameTimer;
        private Game _game;
        private Page _renderPage;
        private Func<int> TestCounter;
        private bool tc = false;
        public static int _FRAMES = 30;
        private GameState _gameState;

        public  GameMain(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        private void  AddToGrid<TGrid>(TGrid grid, UIElement element) where TGrid : Grid
        {
            grid.Children.Add(element);
        }

        public void StartGame()
        {
            //StopGame();
            //_gameEngine.GameStart();
        }

        public void StopGame()
        {
            //_gameEngine.GameStop();
        }

        public void Update(object sender, EventArgs elapsedEventArgs)
        {
            switch (_gameState)
            {
                case GameState.Running:
                    RunState();
                    break;
                case GameState.Paused:
                    _gameState = GameState.InMenu;
                    break;
                case GameState.Stopped:
                    _gameState = GameState.InMenu;
                    break;
            }

            //if (_game.IsLevelRunning)
            //    _game.Update();
            //else if (_game.IsNewGame)
            //    _game.StartGame();
        }

        public void RunState()
        {
            _game.Update();
        }

        public void MenuControl()
        {

        }

        public void Run()
        {
            _gameTimer = new GameDispatcherTimer(_FRAMES, Update);
            _game = new Game(_renderPage, _FRAMES);
            _gameState = GameState.Running;
            _game.Run();
            _gameTimer.Start();
        }

        private Page InitialisePage(Page page)
        {
            page.DataContext = this;
            return page;
        }

        public void Initialise<TGrid>(TGrid grid)
        {
            _gameState = GameState.InMenu;
            _renderPage = SetupRenderPage();
            Frame frame = SetupRenderFrame();
            AddPageToFrame(_renderPage, frame);
            AddToGrid(grid as Grid, frame);
            //_gameEngine = new GameEngine(renderPage);
        }

        private Page SetupRenderPage()
        {
            Page RenderPage = new RenderPage(_mainWindow.ActualWidth, _mainWindow.ActualHeight);
            RenderPage.Margin = new Thickness(0, 0, 0, 0);
            RenderPage.VerticalAlignment = VerticalAlignment.Top;
            RenderPage.HorizontalAlignment = HorizontalAlignment.Left;
            return RenderPage;
        }

        private Frame SetupRenderFrame()
        {
            Frame frame = new Frame();
            frame.Margin = new Thickness(0, 0, 0, 0);
            frame.VerticalAlignment = VerticalAlignment.Top;
            frame.HorizontalAlignment = HorizontalAlignment.Left;
            frame.Background = new SolidColorBrush(Colors.Blue);
            return frame;
        }

        private void AddPageToFrame(Page page, Frame frame)
        {
            frame.Navigate(InitialisePage(page));
        }

        private void CleanUp()
        {
            //_gameEngine?.Dispose();
            _gameEngine = null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                CleanUp();
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~GameMain()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(false);
        //}

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
