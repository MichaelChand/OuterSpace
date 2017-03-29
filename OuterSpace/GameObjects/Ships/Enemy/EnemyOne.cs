using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OuterSpace.Physics;
using OuterSpace.Game;
using System.Windows;
using System.Runtime.CompilerServices;
using OuterSpace.Common;

namespace OuterSpace.GameObjects.Ships.Enemy
{
    public class EnemyOne : EnemyShip
    {
        private int _width = 50;
        private int _height = 50;
        private double _angle;
        private readonly int _headingAngleRange = 160;
        private int _frameCount = 0;
        private int _framesPerSecond;
        private int _secondsRange = 10;
        private int _waitToChangeHeadingSeconds = 10;
        private Mathematics _maths = new Mathematics();

        public EnemyOne(GameData gameData, int framesPerSecond) : this(gameData, null, null)
        {
            _texturePath = "Assets//Images//SampleBlank.png";
            _framesPerSecond = framesPerSecond;
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
            _angle = GenerateRangedRandom(1, _headingAngleRange);
            Update();
        }

        private void Heading()
        {
            _frameCount++;
            double secondsElapsed = _maths.FramesToSeconds(_frameCount, _gameData.FramesPerSecond);
            if(secondsElapsed >= _waitToChangeHeadingSeconds)
            {
                _waitToChangeHeadingSeconds = (int)GenerateRangedRandom(1, _secondsRange);
                _angle = GenerateRangedRandom(1, _headingAngleRange);
                _frameCount = 0;
            }
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
            Heading();
            double X = _maths.GetX(_angle, _speed);
            double Y = _maths.GetY(_angle, _speed);
            _boundingBox.Dimension = new Box { Left = _boundingBox.Dimension.Left  + (_moveScaleX * X), Top = _boundingBox.Dimension.Top + (_moveScaleY * Y), Width = _boundingBox.Dimension.Width, Height = _boundingBox.Dimension.Height };
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
