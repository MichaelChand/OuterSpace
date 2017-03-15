using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OuterSpace.Physics;
using OuterSpace.Game;

namespace OuterSpace.GameObjects.Ships.Enemy
{
    public class EnemyOne : EnemyShip
    {
        public EnemyOne()
        {

        }

        public EnemyOne(GameData gameData, BoundingBox boundingBox, string texturePath) : base(gameData, boundingBox, texturePath)
        {
        }
    }
}
