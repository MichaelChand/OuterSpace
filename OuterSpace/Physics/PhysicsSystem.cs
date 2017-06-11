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

            Action<CollisionDirection> CollisionHandler = (collisionDir) =>
            {
                switch (collisionDir)
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
            };

            CollisionDirection collisionDirection = LateralCollision(boundry, boxB);
            CollisionHandler(collisionDirection);
            collisionDirection = VentralCollision(boundry, boxB);
            CollisionHandler(collisionDirection);

            return new Point(xScale, yScale);
        }

        public CollisionDirection[] BoundryCollisionDirections(BoundingBox boundry, BoundingBox box)
        {
            CollisionDirection[] collisionDirections = new CollisionDirection[2];
            collisionDirections[0] = LateralCollision(boundry, box);
            collisionDirections[1] = VentralCollision(boundry, box);

            return collisionDirections;
        }

        private CollisionDirection LateralCollision(BoundingBox boundry, BoundingBox box)
        {
            if (box.Dimension.Left <= 0)
                return CollisionDirection.Left;
            if (box.Dimension.Left + box.Dimension.Width >= boundry.Dimension.Left + boundry.Dimension.Width)
                return CollisionDirection.Right;
            return CollisionDirection.None;
        }

        private CollisionDirection VentralCollision(BoundingBox boundry, BoundingBox box)
        {
            if (box.Dimension.Top <= 0)
                return CollisionDirection.Top;
            
            if (box.Dimension.Top + box.Dimension.Height >= boundry.Dimension.Top + boundry.Dimension.Height)
                return CollisionDirection.Bottom;
            return CollisionDirection.None;
        }

        public bool CollisionDetection(BoundingBox boxA, BoundingBox boxB)
        {
            return boxA.Intersects(boxB);
        }
    }
}
