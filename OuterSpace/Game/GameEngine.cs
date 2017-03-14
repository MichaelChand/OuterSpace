using OuterSpace.GameObjects.Ships.Enemy;
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

        public GameEngine (Page renderPage)
        {
            _renderer = renderPage as RenderPage;
            EnemyShip enemyship = new EnemyShip(_renderer.ActualWidth, _renderer.ActualHeight, null, "Assets//Images//SampleBlank.png");
            (_renderer as RenderPage).SetupWorldObjects(enemyship);
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
            //Once time has stopped, resume needs to be done by recreating the timer.
            GameStart(); 
        }

        private void ProcessFrameCallback(object sender, ElapsedEventArgs eea)
        {
            Console.WriteLine("DEBUG: Callback: ProcessFrameCallback");
        }
    }
}
