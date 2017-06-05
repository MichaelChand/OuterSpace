using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.Common
{
    public class Mathematics
    {
        public double RemoveByPercentage(double value, double percent)
        {
            return (value * 2) - (value * (1 + (percent / 100)));
        }

        public double FramesToSeconds(int frames, int framesPerSecond)
        {
            return FrequencyToPeriod(framesPerSecond) * frames;
        }

        public double FrequencyToPeriod(int frequency)
        {
            return 1.0f / frequency;
        }

        public double GetX(double degrees, double radius)
        {
            return radius * Math.Cos(DegreesToRadians(degrees));
        }

        public double GetY(double degrees, double radius)
        {
            return radius * Math.Sin(DegreesToRadians(degrees));
        }

        public Point GetXY(double degrees, double radius)
        {
            return new Point(GetX(degrees, radius), GetY(degrees, radius));
        }

        public double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public double RadiansToDegrees(double radians)
        {
            return radians * (180.0/Math.PI);
        }

        public Point GetRectangleCenterPoint(Point position, double width, double height)
        {
            return new Point(position.X + width / 2.0f, position.Y + height / 2.0f);
        }

        public double DistanceBetweenPoints(Point p1, Point p2)
        {
            double xDifSquared = Math.Pow((p2.X - p1.X), 2.0);
            double yDifSquared = Math.Pow((p2.Y - p1.Y), 2.0);
            return Math.Sqrt(xDifSquared + yDifSquared);
        }

        public double LengthOfHypotenuse(double width, double height)
        {
            return Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2));
        }

        public double LengthOfHypotenuseFromAngleAndAdjacent(double angle, double adjacent)
        {
            return adjacent / Math.Cos(DegreesToRadians(angle));
        }

        public double LengthOfHypotenuseFromSinAngleAndAdjacent(double angle, double adjacent)
        {
            return adjacent / Math.Sin(DegreesToRadians(angle));
        }

        public double GetLateralDistanceBetweenPoints(Point p1, Point p2)
        {
            return Math.Abs(p2.X - p1.X);
        }

        public double GetAngleAH(double adjacent, double hypotenuse)
        {
            return RadiansToDegrees(Math.Acos(adjacent / hypotenuse));
            
        }
    }
}
