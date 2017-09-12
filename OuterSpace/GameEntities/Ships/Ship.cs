using CommonRelay.DataObjects;
using OuterSpace.GameObjects.Armory;
using System.Windows;

namespace OuterSpace.GameObjects.Ships
{
    public class Ship : GameObject
    {

        public double Strength { get; set; }
        public bool Alive { get; set; }
        public ArmoryType _armoryType { get; set; }
        public ArmamentType _armamentType { get; set; }
        public int FrameTimeStamp = 0;
        public double FiringClock;
        public double FireAngle;

        public override void Update()
        {
            base.Update();
        }

        protected override bool BoundryCorrection(BoundingBox boundry)
        {
            return base.BoundryCorrection(boundry);
        }

        public Point GetPosition()
        {
            return new Point(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top);
        }

        public Point GetPositionOfCentre()
        {
            return new Point(_boundingBox.Dimension.Left + (_boundingBox.Dimension.Width/2), _boundingBox.Dimension.Top + (_boundingBox.Dimension.Height / 2));
        }

        public Size GetSize()
        {
            return new Size(_boundingBox.Dimension.Width, _boundingBox.Dimension.Height);
        }
    }
}
