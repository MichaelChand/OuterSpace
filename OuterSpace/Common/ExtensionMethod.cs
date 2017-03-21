using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Common
{
    public static class ExtensionMethod
    {
        public static bool Intersects(this BoundingBox boxA,  BoundingBox boxB)
        {
            if ((((boxA.Dimension.Left + boxA.Width) >= boxB.Dimension.Left) && boxA.Dimension.Left <= boxB.Dimension.Left) && 
                (((boxA.Dimension.Top + boxA.Height) >= boxB.Dimension.Top) && boxA.Dimension.Top <= boxB.Dimension.Top)) 
                return true;
            return false;
        }
    }
}
