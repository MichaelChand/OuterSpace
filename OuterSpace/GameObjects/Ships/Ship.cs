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
            base.Update();
        }

        protected override bool BoundryCorrection(BoundingBox boundry)
        {
            return base.BoundryCorrection(boundry);
        }
    }
}
