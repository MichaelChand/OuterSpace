using OuterSpace.Common;
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
    public class EnemyShip : Ship, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
                OnEnemyShipPropertyChanged("TextureLOD");
            }
        }

        public EnemyShip(double screenWidth, double screenHeight, BoundingBox boundingBox, string texturePath)
        {
            _texturePath = texturePath;
            _boundingBox = boundingBox;
            SetupShip();
        }

        private void SetupShip()
        {
            _shipTexture = new Image();
            LoadShipTexture(_texturePath);
            ApplyBinding();
            _shipTexture.Margin = new Thickness(200, 200, 0, 0);
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
            TextureLODBinding.Source = this;
            _shipTexture.SetBinding(Image.SourceProperty, TextureLODBinding);
        }

        protected double GenerateStartingPosition(int min, int max)
        {
            return _random.Next(min, max);
        }

        #region Notify event handlers
        protected void OnEnemyShipPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
