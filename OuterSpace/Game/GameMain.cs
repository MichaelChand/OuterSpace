using OuterSpace.RenderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OuterSpace.Game
{
    public class GameMain : IDisposable
    {
        private MainWindow _mainWindow;
        private GameEngine _gameEngine;

        public  GameMain(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void  AddToGrid<TGrid>(TGrid grid, UIElement element) where TGrid : Grid
        {
            grid.Children.Add(element);
        }

        public void StartGame()
        {
            _gameEngine.GameStart();
        }

        public void StopGame()
        {
            _gameEngine.GameStop();
        }

        public Page InitialisePage(Page page)
        {
            page.DataContext = this;
            return page;
        }

        public void Initialise<TGrid>(TGrid grid)
        {
            Page renderPage = SetupRenderPage();
            Frame frame = SetupRenderFrame();
            AddPageToFrame(renderPage, frame);
            AddToGrid(grid as Grid, frame);
            _gameEngine = new GameEngine(renderPage);
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
            _gameEngine?.Dispose();
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
