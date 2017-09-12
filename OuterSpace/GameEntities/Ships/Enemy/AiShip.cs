using CommonRelay.Common;
using CommonRelay.DataObjects;
using OuterSpace.GameObjects.Armory;
using OuterSpace.GameObjects.Ships.Enemy;
using OuterSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.GameEntities.Ships.Enemy
{
    public class AiShip : EnemyShip
    {
        private AiModel _aiModel;
        private double _angle;
        private int _headingAngleRange = 160;
        private int _frameCount = 0;
        private int _framesPerSecond;
        private int _secondsRange = 10;
        private int _waitToChangeHeadingSeconds = 10;
        private int _speedRange = 5;
        private Mathematics _maths = new Mathematics();
        private ArmamentType _weaponType = ArmamentType.Missile;
        private int _firingClockGranularity = 5;
        private double _hitpoint = 50;

        public AiShip(AiModel aiModel, GameData gameData, bool showhitbar = true) : base(gameData, null, null)
        {
            _aiModel = aiModel;
            ShowHitbar = aiModel.HitBarShow;
            Initialise();
        }

        private ArmamentType GetArmanamentType()
        {
            switch (_aiModel.Weapon)
            {
                case 0:
                    return ArmamentType.Missile;
                default:
                    return ArmamentType.Missile;
            }
        }

        private void Initialise()
        {
            _headingAngleRange = (int)_aiModel.HeadingRange;
            _texturePath = string.Format("Assets//Images//{0}", _aiModel.Texture);
            _framesPerSecond = _gameData.FramesPerSecond;
            base.Strength = _aiModel.Strength;
            _weaponType = GetArmanamentType();

            _armoryType = ArmoryType.AI;
            _armamentType = _weaponType;
            FireAngle = _aiModel.ScanRange;
            AdjustFiringClock();
            _boundingBox = new BoundingBox(new Box { Left = 0, Top = 0, Width = _aiModel.Width, Height = _aiModel.Height });

            if (_aiModel.RandomStart.Contains("-1"))
                SetRandomStartPosition();
            else
                _boundingBox = new BoundingBox(new Box { Left = Double.Parse(_aiModel.RandomStart.Split(new char[] { ',' }).ToArray()[0]), Top = Double.Parse(_aiModel.RandomStart.Split(new char[] { ',' }).ToArray()[0]), Width = _aiModel.Width, Height = _aiModel.Height });

            _gameObjectDim = new Size(_boundingBox.Dimension.Width, _boundingBox.Dimension.Height);
            SetupGameObject();
            _angle = GenerateRangedRandom(1, _headingAngleRange);

            if (_aiModel.HitBarShow)
                HitbarInit();

            _speed = _aiModel.Speed;
            if(_aiModel.Speed <= 0)
                AutoSpeed();

            Update();
        }

        public override void AdjustFiringClock()
        {
            SetRandomFiringMilliFrequency();
            FiringClock *= _aiModel.FireFrequency;
        }

        private void Heading()
        {
            _frameCount++;
            double secondsElapsed = _maths.FramesToSeconds(_frameCount, _gameData.FramesPerSecond);
            if (secondsElapsed >= _waitToChangeHeadingSeconds)
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
            _boundingBox.Dimension = new Box { Left = _boundingBox.Dimension.Left + (_moveScaleX * speedXY.X), Top = _boundingBox.Dimension.Top + (_moveScaleY * speedXY.Y), Width = _boundingBox.Dimension.Width, Height = _boundingBox.Dimension.Height };
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
