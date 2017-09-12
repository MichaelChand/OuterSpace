using CommonRelay.DataObjects;
using GameObjects.Interfaces;
using OuterSpace.Game.Loaders;
using OuterSpace.GameEntities.Ships.Enemy;
using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Ships.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OuterSpace.Game.Levels
{
    public class WaveOne : ILevel
    {
        private List<IAGameObject> _enemies;
        private GameData _gameData;
        private AiFactory _aiFactory;

        public WaveOne(GameData gameData)
        {
            _gameData = gameData;
        }

        public void Load()
        {
            //Load Level data here.
            GameObjectLoader gol = new GameObjectLoader("Assets//Scripts//Gamedat.xml");
            _aiFactory = new AiFactory(gol.GetAiParser(), _gameData);
            CreateLevel();
        }

        private void CreateLevel()
        {
            _enemies = CreateEnemies();
        }

        public List<IAGameObject> CreateEnemies()
        {
            List<IAGameObject> enemies = new List<IAGameObject>();
            enemies.Add(_aiFactory.GetAi(0));
            enemies.Add(_aiFactory.GetAi(1));
            enemies.Add(_aiFactory.GetAi(0));
            enemies.Add(_aiFactory.GetAi(1));
            enemies.Add(_aiFactory.GetAi(0));
            enemies.Add(_aiFactory.GetAi(1));
            enemies.Add(_aiFactory.GetAi(0));
            enemies.Add(_aiFactory.GetAi(1));
            enemies.Add(_aiFactory.GetAi(0));
            enemies.Add(_aiFactory.GetAi(1));
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

        public void DeInitialise()
        {
            _enemies.Clear();
            _enemies = null;
        }
    }
}
