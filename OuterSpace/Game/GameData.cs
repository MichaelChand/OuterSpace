using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Game
{
    public class GameData
    {
        public int ViewPortWidth { get; set; }
        public int ViewPortHeight { get; set; }
        public BoundingBox ViewportBounding { get; set; }
    }
}
