﻿using OuterSpace.RenderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OuterSpace.Game
{
    public class GameMain
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
            Page renderPage = new RenderPage(_mainWindow.Width, _mainWindow.Height);
            Frame frame = new Frame();
            SetupRenderFrame(frame);
            AddPageToFrame(renderPage, frame);
            AddToGrid(grid as Grid, frame);
            _gameEngine = new GameEngine(renderPage);
        }

        private void SetupRenderFrame(Frame frame)
        {
            frame.Width = _mainWindow.Width;
            frame.Height = _mainWindow.Height;
            frame.Margin = new Thickness(0, 0, 0, 0);
            frame.VerticalAlignment = VerticalAlignment.Top;
            frame.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void AddPageToFrame(Page page, Frame frame)
        {
            frame.Navigate(InitialisePage(page));
        }
    }
}