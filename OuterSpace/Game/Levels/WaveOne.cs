using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Ships.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Game.Levels
{
    public class WaveOne : ILevel
    {
        private List<IAGameObject> _enemies;
        private GameData _gameData;
        private int _framesPerSecond;

        public WaveOne(GameData gameData)
        {
            _gameData = gameData;
        }

        public void Load()
        {
            //Load Level data here.
            CreateLevel();
        }

        private void CreateLevel()
        {
            _enemies = CreateEnemies();
        }

        public List<IAGameObject> CreateEnemies()
        {
            List<IAGameObject> enemies = new List<IAGameObject>();
            enemies.Add(new EnemyTwo(_gameData, _framesPerSecond));
            return enemies;
        }

        public List<IAGameObject> GetLevelObjects()
        {
            return _enemies;
        }

        public void Update()
        {
            for (int i = 0; i < _enemies.Count; i++)
                _enemies[i].Update();
        }
    }
}
