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

namespace OuterSpace.GameObjects.Armory
{
    public class Armory : GameObject, IArmory
    {

        public int Strength { get; protected set; }
        protected ArmoryType _armoryType; 
        protected int _magnetude = 1;
        protected double _angle = 180;
        protected Mathematics _maths = new Mathematics();
        protected Point _firedFromPosition;
        protected double _width;
        protected double _height;
        protected bool _isActive = false;

        public Armory(GameData gameData, Point firedFromPosition)
        {
            Initialise(gameData, firedFromPosition);
        }

        private void Initialise(GameData gameData, Point firedFromPosition)
        {
            _gameData = gameData;
            _firedFromPosition = firedFromPosition;
        }

        public override void Update()
        {
            Move();
        }

        public virtual void Move()
        {
            Point p = _maths.GetXY(_angle, 1);
            _boundingBox.Dimension = new Box{ Left = _boundingBox.Dimension.Left + p.X, Top = _boundingBox.Dimension.Top + p.Y, Width = _boundingBox.Dimension.Width, Height = _boundingBox.Dimension.Height};
        }
    }
}
