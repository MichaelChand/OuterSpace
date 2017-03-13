﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.Physics
{
    public class BoundingBox
    {
        public Point Position { get; set; }
        public Box Dimension { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public BoundingBox(Point position, double left, double top, double right, double bottom) : this(position, new Box { Left = left, Top = top, Right = right, Bottom = bottom })
        {
        }

        public BoundingBox(double X, double Y, double left, double top, double right, double bottom) : this(new Point(X, Y), left, top, right, bottom)
        {

        }

        public BoundingBox(Point position, Box box)
        {
            Position = position;
            Dimension = box;
        }
    }
}
