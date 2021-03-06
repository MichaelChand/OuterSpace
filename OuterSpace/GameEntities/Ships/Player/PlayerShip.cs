﻿
using CommonRelay.Common;
using CommonRelay.DataObjects;
using System.Windows;

namespace OuterSpace.GameObjects.Ships.Player
{
    public class PlayerShip : Ship
    {
        private int _width = 50;
        private int _height = 50;
        private int _speed = 10;
        private Point _position;
        private double _strength = 50;

        public PlayerShip(GameData gameData)
        {
            _texturePath = "Assets//Images//SamplePlayerShip.png";
            _gameData = gameData;
            Initialise();
        }

        private void Initialise()
        {
            Strength = _strength;
            Alive = true;
            _boundingBox = new BoundingBox(new Box { Left = 0, Top = 0, Width = _width, Height = _height });
            _gameObjectDim = new Size(_boundingBox.Dimension.Width, _boundingBox.Dimension.Height);
            SetStartPosition();
            SetupGameObject();
            ZIndex = 100;
            Update();
        }

        private void SetStartPosition()
        {
            SetStartPositionX();
            SetStartPositionY();
            _position = new Point(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top);
        }

        private void SetStartPositionX()
        {
            _boundingBox.Dimension.Left = _gameData.ViewportBounding.Dimension.Left + 50;
        }

        private void SetStartPositionY()
        {
            _boundingBox.Dimension.Top = _gameData.ViewportBounding.Dimension.Top + _gameData.ViewportBounding.Dimension.Height - 50;
        }

        public void SpeedChange(int speed)
        {
            _speed = speed;
        }

        public void MoveUp()
        {
            _position = new Point(_position.X, _position.Y - _speed);
        }

        public void MoveDown()
        {
            _position = new Point(_position.X, _position.Y + _speed);
        }

        public void MoveLeft()
        {
            _position = new Point(_position.X - _speed, _position.Y);
        }

        public void MoveRight()
        {
            _position = new Point(_position.X + _speed, _position.Y);
        }

        public override void Update()
        {
            _boundingBox.Dimension = new Box { Left = _position.X, Top = _position.Y, Width = _boundingBox.Dimension.Width, Height = _boundingBox.Dimension.Height };
            if (BoundryCorrection(_gameData.ViewportBounding))
                _position = new Point(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top);
        }
    }
}
