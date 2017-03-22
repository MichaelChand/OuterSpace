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
            _boundingBox.Dimension.Left = GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Dimension.Width);
            _boundingBox.Dimension.Top = GenerateStartingPosition(0, (int)_gameData.ViewportBounding.Dimension.Height);
        }

        protected virtual void SetupShip()
        {
            _shipTexture = new Image();
            LoadShipTexture(_texturePath, new Size(_boundingBox.Dimension.Width, _boundingBox.Dimension.Height));
            ApplyBinding();
            _uiComponents.Add(_shipTexture);
        }

        private void LoadShipTexture(string texturePath, Size size)
        {
            TextureReader textureLOD = new TextureReader();
            TextureLOD = textureLOD.BitmapFromFile(_texturePath, (int)size.Width, (int)size.Height);
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
    }
}
