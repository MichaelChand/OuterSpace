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
        private EnemyShipParser _enemyShipParser;

        public LevelFactory(LevelParser levelParser, EnemyShipParser enemyShipParser, GameData gameData)
        {
            _levelParser = levelParser;
            _enemyShipParser = enemyShipParser;
            _gameData = gameData;
        }

        public ILevel MakeLevel(int level)
        {
            Level newLevel =  new Level((from levelModel in _levelParser.GetLevelsList()
                                         where levelModel.ID == level
                                         select levelModel
                                        ).FirstOrDefault(), 
                                        _enemyShipParser, _gameData
                                       );
            return newLevel;
        }
    }
}
