// The game itself. Deals with How the game is structured. Relying on Renderer to render provided components.
//Components are processed here as per the game logic in this class and member instances.
using Common.Common;
using OuterSpace.Game.Input;
using OuterSpace.Game.Levels;
using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Armory;
using OuterSpace.GameObjects.Armory.Weapons;
using OuterSpace.GameObjects.Ships;
using OuterSpace.GameObjects.Ships.Player;
using OuterSpace.Physics;
using OuterSpace.RenderSystem;
using ReConInvaders.Inputsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
        private int _levelCounter;
        private Player _player;
        private int _intersetCount = 0;
        private CollisionDetector _collisionDetection = new CollisionDetector();
        private List<IAGameObject> _weaponEnemy;
        private List<IAGameObject> _weaponPlayer;

        private GameEngine _gameEngine;

        public Game(Page renderPage, int frames)
        {
            Initialise(renderPage, frames);
        }

        private void Initialise(Page renderPage, int frames)
        {
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
            _levelFactory = new LevelFactory(_gameData);
            _munitionsFactory = new MunitionsFactory(_gameData);
            _weaponPlayer = new List<IAGameObject>();
            _player = new Player(_gameData, _keyboardInput, _weaponPlayer);
        }

        private void SetupGameData()
        {
            BoundingBox ViewPortBounding = new BoundingBox(new Point(0, 0), _renderer.cnvViewPort.Width, _maths.RemoveByPercentage(_renderer.cnvViewPort.Height, 30));
            _gameData = new GameData();
            _gameData.ViewPortWidth = (int)_renderer.cnvViewPort.Width;
            _gameData.ViewPortHeight = (int)_renderer.cnvViewPort.Height;
            _gameData.ViewportBounding = ViewPortBounding;
            _gameData.FramesPerSecond = _frames;
        }

        public void Run()
        {
            _level = _levelFactory.MakeLevel(_levelCounter++);
            _level.Load();
            _gameEngine.AddWorldObjects(_level.GetLevelObjects());
            _gameEngine.AddWorldObject(_player.GetPlayerObject());
            _gameEngine.AddWorldObjects(_weaponPlayer);
        }

        private void CollisionTest()
        {
            //BoundingBox playerBounds = (_player.GetPlayerObject() as Ship).GetBoundingBox();
            //BoundingBox enemyBounds = (_level.GetLevelObjects()[0] as Ship).GetBoundingBox();
            //if (_collisionDetection.Collision(playerBounds, enemyBounds))
            //    Console.WriteLine(string.Format("{0} COLLISION", _intersetCount++));
        }

        private void WeaponPersistanceCheck(List<IAGameObject> weaponList)
        {
            for (int i = weaponList.Count - 1; i >= 0; i--)
            {
                if (!(weaponList[i] as Armory).IsActive)
                    _renderer.RemoveWorldObject(weaponList[i]);
            }

            List<IAGameObject> inactiveObjects = (from IAGameObject go in weaponList
                                                  where (!(go as Armory).IsActive)
                                                  select go).ToList();
            for (int i = inactiveObjects.Count - 1; i >= 0; i--)
                weaponList.Remove(inactiveObjects[i]);
        }

        private void CheckForNewPlayerWeaponToAdd()
        {
            IAGameObject weapon = _player.GetNewWeapon();
            if (weapon != null)
                _gameEngine.DynamicAdd(weapon);
        }

        public void Update()
        {
            _player.Update();
            _gameEngine.Update();
            _gameEngine.Render();
            WeaponPersistanceCheck(_weaponPlayer);
            CheckForNewPlayerWeaponToAdd();
            CollisionTest();
            //Update Game engine.
            //If level ended, process end level stuff.
            //Once user ready, load next level.
        }
    }
}
