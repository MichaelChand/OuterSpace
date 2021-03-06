﻿using CommonRelay.Extensions;
using CommonRelay.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsSystem
{
    public class CollisionDetector
    {
        public bool Collision(BoundingBox boxA, BoundingBox boxB)
        {
            return boxA.Intersects(boxB);
        }

        //Caution: returns first occurance of detected collision item in the list.
        public BoundingBox CollisionWith(BoundingBox boxA, List<BoundingBox> boxes)
        {
            return (from box in boxes
                    where (boxA.Intersects(box, ExtensionMethod.IntersectType.AxisAligned))
                    select box).ToArray()[0];
        }
    }
}
