// The game itself. Deals with How the game is structured. Relying on Renderer to render provided components.
//Components are processed here as per the game logic in this class and member instances.
using CommonRelay.Common;
using CommonRelay.DataObjects;
using GameObjects.Interfaces;
using OuterSpace.Game.Input;
using OuterSpace.Game.Levels;
using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Armory;
using OuterSpace.GameObjects.Ships.Player;
using RenderSystem;
using PhysicsSystem;
using ReConInvaders.Inputsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using OuterSpace.Game.Loaders;

namespace OuterSpace.Game
{
    public class Game
    {
        public bool IsNewGame { get; private set; }
        public bool IsLevelRunning { get; private set; }
        public bool UserReady { get; set; }
        public bool ThisGameEnded { get; set; }

        private IKeyboardInput _keyboardInput;
        private Mathematics _maths = new Mathematics();
        private int _frames;
        private RenderPage _renderer;
        private GameData _gameData;
        private ILevel _level;
        private LevelFactory _levelFactory;
        private MunitionsFactory _munitionsFactory;
        private Player _player;
        private CollisionDetector _collisionDetection = new CollisionDetector();
        private List<IAGameObject> _weaponEnemy;
        private List<IAGameObject> _weaponPlayer;
        private GameManager _gameManager;
        private LevelManager _levelManager;
        private GameEngine _gameEngine;
        private GameObjectLoader _gameObjectLoader;

        private static System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();

        public Game(Page renderPage, int frames, GameData gameData)
        {
            Initialise(renderPage, frames, gameData);
        }

        private void Initialise(Page renderPage, int frames, GameData gameData)
        {
            ThisGameEnded = false;
            _gameData = gameData;
            _frames = frames;
            _renderer = renderPage as RenderPage;
            _gameObjectLoader = new GameObjectLoader("Assets//Scripts//Gamedat.xml");
            _levelFactory = new LevelFactory(_gameObjectLoader.GetLevelParser(), _gameObjectLoader.GetAiParser(), _gameData);
            _keyboardInput = new KeyboardInput(new PlayKeyManager());
            _keyboardInput.KBPreviewEventInitialise();
            SetupGameData();
            _munitionsFactory = new MunitionsFactory(_gameData);
            _weaponEnemy = new List<IAGameObject>();
            _weaponPlayer = new List<IAGameObject>();
            NextLevelInit();
            _player = new Player(_gameData, _keyboardInput, _weaponPlayer);
        }

        private void SetupGameData()
        {
            BoundingBox ViewPortBounding = new BoundingBox(new Point(0, 0), _renderer.cnvViewPort.Width, _maths.RemoveByPercentage(_renderer.cnvViewPort.Height, 30));
            _gameData.ViewPortWidth = (int)_renderer.cnvViewPort.Width;
            _gameData.ViewPortHeight = (int)_renderer.cnvViewPort.Height;
            _gameData.ViewportBounding = ViewPortBounding;
            _gameData.FramesPerSecond = _frames;
        }

        public void Run()
        {
            _level = _levelFactory.MakeLevel(_gameData.StartLID);
            _gameManager = new GameManager(_weaponPlayer, _weaponEnemy, _gameData, _gameEngine, _munitionsFactory, _renderer, _level, _player);
            _levelManager = new LevelManager(_level, _gameData, _gameManager);
            _levelManager.PlayLevel();
            _gameEngine.AddWorldObjects(_level.GetLevelObjects());
            _gameEngine.AddWorldObject(_player.GetPlayerObject());
            _gameEngine.AddWorldObjects(_weaponPlayer);
            _gameEngine.AddWorldObjects(_weaponEnemy);
        }

        private void NextLevelInit()
        {
            _gameData.WriteToConsole.Invoke(new[] { "\rInitialising...\r" });
            _gameEngine = new GameEngine(_renderer);
            IsLevelRunning = false;
            IsNewGame = true;
            UserReady = false;
            IsNewGame = false;
            _weaponEnemy.Clear();
            _weaponPlayer.Clear();
        }

        private void StartNextLevel()
        {
            int levelCount = _gameObjectLoader.GetLevelParser().GetLevelsList().Count;
            _levelManager.Next();
            if (_gameData.StartLID < levelCount && (_player.GetPlayerObject() as GameObjects.Ships.Ship).Alive)
            {
                NextLevelPreprocess();
                NextLevelInit();
                Run();
            }
            else
                ThisGameEnded = true;
        }

        private void NextLevelPreprocess()
        {
            _gameManager.DeInitialise();
            _gameEngine.DeInitialise();
            _levelManager.DeInitialise();
        }

        public bool GameRunning()
        {
            return _levelManager.LevelRunning;
        }

        public void Update()
        {
            if (_levelManager.LevelRunning)
            {
                _levelManager.Update();
                _player.Update();
                _gameEngine.Update();
                _gameEngine.Render();
            }
            else
            {
                StartNextLevel();
            }
        }

        #region Cleanup
        public void DeInitialise()
        {
            _gameData = null;
            _gameManager.DeInitialise();
            _gameEngine.DeInitialise();
            _levelManager.DeInitialise();
            (_keyboardInput as KeyboardInput).Dispose();
            _keyboardInput = null;
        }

        #endregion
    }
}
