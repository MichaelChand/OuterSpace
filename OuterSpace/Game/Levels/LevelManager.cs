using CommonRelay.DataObjects;
using CommonRelay.Extensions;
using GameObjects.Interfaces;
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

/* What should this do?
 * Pass a level to it. 
 * The level contains information the manager uses to manage the level.
 * The manager sets status that can be checked on each update.
 *      Status such as: is level still running? Is player dead. Is enemy cleared. Powerup timer/number of power ups obtained this level. Score obtained this level.
 * 
 * TODOs':     
 * How do we determine when to transistion level?
 */

namespace OuterSpace.Game.Levels
{
    public class LevelManager
    {
        private ILevel _level;
        private GameData _gameData;
        private LevelState _levelState;
        private Player _player;
        private GameEngine _gameEngine;
        private RenderPage _renderer;
        private AiManager _aiManager;
        private CollisionDetector _collisionDetector;
        private List<IAGameObject> _playerWeaponList;
        private List<IAGameObject> _enemyWeaponList;

        public bool LevelRunning { get; private set; }

        public LevelManager(ILevel level, GameData gamedata, Player player, GameEngine gameEngine, RenderPage renderer, AiManager aimanager, CollisionDetector collisionDetector, List<IAGameObject> playerWeaponList, List<IAGameObject> enemyWeaponList)
        {
            _level = level;
            _gameData = level.GetGameData<GameData>();
            _levelState = LevelState.Active;
            _player = player;
            _gameEngine = gameEngine;
            _renderer = renderer;
            _aiManager = aimanager;
            _collisionDetector = collisionDetector;
            _playerWeaponList = playerWeaponList;
            _enemyWeaponList = enemyWeaponList;
        }

        public ILevel GetLevelObject()
        {
            return _level;
        }

        public void PlayLevel()
        {
            LevelRunning = true;
            _level.Load();
        }

        private void LevelEnded()
        {
            LevelRunning = false;
        }

        public void PauseLevel()
        {

        }

        public void ResumeLevel()
        {

        }

        public void AbortLevel()
        {

        }

        public int Next()
        {
            _gameData.StartLID = _gameData.StartLID + 1;
            return _gameData.StartLID;
        }

        public void Update()
        {
            ConfirmLevelState();
            switch(_levelState)
            {
                case LevelState.Active :
                    //_gameManager.Update();
                    EnemyHitTest(_level.GetLevelObjects(), _playerWeaponList);
                    PlayerHitTest(_level.GetLevelObjects(), _enemyWeaponList);
                    WeaponPersistanceCheck(_playerWeaponList);
                    WeaponPersistanceCheck(_enemyWeaponList);
                    CheckForNewWeaponToAdd(_playerWeaponList);
                    CheckForNewWeaponToAdd(_enemyWeaponList);
                    UpdateAi();
                    _player.Update();
                    _gameEngine.Update();
                    _gameEngine.Render();
                    break;
                case LevelState.LevelCleared :
                    LevelRunning = false;
                    break;
                case LevelState.DeadPlayer:
                    LevelRunning = false;
                    break;
                default:
                    LevelRunning = false;
                    break;
            }
        }

        public LevelState GetLevelState()
        {
            return _levelState;
        }

        private void ConfirmLevelState()
        {
            //PlayerState
            if (!(_player.GetPlayerObject() as Ship).Alive)
                _levelState = LevelState.DeadPlayer;
            //EnemyState
            if (_level.GetLevelObjects().Count <= 0 && _levelState == LevelState.Active)
                _levelState = LevelState.LevelCleared; //We should then run a level end timer to allow player to mop up any powerups.
        }


        private void WeaponPersistanceCheck(List<IAGameObject> weaponList)
        {
            int count = weaponList.Count - 1;
            List<IAGameObject> inactiveObjects = (from IAGameObject gObj in weaponList
                                                  where (!(gObj as Armory).IsActive)
                                                  select gObj).ToList();
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
            for (int i = enemyCount - 1; i >= 0; i--)
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
            if (collidedWeapon.Length > 0)
            {
                for (int i = 0; i < collidedWeapon.Length; i++)
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
            for (int i = aiShips.Count - 1; i >= 0; i--)
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
            _level.DeInitialise();
            _gameData = null;
        }
    }
}
