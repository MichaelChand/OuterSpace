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

        public GameManager(List<IAGameObject> playerWeaponList, List<IAGameObject> enemyWeaponList, GameData gameData, GameEngine gameEngine, MunitionsFactory munitionsFactory, RenderPage renderer, ILevel level, Player player)
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

        private bool HitTest(GameObject ship, List<IAGameObject> weaponList)
        {
            if (!(ship as Ship).Alive)
                return false;
            IAGameObject[] collidedWeapon = (from weapon in weaponList
                                             where _collisionDetector.Collision(ship.GetBoundingBox(), (weapon as GameObject).GetBoundingBox())
                                             select weapon).ToArray();
            if(collidedWeapon.Length > 0)
            {
                for(int i = 0; i < collidedWeapon.Length; i++)
                {
                    (collidedWeapon[i] as Armory).IsActive = false;
                    (ship as Ship).Strength -= (collidedWeapon[i] as Armory).Strength;
                }
                return true;
            }
            return false;
        }

        private void EnemyHitTest(List<IAGameObject> ships, List<IAGameObject> weaponList)
        {
            List<IAGameObject> aiShips = ships;
            for(int i = aiShips.Count -1; i >= 0; i--)
            {
                HitTest(aiShips[i] as GameObject, weaponList);
                (aiShips[i] as EnemyShip).HitPointBar.SetHitpoint((aiShips[i] as Ship).Strength);
                if ((aiShips[i] as EnemyShip).HitPointBar.Hitpoint <= 0)
                {
                    _renderer.RemoveWorldObject(aiShips[i]);
                    aiShips.Remove(aiShips[i]);
                }
            }
        }

        private void PlayerHitTest(List<IAGameObject> ships, List<IAGameObject> weaponList)
        {
            HitTest((_player.GetPlayerObject() as PlayerShip), weaponList);
            if ((_player.GetPlayerObject() as Ship).Strength <= 0)
            {
                _renderer.RemoveWorldObject(_player.GetPlayerObject());
                (_player.GetPlayerObject() as Ship).Alive = false;
            }
        }

        public void Update()
        {
            EnemyHitTest(_level.GetLevelObjects(), _playerWeaponList);
            PlayerHitTest(_level.GetLevelObjects(), _enemyWeaponList);
            WeaponPersistanceCheck(_playerWeaponList);
            WeaponPersistanceCheck(_enemyWeaponList);
            CheckForNewWeaponToAdd(_playerWeaponList);
            CheckForNewWeaponToAdd(_enemyWeaponList);
            UpdateAi();
        }

        private void RemoveGameObjectsInList(List<IAGameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
                _renderer.RemoveWorldObject(objects[i]);
        }

        public void DeInitialise()
        {
            _collisionDetector = null;
            _aiManager.DeInitialise();
            RemoveGameObjectsInList(new List<IAGameObject> { _player.GetPlayerObject() });
            RemoveGameObjectsInList(_playerWeaponList);
            RemoveGameObjectsInList(_enemyWeaponList);
            RemoveGameObjectsInList(_level.GetLevelObjects());
        }
    }
}
