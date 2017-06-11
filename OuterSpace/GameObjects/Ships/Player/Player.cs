﻿using OuterSpace.Common;
using OuterSpace.Game;
using OuterSpace.Game.Input;
using OuterSpace.Physics;
using OuterSpace.RenderSystem;
using ReConInvaders.Inputsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ReConInvaders.Inputsystem.KeyboardInput;

namespace OuterSpace.GameObjects.Ships.Player
{
    public class Player
    {
        private PlayerShip _playerShip;
        private GameData _gameData;
        private int _debugCounter_ = 0;
        private IKeyboardInput _keyboardInput;

        public Player(GameData gameData, IKeyboardInput keyboardInput)
        {
            _keyboardInput = keyboardInput;
            _gameData = gameData;
            PlayerSetup();
        }

        private void PlayerSetup()
        {
            _playerShip = new PlayerShip(GetPlayerGameData());
            _playerShip.SpeedChange(1);
        }

        private GameData GetPlayerGameData()
        {
            GameData gameData = new GameData();
            gameData.FramesPerSecond = _gameData.FramesPerSecond;
            gameData.ViewportBounding = new BoundingBox(new Point(0, 0), _gameData.ViewPortWidth, _gameData.ViewPortHeight);
            gameData.ViewPortHeight = _gameData.ViewPortHeight;
            gameData.ViewPortWidth = _gameData.ViewPortWidth;
            return gameData;
        }

        public IAGameObject GetPlayerObject()
        {
            return _playerShip;
        }

        private void UpdateAction()
        {
            List<Key?> keys = _keyboardInput.GetActiveKeys();

            Action<Key?> ManageAction = (key) =>
            {
                switch(key)
                {
                    case Key.Left:
                        _playerShip.MoveLeft();
                        break;
                    case Key.Right:
                        _playerShip.MoveRight();
                        break;
                    case Key.Up:
                        _playerShip.MoveUp();
                        break;
                    case Key.Down:
                        _playerShip.MoveDown();
                        break;
                    case Key.Space :
                        Console.WriteLine(string.Format("{0} : SPACE! The final frontier...", _debugCounter_++));
                        break;
                }
            };

            for(int i = keys.Count - 1; i >= 0; i--)
                ManageAction(keys[i]);
        }

        public void Update()
        {
            UpdateAction();
            _playerShip.Update();
        }
    }
}
