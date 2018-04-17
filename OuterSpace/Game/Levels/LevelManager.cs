using CommonRelay.DataObjects;
using OuterSpace.GameObjects.Ships;
using OuterSpace.GameObjects.Ships.Player;
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

        public bool LevelRunning { get; private set; }

        public LevelManager(ILevel level, GameData gamedata, Player player)
        {
            _level = level;
            _gameData = level.GetGameData<GameData>();
            _levelState = LevelState.Active;
            _player = player;
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

        public void DeInitialise()
        {
            _level.DeInitialise();
            _gameData = null;
        }
    }
}
