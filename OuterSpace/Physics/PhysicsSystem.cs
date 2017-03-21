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
        public Point BoundingTest(BoundingBox boxA, BoundingBox boxB, Point scaler)
        {
            int xScale = (int)scaler.X;
            int yScale = (int)scaler.Y;
            if (boxB.Dimension.Left <= 0)
            {
                xScale *= -1;
            }
            if(boxB.Dimension.Top <= 0)
            {
                yScale *= -1;
            }
            if(boxB.Dimension.Left + boxB.Width >= boxA.Dimension.Left + boxA.Width)
            {
                xScale *= -1;
            }
            if (boxB.Dimension.Top + boxB.Height >= boxA.Dimension.Top + boxA.Height)
            {
                yScale *= -1;
            }

            return new Point(xScale, yScale);
        }

        public void CollisionDetection(/*param A, param B*/)
        {

        }
    }
}
