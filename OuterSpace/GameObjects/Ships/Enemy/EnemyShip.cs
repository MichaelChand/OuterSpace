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
        private BitmapImage _textureLOD;
        private Image _shipTexture;
        protected string _texturePath;
        protected int _moveScaleX = 1;
        protected int _moveScaleY = 1;
        protected int _speed = 2;
        protected PhysicsSystem _physics = new PhysicsSystem();

        public BitmapImage TextureLOD
        {
            get { return _textureLOD; }
            private set
            {
                _textureLOD = value;
                OnShipPropertyChanged("TextureLOD");
            }
        }

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
            Position = new Point(GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Width), GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Height));
        }

        protected virtual void SetupShip()
        {
            _shipTexture = new Image();
            LoadShipTexture(_texturePath);
            ApplyBinding();
            _uiComponents.Add(_shipTexture);
        }

        private void LoadShipTexture(string texturePath)
        {
            TextureReader textureLOD = new TextureReader();
            TextureLOD = textureLOD.BitmapFromFile(_texturePath);
        }

        private void ApplyBinding()
        {
            Binding TextureLODBinding = new Binding("TextureLOD");
            Binding DrawPositionBinding = new Binding("DrawPosition");
            TextureLODBinding.Source = this;
            DrawPositionBinding.Source = this;
            _shipTexture.SetBinding(Image.SourceProperty, TextureLODBinding);
            _shipTexture.SetBinding(Image.MarginProperty, DrawPositionBinding);
        }

        protected double GenerateStartingPosition(int min, int max)
        {
            return _random.Next(min, max);
        }

        public override void Update()
        {
            Point scaler = _physics.BoundingTest(_gameData.ViewportBounding, _boundingBox, new Point(_moveScaleX, _moveScaleY));
            _moveScaleX = (int)scaler.X;
            _moveScaleY = (int)scaler.Y;
            Position = new Point(Position.X + ( _moveScaleX * _speed), Position.Y + (_moveScaleY * _speed));
            _boundingBox.Dimension = new Box { Left = Position.X, Top = Position.Y, Right = _boundingBox.Dimension.Right, Bottom = _boundingBox.Dimension.Bottom };
        }
    }
}
