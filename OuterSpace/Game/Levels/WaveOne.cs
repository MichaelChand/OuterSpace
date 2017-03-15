using OuterSpace.GameObjects.Ships.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Game.Levels
{
    public class WaveOne
    {
        private List<EnemyShip> _enemies;
        public WaveOne()
        {
            CreateLevel();
        }

        private void CreateLevel()
        {
            _enemies = CreateEnemies();
        }

        public List<EnemyShip> CreateEnemies()
        {
            List<EnemyShip> enemies = new List<EnemyShip>();
            enemies.Add(new EnemyOne());
            return enemies;
        }
    }


}
