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
using System.Windows.Controls;

namespace OuterSpace.Game
{
    public class GameEngine
    {
        private RenderPage _renderer;
        private GameTimer _gameTimer;
        private readonly int _FRAMES = 30;
        private GameData _gameData;

        public GameEngine (Page renderPage)
        {
            _renderer = renderPage as RenderPage;

            BoundingBox ViewPortBounding = new BoundingBox(_renderer.Margin.Left, _renderer.Margin.Top, _renderer.Width, (_renderer.Height * 2) - (_renderer.Height / 1.20), 0, 0);
            _gameData = new GameData();
            _gameData.ViewPortWidth = (int)_renderer.RenderGrid.Width;
            _gameData.ViewPortHeight = (int)(int)_renderer.RenderGrid.Height;
            _gameData.ViewportBounding = ViewPortBounding;

            WaveOne level1 = new WaveOne(_gameData);
            level1.Load();
            _renderer.SetupWorldObjects(level1.GetEnemies().ToArray());
            _renderer.Render();
            //EnemyShip enemyship = new EnemyShip(_gameData, null, "Assets//Images//SampleBlank.png");
            //enemyship.SetRandomStartPosition();
            //(_renderer as RenderPage).SetupWorldObjects(enemyship);
            //enemyship.Render();
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

        private void ProcessFrameCallback(object sender, ElapsedEventArgs eea)
        {
            Console.WriteLine("DEBUG: Callback: ProcessFrameCallback");
        }
    }
}
