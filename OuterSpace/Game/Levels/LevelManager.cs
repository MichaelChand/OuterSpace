using CommonRelay.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* What should this do?
 * Pass a level to it. 
 * The level contains information the manager uses to manage the level.
 * The manager sets status that can be chacked on each update.
 *      Status such as: is level still running? Is player dead. Is enemy cleared. Powerup timer/number of power ups obtained this level. Score obtained this level.
 *      
 * How do we determine when to transistion level?
 */

namespace OuterSpace.Game.Levels
{
    public class LevelManager
    {
        private ILevel _level;
        private GameData _gameData;
        private GameManager _gameManager;
        private LevelState _levelState;

        public bool LevelRunning { get; private set; }

        public LevelManager(ILevel level, GameData gamedata, GameManager gameManager)
        {
            _level = level;
            _gameManager = gameManager;
            _gameData = level.GetGameData<GameData>();
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
            _levelState = _gameManager.GetState();
            if (_levelState == LevelState.Active)
                _gameManager.Update();
            else
                LevelRunning = false;
        }

        public void DeInitialise()
        {
            _level.DeInitialise();
            _gameManager = null;
            _gameData = null;
        }
    }
}
