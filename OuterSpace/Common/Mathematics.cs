using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Common
{
    public class Mathematics
    {
        public double RemoveByPercentage(double value, double percent)
        {
            return (value * 2) - (value * (1 + (percent / 100)));
        }
        public double GetX(double degree, double x, double radius)
        {
            return x * (radius * Math.Cos(degree));
        }
    }
}
