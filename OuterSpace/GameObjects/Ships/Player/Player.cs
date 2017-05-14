using OuterSpace.Common;
using OuterSpace.Game;
using OuterSpace.Physics;
using OuterSpace.RenderSystem;
using ReConInvaders.Inputsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ReConInvaders.Inputsystem.KeyboardInput;

namespace OuterSpace.GameObjects.Ships.Player
{
    public class Player
    {
        private PlayerShip _playerShip;
        private GameData _gameData;
        private int _debugCounter_ = 0;

        public Player(GameData gameData)
        {
            _gameData = gameData;
            PlayerSetup();
        }

        private void PlayerSetup()
        {
            _playerShip = new PlayerShip(GetPlayerGameData());
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

        private void UpdateAction(List<KeypressType> keys)
        {
            Action<KeypressType> ManageAction = (key) =>
            {
                switch(key)
                {
                    case KeypressType.Space :
                        Console.WriteLine(string.Format("{0} : SPACE! The final frontier...", _debugCounter_++));
                        break;
                }
            };

            foreach (var key in keys)
                ManageAction(key);
        }

        private void UpdateMovement(KeypressType key)
        {
            switch (key)
            {
                case KeypressType.Left:
                    _playerShip.MoveLeft();
                    break;
                case KeypressType.Right:
                    _playerShip.MoveRight();
                    break;
            }
        }

        public void Update(KeypressType moveKey, List<KeypressType> actionKeys)
        {
            UpdateMovement(moveKey);
            UpdateAction(actionKeys);
            _playerShip.Update();
        }
    }
}
