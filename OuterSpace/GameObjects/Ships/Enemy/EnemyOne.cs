using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OuterSpace.Physics;
using OuterSpace.Game;
using System.Windows;
using System.Runtime.CompilerServices;

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
            _boundingBox = new BoundingBox (new Box { Left = 0, Top = 0, Width = _width, Height = _height } );
            SetRandomStartPosition();
            SetupShip();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetMovementScaler()
        {
            Point scaler = _physics.BoundingTest(_gameData.ViewportBounding, _boundingBox, new Point(_moveScaleX, _moveScaleY));
            _moveScaleX = (int)scaler.X;
            _moveScaleY = (int)scaler.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AdjustPositionAndBounding()
        {
            _boundingBox.Dimension = new Box { Left = _boundingBox.Dimension.Left + (_moveScaleX * _speed), Top = _boundingBox.Dimension.Top + (_moveScaleY * _speed), Width = _boundingBox.Dimension.Width, Height = _boundingBox.Dimension.Height };
            BoundryCorrection(_gameData.ViewportBounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Update()
        {
            SetMovementScaler();
            AdjustPositionAndBounding();
        }
    }
}
