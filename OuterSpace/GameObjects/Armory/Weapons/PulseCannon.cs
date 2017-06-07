using OuterSpace.Game;
using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.GameObjects.Armory.Weapons
{
    public class PulseCannon : Armory
    {
        private int _speed = 1;

        public PulseCannon(GameData gameData, Point firedFromPosition) : base(gameData, firedFromPosition)
        {
            Initialise();
        }

        private void Initialise()
        {
            _width = 10;
            _height = 60;
            _magnetude = _speed;
            _texturePath = "Assets//Images//SampleBlank.png";
            SetStartPosition();
            _isActive = true;
            _gameObjectDim = new Size(_boundingBox.Dimension.Width, _boundingBox.Dimension.Height);
            SetupGameObject();
            if (_gameData.ViewportBounding.Dimension.Top + _gameData.ViewportBounding.Dimension.Height > _firedFromPosition.Y)
                _angle = 90;
            else _angle = -90;
            Update();
        }

        private void SetStartPosition()
        {
            _boundingBox = new BoundingBox(new Box { Left = _firedFromPosition.X, Top = _firedFromPosition.Y, Width = _width, Height = _height });
        }

        public override void Update()
        {
            if (_isActive && _boundingBox.Dimension.Top <= _gameData.ViewPortHeight && _boundingBox.Dimension.Top + _boundingBox.Dimension.Height >= 0 && _boundingBox.Dimension.Left + _boundingBox.Dimension.Width > 0 && _boundingBox.Dimension.Left < _gameData.ViewPortWidth)
                base.Update();
            else _isActive = false;
        }

    }
}
