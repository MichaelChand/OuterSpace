using CommonRelay.Common;
using CommonRelay.DataObjects;
using OuterSpace.Game.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Game.Levels
{
    public class LevelFactory
    {
        private GameData _gameData;

        public LevelFactory(GameData gameData)
        {
            _gameData = gameData;
        }

        public ILevel MakeLevel(int level)
        {
            switch (level)
            {
                case 0:
                    return new WaveOne(_gameData);
                default :
                    return null;
            }
        }
    }
}
