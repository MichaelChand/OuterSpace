using CommonRelay.DataObjects;
using GameObjects.Interfaces;
using OuterSpace.Game.Loaders;
using OuterSpace.GameEntities.Ships.Enemy;
using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Ships.Enemy;
using OuterSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OuterSpace.Game.Levels
{
    public class Level : ILevel
    {
        private List<IAGameObject> _enemies;
        private GameData _gameData;
        private AiFactory _aiFactory;
        private LevelModel _levelModel;
        private EnemyShipParser _enemyShipParser;

        public Level(LevelModel levelModel, EnemyShipParser enemyShipParser, GameData gameData)
        {
            _levelModel = levelModel;
            _enemyShipParser = enemyShipParser;
            _gameData = gameData;
        }

        public void Load()
        {
            //Load Level data here.
            _gameData.WriteToConsole(new[] { "Loading Level Data...\r" });
            _aiFactory = new AiFactory(_enemyShipParser, _gameData);
            _gameData.WriteToConsole(new[] { "Creating Level...\r" });
            CreateLevel();
        }

        private void CreateLevel()
        {
            _enemies = CreateEnemies();
        }

        private List<IAGameObject> CreateEnemies()
        {
            _gameData.WriteToConsole(new[] { "Loading AI...\r" });
            List<IAGameObject> enemies = new List<IAGameObject>();
            foreach(int enemyType in _levelModel.AiTypes)
                enemies.Add(_aiFactory.GetAi(enemyType));
            return enemies;
        }

        public List<IAGameObject> GetLevelObjects()
        {
            return _enemies;
        }

        public void Update()
        {
            for (int i = _enemies.Count-1; i >= 0; i--)
                _enemies[i].Update();
        }

        public TGameData GetGameData<TGameData>()
        {
            return (TGameData)Convert.ChangeType(_gameData, typeof(TGameData));
        }

        public void DeInitialise()
        {
            _enemies.Clear();
            _enemies = null;
        }
    }
}
