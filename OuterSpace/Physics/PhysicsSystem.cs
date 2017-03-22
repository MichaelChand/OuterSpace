using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OuterSpace.Common;
using System.Windows;

namespace OuterSpace.Physics
{
    public class PhysicsSystem
    {
        public Point BoundingTest(BoundingBox boundry, BoundingBox boxB, Point scaler)
        {
            int xScale = (int)scaler.X;
            int yScale = (int)scaler.Y;
            CollisionDirection collisionDirection = BoundryCollisionDirection(boundry, boxB);
            switch(collisionDirection)
            {
                case CollisionDirection.Left:
                    xScale *= -1;
                    break;
                case CollisionDirection.Top:
                    yScale *= -1;
                    break;
                case CollisionDirection.Right:
                    xScale *= -1;
                    break;
                case CollisionDirection.Bottom:
                    yScale *= -1;
                    break;
            }
            return new Point(xScale, yScale);
        }

        public CollisionDirection BoundryCollisionDirection(BoundingBox boundry, BoundingBox box)
        {
            if (box.Dimension.Left <= 0)
                return CollisionDirection.Left;
            if (box.Dimension.Top <= 0)
                return CollisionDirection.Top;
            if (box.Dimension.Left + box.Dimension.Width >= boundry.Dimension.Left + boundry.Dimension.Width)
                return CollisionDirection.Right;
            if (box.Dimension.Top + box.Dimension.Height >= boundry.Dimension.Top + boundry.Dimension.Height)
                return CollisionDirection.Bottom;
            return CollisionDirection.None;
        }

        public void CollisionDetection(/*param A, param B*/)
        {

        }
    }
}
