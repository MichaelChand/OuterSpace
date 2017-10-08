
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
            Alive = true;
            //HitbarInit();
        }

        protected void HitbarInit(bool show = true)
        {
            
            HitPointBar = new ShipComponents.Hitbar(_boundingBox, Strength, show);
            _uiComponents.Add(HitPointBar.GetElements()[0]);
        }

        public void AdjustHitBar(double value)
        {
            double val = Strength % 100;
            HitPointBar.HitpointSubtract(value);
        }

        public void SetHitpoint(double value)
        {
            double factor = 100 / MaxStrength;
            HitPointBar.SetHitpoint(100 - ((factor * value) - 100));
        }

        public virtual void SetRandomStartPosition()
        {
            _boundingBox.Dimension.Left = GenerateRangedRandom(0, (int)_gameData.ViewportBounding.Dimension.Width);
            _boundingBox.Dimension.Top = GenerateRangedRandom(0, (int)_gameData.ViewportBounding.Dimension.Height);
        }

        protected double GenerateRangedRandom(int min, int max)
        {
            return _random.Next(min, max == 0? 1 : max);
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
            FiringClock = (long)GenerateRangedRandom(_gameData.FramesPerSecond/4, (int)FiringClockSet); //minimum 1 second interval.
        }

        public override void Render()
        {
            if(ShowHitbar)
                HitPointBar.Render();
            base.Render();
        }
    }
}
