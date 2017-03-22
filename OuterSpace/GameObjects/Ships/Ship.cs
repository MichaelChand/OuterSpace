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

namespace OuterSpace.GameObjects.Ships
{
    public class Ship : IAGameObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected BoundingBox _boundingBox;
        private Thickness _drawPosition;
        protected GameData _gameData;
        protected PhysicsSystem _physics = new PhysicsSystem();

        public Thickness DrawPosition
        {
            get { return _drawPosition; }
            set
            {
                _drawPosition = value;
                OnShipPropertyChanged("DrawPosition");
            }
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

        protected void BoundryCorrection(BoundingBox boundry)
        {
            CollisionDirection collisionDirection = _physics.BoundryCollisionDirection(boundry, _boundingBox);
            switch(collisionDirection)
            {
                case CollisionDirection.Left:
                    _boundingBox.Dimension.Left = 0;
                    break;
                case CollisionDirection.Top:
                    _boundingBox.Dimension.Top = 0;
                    break;
                case CollisionDirection.Right:
                    _boundingBox.Dimension.Left = boundry.Dimension.Left + boundry.Dimension.Width - _boundingBox.Dimension.Width;
                    break;
                case CollisionDirection.Bottom:
                    _boundingBox.Dimension.Top = boundry.Dimension.Top + boundry.Dimension.Height - _boundingBox.Dimension.Height;
                    break;
            }
        }

        #region Notify event handlers
        protected void OnShipPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
