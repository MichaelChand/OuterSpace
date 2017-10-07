using CommonRelay.Common;
using CommonRelay.DataObjects;
using OuterSpace.Game.Levels;
using OuterSpace.Game.Loaders;
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
        private LevelParser _levelParser;

        public LevelFactory(LevelParser levelParser, GameData gameData)
        {
            _levelParser = levelParser;
            _gameData = gameData;
        }

        public ILevel MakeLevel(int level)
        {
            //switch (level)
            //{
            //    case 0:
            //        return new WaveOne(_gameData);
            //    default :
            //        return null;
            //}
            Level newLevel =  new Level((from levelModel in _levelParser.GetLevelsList()
                              where levelModel.ID == level
                              select levelModel
                             ).FirstOrDefault(), _gameData);
            //newLevel.Load();
            return newLevel;
        }
    }
}
