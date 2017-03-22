using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OuterSpace.Physics;
using OuterSpace.Game;
using System.Windows;

namespace OuterSpace.GameObjects.Ships.Enemy
{
    public class EnemyOne : EnemyShip
    {
        private int _width = 50;
        private int _height = 50;

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
            int startX = (int)GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Dimension.Width);
            int startY = (int)GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Dimension.Height);
            _boundingBox = new BoundingBox (new Box { Left = startX, Top = startY, Width = _width, Height = _height } );
            SetupShip(new Size(_width, _height));
            SetRandomStartPosition();
        }

        public override void Update()
        {
            Point scaler = _physics.BoundingTest(_gameData.ViewportBounding, _boundingBox, new Point(_moveScaleX, _moveScaleY));
            _moveScaleX = (int)scaler.X;
            _moveScaleY = (int)scaler.Y;
            Position = new Point(Position.X + (_moveScaleX * _speed), Position.Y + (_moveScaleY * _speed));
            _boundingBox.Dimension = new Box { Left = Position.X, Top = Position.Y, Width = _boundingBox.Dimension.Width, Height = _boundingBox.Dimension.Height };
            BoundryCorrection(_gameData.ViewportBounding);
        }
    }
}
