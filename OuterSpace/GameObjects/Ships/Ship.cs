using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.GameObjects.Ships
{
    public class Ship : IAGameObject
    {
        protected BoundingBox _boundingBox;

        public override void Render()
        {
            throw new NotImplementedException();
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
    }
}
