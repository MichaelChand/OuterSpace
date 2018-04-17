/*
 Manage the game's current state. Eg state of level, state of player, state of enemies, etc.
 */
using CommonRelay.Common;
using CommonRelay.DataObjects;
using CommonRelay.Extensions;
using GameObjects.Interfaces;
using OuterSpace.Game.Levels;
using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Armory;
using OuterSpace.GameObjects.Ships;
using OuterSpace.GameObjects.Ships.Enemy;
using OuterSpace.GameObjects.Ships.Player;
using PhysicsSystem;
using RenderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Game
{
    public class GameManager
    {
        private List<IAGameObject> _playerWeaponList;
        private List<IAGameObject> _enemyWeaponList;
        private GameData _gameData;
        private GameEngine _gameEngine;
        private RenderPage _renderer;
        private AiManager _aiManager;
        private MunitionsFactory _munitionsFactory;
        private ILevel _level;
        private Player _player;
        private CollisionDetector _collisionDetector;
        private LevelState _levelState;
        private LevelManager _levelManager;

        public bool LevelRunning { get { return _levelManager.LevelRunning; } }

        public GameManager(List<IAGameObject> playerWeaponList, List<IAGameObject> enemyWeaponList, GameData gameData, GameEngine gameEngine, 
            MunitionsFactory munitionsFactory, RenderPage renderer, ILevel level, Player player)
        {

            _level = level;
            _playerWeaponList = playerWeaponList;
            _enemyWeaponList = enemyWeaponList;
            _gameData = gameData;
            _gameEngine = gameEngine;
            _renderer = renderer;
            _munitionsFactory = munitionsFactory;
            _aiManager = new AiManager(_gameData, enemyWeaponList, _munitionsFactory);
            _player = player;
            _collisionDetector = new CollisionDetector();
            _levelState = LevelState.Active;
            _levelManager = new LevelManager(_level, _gameData, _player, _gameEngine, _renderer, _aiManager, _collisionDetector, _playerWeaponList, _enemyWeaponList);
        }

        public void PlayLevel()
        {
            _levelManager.PlayLevel();
        }

        public void NextLevel()
        {
            _levelManager.Next();
        }

        public LevelState GetState()
        {
            return _levelManager.GetLevelState();
        }

        public void Update()
        {
            _levelManager.Update();
        }

        #region Deinitialise Methods

        private void RemoveGameObjectsInList(List<IAGameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
                _renderer.RemoveWorldObject(objects[i]);
        }

        public void DeInitialise()
        {
            RemoveGameObjectsInList(_level.GetLevelObjects());
            _levelManager.DeInitialise();
            _levelManager = null;
        }

        #endregion
    }
}
