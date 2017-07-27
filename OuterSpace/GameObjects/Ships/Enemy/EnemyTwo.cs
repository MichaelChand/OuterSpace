using System.Windows;
using System.Runtime.CompilerServices;
using CommonRelay.Common;
using CommonRelay.DataObjects;

namespace OuterSpace.GameObjects.Ships.Enemy
{
    public class EnemyTwo : EnemyShip
    {
        private int _width = 10;
        private int _height = 70;
        private double _angle;
        private readonly int _headingAngleRange = 160;
        private int _frameCount = 0;
        private int _framesPerSecond;
        private int _secondsRange = 10;
        private int _waitToChangeHeadingSeconds = 10;
        private int _speedRange = 5;
        private Mathematics _maths = new Mathematics();
        private int _firingClockGranularity = 2;
        private double _hitpoint = 50;

        public EnemyTwo(GameData gameData, bool showHitbar) : this(gameData, null, null)
        {
            _texturePath = "Assets//Images//SampleBlank.png";
            _framesPerSecond = gameData.FramesPerSecond;
            base.Strength = _hitpoint;
            ShowHitbar = showHitbar;
            Initialise();
        }

        public EnemyTwo(GameData gameData, BoundingBox boundingBox, string texturePath) : base(gameData, boundingBox, texturePath)
        {
        }

        private void Initialise()
        {
            _boundingBox = new BoundingBox (new Box { Left = 0, Top = 0, Width = _width, Height = _height } );
            //SetRandomStartPosition();
            _boundingBox.Dimension.Left = 100;
            _boundingBox.Dimension.Top = 500;
            _gameObjectDim = new Size(_boundingBox.Dimension.Width, _boundingBox.Dimension.Height);
            SetupGameObject();
            _angle = GenerateRangedRandom(1, _headingAngleRange);
            AdjustFiringClock();
            if (ShowHitbar)
                HitbarInit();
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
            double angle = _angle;
            Heading();
            return new Point(_maths.GetX(_angle, _speed), _maths.GetY(_angle, _speed));
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
        }
    }
}
