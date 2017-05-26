using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Game.Levels
{
    public class LevelManager
    {
        private ILevel _level;

        public bool LevelRunning { get; private set; }

        public LevelManager(ILevel level)
        {
            _level = level;
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

        public void Update()
        {
            _level.Update();
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
    }
}
