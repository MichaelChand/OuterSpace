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
            return _boundingBox.Position;
        }

        public double GetWidth()
        {
            return _boundingBox.Width;
        }

        public double GetHeight()
        {
            return _boundingBox.Height;
        }

        protected void SetPosition(int x, int y)
        {
            _boundingBox.Position = new Point(x, y);
        }

        protected void SetPosition(Point position)
        {
            _boundingBox.Position = position;
        }

        protected void SetShipWidth(double width)
        {
            _boundingBox.Width = width;
        }

        protected void SetShipHeight(double height)
        {
            _boundingBox.Height = height;
        }

        #region Notify event handlers
        protected void OnShipPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
