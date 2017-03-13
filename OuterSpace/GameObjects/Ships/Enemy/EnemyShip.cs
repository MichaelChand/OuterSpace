using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.GameObjects.Ships.Enemy
{
    public class EnemyShip : Ship
    {
        private Random _random = new Random();

        public EnemyShip(double screenWidth, double screenHeight)
        {
            _boundingBox = new BoundingBox(new Point(GenerateStartingPosition(0, (int)screenWidth), GenerateStartingPosition(0, (int)screenHeight)), 50,50);
        }

        protected double GenerateStartingPosition(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
