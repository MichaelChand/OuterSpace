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
    }
}
