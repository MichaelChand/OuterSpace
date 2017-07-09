
using System;

namespace CommonRelay.DataObjects
{
    public class GameData
    {
        public int ViewPortWidth { get; set; }
        public int ViewPortHeight { get; set; }
        public BoundingBox ViewportBounding { get; set; }
        public int FramesPerSecond;
        public Action<string[]> WriteToConsole;
    }
}
