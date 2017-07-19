using System.Windows;
using System.Runtime.CompilerServices;
using OuterSpace.GameObjects.Armory;
using CommonRelay.Common;
using CommonRelay.DataObjects;

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
        private int _speedRange = 5;
        private Mathematics _maths = new Mathematics();
        private ArmamentType _weaponType = ArmamentType.Missile;
        private int _firingClockGranularity = 5;

        public EnemyOne(GameData gameData, int framesPerSecond) : base(gameData, null, null)
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
            _armoryType = ArmoryType.AI;
            _armamentType = _weaponType;
            FireAngle = 90;
            AdjustFiringClock();

            _boundingBox = new BoundingBox (new Box { Left = 0, Top = 0, Width = _width, Height = _height } );
            SetRandomStartPosition();
            _gameObjectDim = new Size(_boundingBox.Dimension.Width, _boundingBox.Dimension.Height);
            SetupGameObject();
            _angle = GenerateRangedRandom(1, _headingAngleRange);

            _hitbar = new ShipComponents.Hitbar(new Point(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top), _boundingBox);
            _uiComponents.Add(_hitbar.GetElements()[0]);

            AutoSpeed();
            Update();
        }

        public override void AdjustFiringClock()
        {
            SetRandomFiringMilliFrequency();
            FiringClock *= _firingClockGranularity;
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

        private void AutoSpeed()
        {
            _speed = (int)GenerateRangedRandom(1, _speedRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Point SpeedAndHeadingControl()
        {
            Heading();
            // new Point(_maths.GetX(_angle, _speed), _maths.GetY(_angle, _speed));
            return _maths.GetXY(_angle, _speed);
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
            Point speedXY = SpeedAndHeadingControl();
            _boundingBox.Dimension = new Box { Left = _boundingBox.Dimension.Left  + (_moveScaleX * speedXY.X), Top = _boundingBox.Dimension.Top + (_moveScaleY * speedXY.Y), Width = _boundingBox.Dimension.Width, Height = _boundingBox.Dimension.Height };
            BoundryCorrection(_gameData.ViewportBounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Update()
        {
            SetMovementScaler();
            AdjustPositionAndBounding();
            _hitbar.Update();
        }

        public override void Render()
        {
            _hitbar.Render();
            base.Render();
        }
    }
}
