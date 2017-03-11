using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Common
{
    public class Mathematics
    {
        public double GetX(double degree, double x, double radius)
        {
            return x * (radius * Math.Cos(degree));
        }
    }
}
