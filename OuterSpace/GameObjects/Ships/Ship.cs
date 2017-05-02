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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OuterSpace.GameObjects.Ships
{
    public class Ship : IAGameObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsWireframe = false;
        private Thickness _drawPosition;
        private BitmapImage _textureLOD;
        protected BoundingBox _boundingBox;
        protected string _texturePath;
        protected Image _shipTexture;
        protected GameData _gameData;
        protected PhysicsSystem _physics = new PhysicsSystem();
        protected Size _shipDim { get; set; }
        private Rectangle _rectangle;

        public BitmapImage TextureLOD
        {
            get { return _textureLOD; }
            private set
            {
                _textureLOD = value;
                OnShipPropertyChanged("TextureLOD");
            }
        }

        public Thickness DrawPosition
        {
            get { return _drawPosition; }
            set
            {
                _drawPosition = value;
                OnShipPropertyChanged("DrawPosition");
            }
        }

        protected virtual void LoadShipTexture(string texturePath, Size size)
        {
            TextureReader textureLOD = new TextureReader();
            TextureLOD = textureLOD.LoadTextureFromAssemblyPath(_texturePath, (int)size.Width, (int)size.Height);
        }

        protected virtual void SetupShip()
        {
            _shipTexture = new Image();
            LoadShipTexture(_texturePath, _shipDim);
            ApplyBinding();
            _uiComponents.Add(_shipTexture);
            if(IsWireframe)
                DisplayWireFrame();
        }

        private void DisplayWireFrame()
        {
            _rectangle = new Rectangle();
            Binding DrawPositionBinding = new Binding("DrawPosition");
            DrawPositionBinding.Source = this;
            _rectangle.SetBinding(Image.MarginProperty, DrawPositionBinding);
            _rectangle.Width = _boundingBox.Dimension.Width;
            _rectangle.Height = _boundingBox.Dimension.Height;
            _rectangle.Stroke = new SolidColorBrush(Colors.Red);
            _uiComponents.Add(_rectangle);
        }

        protected virtual void ApplyBinding()
        {
            Binding TextureLODBinding = new Binding("TextureLOD");
            Binding DrawPositionBinding = new Binding("DrawPosition");
            TextureLODBinding.Source = this;
            DrawPositionBinding.Source = this;
            _shipTexture.SetBinding(Image.SourceProperty, TextureLODBinding);
            _shipTexture.SetBinding(Image.MarginProperty, DrawPositionBinding);
        }

        public override void Update()
        {
            
        }

        public override void Render()
        {
            DrawPosition = new Thickness(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top, DrawPosition.Right, DrawPosition.Bottom);
        }

        public BoundingBox GetBoundingBox()
        {
            return _boundingBox;
        }

        public Point GetPosition()
        {
            return new Point(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top);
        }

        public double GetWidth()
        {
            return _boundingBox.Dimension.Width;
        }

        public double GetHeight()
        {
            return _boundingBox.Dimension.Height;
        }

        protected void SetPosition(int x, int y)
        {
            _boundingBox.Dimension.Left = x;
            _boundingBox.Dimension.Top = y;
        }

        protected void SetPosition(Point position)
        {
            _boundingBox.Dimension.Left = position.X;
            _boundingBox.Dimension.Top = position.Y;
        }

        protected void SetShipWidth(double width)
        {
            _boundingBox.Dimension.Width = width;
        }

        protected void SetShipHeight(double height)
        {
            _boundingBox.Dimension.Height = height;
        }

        protected bool BoundryCorrection(BoundingBox boundry)
        {
            CollisionDirection collisionDirection = _physics.BoundryCollisionDirection(boundry, _boundingBox);
            switch(collisionDirection)
            {
                case CollisionDirection.Left:
                    _boundingBox.Dimension.Left = 0;
                    return true;
                case CollisionDirection.Top:
                    _boundingBox.Dimension.Top = 0;
                    return true;
                case CollisionDirection.Right:
                    _boundingBox.Dimension.Left = boundry.Dimension.Left + boundry.Dimension.Width - _boundingBox.Dimension.Width;
                    return true;
                case CollisionDirection.Bottom:
                    _boundingBox.Dimension.Top = boundry.Dimension.Top + boundry.Dimension.Height - _boundingBox.Dimension.Height;
                    return true;
            }

            return false;
        }

        #region Notify event handlers
        protected void OnShipPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
