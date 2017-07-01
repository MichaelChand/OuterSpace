using CommonRelay.Common;
using CommonRelay.DataObjects;
using CommonRelay.Extensions;
using GameObjects.Interfaces;
using OuterSpace.Game.Levels;
using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Armory;
using OuterSpace.RenderSystem;
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

        public GameManager(List<IAGameObject> playerWeaponList, List<IAGameObject> enemyWeaponList, GameData gameData, GameEngine gameEngine, MunitionsFactory munitionsFactory, RenderPage renderer, ILevel level)
        {
            _level = level;
            _playerWeaponList = playerWeaponList;
            _enemyWeaponList = enemyWeaponList;
            _gameData = gameData;
            _gameEngine = gameEngine;
            _renderer = renderer;
            _munitionsFactory = munitionsFactory;
            _aiManager = new AiManager(_gameData, enemyWeaponList, _munitionsFactory);
        }


        private void WeaponPersistanceCheck(List<IAGameObject> weaponList)
        {
            int count = weaponList.Count - 1;
            List<IAGameObject> inactiveObjects = (from IAGameObject go in weaponList
                                                  where (!(go as Armory).IsActive)
                                                  select go).ToList();
            for (int i = inactiveObjects.Count - 1; i >= 0; i--)
            {
                _renderer.RemoveWorldObject(inactiveObjects[i]);
                weaponList.Remove(inactiveObjects[i]);
            }

            weaponList.CapacityTrim();
        }

        private void CheckForNewWeaponToAdd(List<IAGameObject> weaponList)
        {
            Armory[] armory = (from Armory wp in weaponList
                               where (!(wp as Armory).Fired && (wp as Armory).IsActive)
                               select wp).ToArray();
            for (int i = armory.Length - 1; i >= 0; i--)
            {
                armory[i].Fired = true;
                _gameEngine.DynamicAdd(armory[i]);
            }
        }

        private void UpdateAi()
        {
            int enemyCount = _level.GetLevelObjects().Count;
            List<IAGameObject> enemies = _level.GetLevelObjects();
            for (int i = enemyCount -1; i >= 0; i--)
            {
                _aiManager.SetAi(enemies[i]);
                _aiManager.Update();
            }
        }

        public void Update()
        {
            UpdateAi();
            CheckForNewWeaponToAdd(_playerWeaponList);
            CheckForNewWeaponToAdd(_enemyWeaponList);
            WeaponPersistanceCheck(_playerWeaponList);
            WeaponPersistanceCheck(_enemyWeaponList);
        }
    }
}
