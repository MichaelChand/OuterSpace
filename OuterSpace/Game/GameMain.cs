using RenderSystem;
using Chronometers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommonRelay.DataObjects;

//make progress bar. add collision detection. export Ai and level making to xml. Make menu system

namespace OuterSpace.Game
{
    public class GameMain : IDisposable
    {
        private MainWindow _mainWindow;
        private GameDispatcherTimer _gameTimer;
        private Game _game;
        private Page _renderPage;
        public static int _FRAMES = 30;
        private GameState _gameState;
        private GameData _gameData;

        public  GameMain(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _gameData = new GameData();
            _gameData.WriteToConsole = _mainWindow.ConsoleWrite;
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
            _gameState = GameState.Stopped;
            Update(null, null);
            DeInitialise();
        }

        public void PauseGame()
        {
            _gameState = _gameState == GameState.Running ? GameState.Paused : GameState.Running;
        }

        public void Update(object sender, EventArgs eventArgs)
        {
            switch (_gameState)
            {
                case GameState.Running:
                    if (!_renderPage.IsVisible)
                        (_renderPage as RenderPage).SetVisible();
                    RunState();
                    break;
                case GameState.Paused:
                    PauseState();
                    break;
                case GameState.Stopped:
                    _gameState = GameState.InMenu;
                    MenuState();
                    break;
                default:
                    MenuState();
                    break;
            }

            //if (_game.IsLevelRunning)
            //    _game.Update();
            //else if (_game.IsNewGame)
            //    _game.StartGame();
        }

        private void PauseState()
        {
            // Call to Pause Menu;
        }

        private void MenuState()
        {
            if (_renderPage.IsVisible)
                (_renderPage as RenderPage).SetHidden();
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
            _game = new Game(_renderPage, _FRAMES, _gameData);
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
            //_gameEngine = null;
            _gameTimer = null;
            _gameData = null;
            _game = null;
        }

        public void DeInitialise()
        {
            _gameTimer.Dispose();
            _game.DeInitialise();
            CleanUp();
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
                DeInitialise();
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
