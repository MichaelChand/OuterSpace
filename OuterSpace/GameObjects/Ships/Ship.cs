using OuterSpace.Common;
using OuterSpace.Game;
using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OuterSpace.GameObjects.Ships
{
    public class Ship : GameObject
    {

        public override void Update()
        {
            
        }

        protected override bool BoundryCorrection(BoundingBox boundry)
        {
            bool collisionDetected = false;
            Func<CollisionDirection, bool> CorrectBoundryIntrusion = (collisionDirection) =>
            {
                switch (collisionDirection)
                {
                    case CollisionDirection.Left:
                        _boundingBox.Dimension.Left = 0;
                        return true;
                    case CollisionDirection.Top:
                        _boundingBox.Dimension.Top = 0;
                        return true;
                    case CollisionDirection.Right:
                        _boundingBox.Dimension.Left = boundry.Dimension.Left + boundry.Dimension.Width - _boundingBox.Dimension.Width;
                        return true;
                    case CollisionDirection.Bottom:
                        _boundingBox.Dimension.Top = boundry.Dimension.Top + boundry.Dimension.Height - _boundingBox.Dimension.Height;
                        return true;
                    default:
                        return false;
                }
            };

            CollisionDirection[] collisionDirections = _physics.BoundryCollisionDirections(boundry, _boundingBox);

            for(int i = 1; i >=0; i--)
                if (CorrectBoundryIntrusion(collisionDirections[i]))
                    collisionDetected = true;

            return collisionDetected;
        }
    }
}
