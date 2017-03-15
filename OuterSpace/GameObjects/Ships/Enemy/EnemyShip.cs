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
        private string _texturePath;

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
            SetupShip();
        }

        public virtual void SetRandomStartPosition()
        {
            Position = new Point(GenerateStartingPosition(0, _gameData.ViewPortWidth), GenerateStartingPosition(0, _gameData.ViewPortHeight));
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
    }
}
