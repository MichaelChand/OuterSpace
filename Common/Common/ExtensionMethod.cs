using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Common.Common
{
    public static class ExtensionMethod
    {
        public enum IntersectType
        {
            AxisAligned, SquareIntersect
        }

        private static bool AxisAligned(BoundingBox boxA, BoundingBox boxB)
        {
            if (boxA.Dimension.Left + boxA.Dimension.Width < boxB.Dimension.Left)
                return false;
            if (boxA.Dimension.Left > boxB.Dimension.Left + boxB.Dimension.Width)
                return false;
            if (boxA.Dimension.Top + boxA.Dimension.Height < boxB.Dimension.Top)
                return false;
            if (boxA.Dimension.Top > boxB.Dimension.Top + boxB.Dimension.Height)
                return false;

            return true;
        }

        private static bool SquareIntersect(BoundingBox boxA, BoundingBox boxB)
        {
            Mathematics maths = new Mathematics();
            //Get Center points.
            Point p1 = maths.GetRectangleCenterPoint(new Point(boxA.Dimension.Left, boxA.Dimension.Top), boxA.Dimension.Width, boxA.Dimension.Height);
            Point p2 = maths.GetRectangleCenterPoint(new Point(boxB.Dimension.Left, boxB.Dimension.Top), boxB.Dimension.Width, boxB.Dimension.Height);

            //Get distance between points 
            double d = maths.DistanceBetweenPoints(p1, p2);

            //Get adjacent of right-angle-traingle formed by points p1 and p2.
            double x = maths.GetLateralDistanceBetweenPoints(p1, p2);

            //Get angle of intersection  between adjacent and hypotenuse of p1p2 triangle.
            double angle = maths.GetAngleAH(x, d);

            double d1;
            double d2;
            if (angle < 45)
            {
                d1 = maths.LengthOfHypotenuseFromAngleAndAdjacent(angle, boxA.Dimension.Width / 2);
                d2 = maths.LengthOfHypotenuseFromAngleAndAdjacent(angle, boxB.Dimension.Width / 2);
            }
            else
            {
                d1 = maths.LengthOfHypotenuseFromAngleAndAdjacent(90 - angle, boxA.Dimension.Height / 2);
                d2 = maths.LengthOfHypotenuseFromAngleAndAdjacent(90 - angle, boxB.Dimension.Height / 2);
            }

            return (d1 + d2) > d ? true : false;
        }

        public static bool Intersects(this BoundingBox boxA, BoundingBox boxB, IntersectType intersectType = IntersectType.AxisAligned)
        {
            switch (intersectType)
            {
                case IntersectType.AxisAligned:
                    return AxisAligned(boxA, boxB);
                case IntersectType.SquareIntersect:
                    return SquareIntersect(boxA, boxB);
                default:
                    return false;
            }
        }

        public static List<T> CapacityTrim<T>(this List<T> list)
        {
            list.Capacity = list.Count + 2;
            return list;
        }
    }
}
