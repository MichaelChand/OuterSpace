
using CommonRelay.Common;
using CommonRelay.DataObjects;
using OuterSpace.GameObjects.ShipComponents;
using System;

namespace OuterSpace.GameObjects.Ships.Enemy
{
    public class EnemyShip : Ship
    {
        private Random _random;
        protected int _moveScaleX = 1;
        protected int _moveScaleY = 1;
        protected int _speed = 2;
        public Hitbar HitPointBar { get; set; }

        public bool ShowHitbar { get; set; }

        public EnemyShip()
        {

        }

        public EnemyShip(GameData gameData, BoundingBox boundingBox, string texturePath)
        {
            _random = new Random(GetHashCode());
            _gameData = gameData;
            _texturePath = texturePath;
            _boundingBox = boundingBox;
            //HitbarInit();
        }

        protected void HitbarInit()
        {
            
            HitPointBar = new ShipComponents.Hitbar(_boundingBox, Strength);
            _uiComponents.Add(HitPointBar.GetElements()[0]);
        }

        public void AdjustHitBar(double value)
        {
            HitPointBar.HitpointSubtract(value);
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

        protected double GenerateRangedRandom(int seed)
        {
            return _random.Next();
        }

        public virtual void AdjustFiringClock()
        {
            SetRandomFiringMilliFrequency();
        }

        protected void SetRandomFiringMilliFrequency()
        {
            FiringClock = (long)GenerateRangedRandom(1, _gameData.FramesPerSecond) + _gameData.FramesPerSecond; //minimum 1 second interval.
        }

        public override void Render()
        {
            if(ShowHitbar)
                HitPointBar.Render();
            base.Render();
        }
    }
}
