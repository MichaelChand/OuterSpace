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
        protected Point Position;
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
            DrawPosition = new Thickness(Position.X, Position.Y, DrawPosition.Right, DrawPosition.Bottom);
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
                    Position = new Point(0, Position.Y);
                    _boundingBox.Dimension.Left = 0;
                    break;
                case CollisionDirection.Top:
                    Position = new Point(Position.X, 0);
                    _boundingBox.Dimension.Top = 0;
                    break;
                case CollisionDirection.Right:
                    Position = new Point(boundry.Dimension.Left + boundry.Dimension.Width - _boundingBox.Dimension.Width, Position.Y);
                    _boundingBox.Dimension.Left = Position.X;
                    break;
                case CollisionDirection.Bottom:
                    Position = new Point(Position.X, boundry.Dimension.Top + boundry.Dimension.Height - _boundingBox.Dimension.Height);
                    _boundingBox.Dimension.Top = Position.Y;
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
