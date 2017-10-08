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

        private IKeyboardInput _keyboardInput;
        private Mathematics _maths = new Mathematics();
        private int _frames;
        private RenderPage _renderer;
        private GameData _gameData;
        private ILevel _level;
        private LevelFactory _levelFactory;
        private MunitionsFactory _munitionsFactory;
        private int _levelCounter = 0;
        private Player _player;
        private CollisionDetector _collisionDetection = new CollisionDetector();
        private List<IAGameObject> _weaponEnemy;
        private List<IAGameObject> _weaponPlayer;
        private GameManager _gameManager;
        private GameEngine _gameEngine;

        private static System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();

        public Game(Page renderPage, int frames, GameData gameData)
        {
            Initialise(renderPage, frames, gameData);
        }

        private void Initialise(Page renderPage, int frames, GameData gameData)
        {
            _gameData = gameData;
            _gameData.WriteToConsole.Invoke(new[] { "\rInitialising...\r"});
            _keyboardInput = new KeyboardInput(new PlayKeyManager());
            _keyboardInput.KBPreviewEventInitialise();
            _frames = frames;
            _renderer = renderPage as RenderPage;
            _gameEngine = new GameEngine(_renderer);
            IsLevelRunning = false;
            IsNewGame = true;
            UserReady = false;
            IsNewGame = false;
            SetupGameData();
            _weaponEnemy = new List<IAGameObject>();
            GameObjectLoader gol = new GameObjectLoader("Assets//Scripts//Gamedat.xml");
            _levelFactory = new LevelFactory(gol.GetLevelParser(), gol.GetAiParser(), _gameData);
            _munitionsFactory = new MunitionsFactory(_gameData);
            _weaponPlayer = new List<IAGameObject>();
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
            _gameData.WriteToConsole.Invoke(new[] { string.Format("Loading Level {0}\r", _levelCounter) });
            _level = _levelFactory.MakeLevel(_levelCounter++);
            _level.Load();
            _gameEngine.AddWorldObjects(_level.GetLevelObjects());
            _gameEngine.AddWorldObject(_player.GetPlayerObject());
            _gameEngine.AddWorldObjects(_weaponPlayer);
            _gameEngine.AddWorldObjects(_weaponEnemy);
            _gameManager = new GameManager(_weaponPlayer, _weaponEnemy, _gameData, _gameEngine, _munitionsFactory, _renderer, _level, _player);
        }

        public void Update()
        {
            _gameManager.Update();
            _player.Update();
            _gameEngine.Update();
            _gameEngine.Render();
        }

        public void DeInitialise()
        {
            _gameData = null;
            _gameManager.DeInitialise();
            _gameEngine.DeInitialise();
            _level.DeInitialise();
            (_keyboardInput as KeyboardInput).Dispose();
            _keyboardInput = null;
        }
    }
}
