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
        public EnemyOne(GameData gameData) : this(gameData, null, null)
        {
            _texturePath = "Assets//Images//SampleBlank.png";
            Initialise();
        }

        public EnemyOne(GameData gameData, BoundingBox boundingBox, string texturePath) : base(gameData, boundingBox, texturePath)
        {
        }

        private void Initialise()
        {
            int startX = (int)GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Width);
            int startY = (int)GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Height);
            _boundingBox = new BoundingBox(new System.Windows.Point(startX, startY), new Box { Left = startX, Top = startY, Right = 0, Bottom = 0 });
            SetupShip();
            SetRandomStartPosition();
        }
    }
}
