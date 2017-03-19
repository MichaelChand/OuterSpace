using OuterSpace.Common;
using OuterSpace.Game.Levels;
using OuterSpace.GameObjects.Ships.Enemy;
using OuterSpace.Physics;
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

namespace OuterSpace.Game
{
    public class GameEngine
    {
        private RenderPage _renderer;
        private GameTimer _gameTimer;
        private readonly int _FRAMES = 30;
        private GameData _gameData;
        private List<ILevel> _levels = new List<ILevel>();
        private Mathematics _maths = new Mathematics();

        public GameEngine (Page renderPage)
        {
            _renderer = renderPage as RenderPage;
            Initialise();
        }

        private void Initialise()
        {
            SetupGameData();
            CreateLevel();
            LoadLevels();
            _renderer.SetupWorldObjects(_levels[0].GetLevelObjects().ToArray());
        }

        private void SetupGameData()
        {
            BoundingBox ViewPortBounding = new BoundingBox(new Point(0, 0), _renderer.Width, _maths.RemoveByPercentage(_renderer.Height, 30));
            _gameData = new GameData();
            _gameData.ViewPortWidth = (int)_renderer.RenderGrid.Width;
            _gameData.ViewPortHeight = (int)_renderer.RenderGrid.Height;
            _gameData.ViewportBounding = ViewPortBounding;
        }

        private void CreateLevel()
        {
             _levels.Add(new WaveOne(_gameData));
        }

        private void LoadLevels()
        {
            for (int i = 0; i < _levels.Count; i++)
                _levels[i].Load();
        }

        public void GameStart()
        {
            _gameTimer = new GameTimer(_FRAMES, ProcessFrameCallback);
            _gameTimer.Start();
        }

        public void GameStop()
        {
            _gameTimer?.Stop();
            Console.WriteLine("GAME STOPPED.");
        }

        public void GameResume()
        {
            //Once timer has stopped, resume needs to be done by recreating the timer.
            GameStart(); 
        }

        private void Update()
        {
            //for(int i = 0; i < _levels.Count; i++)
            //    _levels[i].Load();
        }

        private void ProcessFrameCallback(object sender, ElapsedEventArgs eea)
        {
            Update();
            _renderer.Render();
        }
    }
}
