using OuterSpace.Game.Loaders;
using OuterSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLParser;

namespace OuterSpace.Game.Loaders
{
    public class GameObjectLoader
    {
        private EnemyShipParser _enemyShipParser;
        private LevelParser _levelParser;
        private string _filename;

        public GameObjectLoader(string filename)
        {
            _filename = filename;
        }

        //public List<AiModel> LoadAi()
        public void LoadAi()
        {
            _enemyShipParser = new EnemyShipParser(_filename);
            //return _enemyShipParser.GetEnemiesList();
        }

        public void LoadLevel()
        {
            _levelParser = new LevelParser(_filename);
        }

        public EnemyShipParser GetAiParser()
        {
            LoadAi();
            return _enemyShipParser;
        }

        public LevelParser GetLevelParser()
        {
            LoadLevel();
            return _levelParser;
        }
    }
}
