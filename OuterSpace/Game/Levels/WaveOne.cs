﻿using OuterSpace.GameObjects;
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
            enemies.Add(new EnemyOne(_gameData));
            return enemies;
        }

        public List<IAGameObject> GetLevelObjects()
        {
            return _enemies;
        }
    }


}
