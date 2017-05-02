using OuterSpace.Common;
using OuterSpace.Game;
using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OuterSpace.GameObjects.Ships.Enemy
{
    public class EnemyShip : Ship
    {
        private Random _random = new Random();
        protected int _moveScaleX = 1;
        protected int _moveScaleY = 1;
        protected int _speed = 2;

        public EnemyShip()
        {

        }

        public EnemyShip(GameData gameData, BoundingBox boundingBox, string texturePath)
        {
            _gameData = gameData;
            _texturePath = texturePath;
            _boundingBox = boundingBox;
            //SetupShip();
        }

        public virtual void SetRandomStartPosition()
        {
            _boundingBox.Dimension.Left = GenerateRangedRandom(0, (int)_gameData.ViewportBounding.Dimension.Width);
            _boundingBox.Dimension.Top = GenerateRangedRandom(0, (int)_gameData.ViewportBounding.Dimension.Height);
        }

        protected double GenerateRangedRandom(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
