using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonRelay.DataObjects
{
    public class BoundingBox
    {
        public Box Dimension { get; set; }

        public BoundingBox(double X, double Y, double width, double height) : this(new Box { Left = X, Top = Y, Width = width, Height = height })
        {
        }

        public BoundingBox(Point position, double width, double height) : this(position.X, position.Y, width, height)
        {
        }

        public BoundingBox(Box dimension)
        {
            Dimension = dimension;
        }
    }
}
