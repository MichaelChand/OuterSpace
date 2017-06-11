using OuterSpace.Game;
using OuterSpace.Game.Input;
using OuterSpace.GameObjects.Armory;
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
        private List<IAGameObject> _weapon;
        private IAGameObject _newWeapon = null;
        private MunitionsFactory _munitionsFactory;
        private int _speed = 10;

        public Player(GameData gameData, IKeyboardInput keyboardInput, List<IAGameObject> weapon)
        {
            _keyboardInput = keyboardInput;
            _gameData = gameData;
            _weapon = weapon;
            PlayerSetup();
        }

        private void PlayerSetup()
        {
            _munitionsFactory = new MunitionsFactory(_gameData);
            _playerShip = new PlayerShip(GetPlayerGameData());
            _playerShip.SpeedChange(_speed);
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

        public IAGameObject GetNewWeapon()
        {
            IAGameObject weapon = _newWeapon;
            _newWeapon = null;
            return weapon;
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
                        _newWeapon = _munitionsFactory.MakeArmament(ArmamentType.Pulsecannon, new Point(_playerShip.GetBoundingBox().Dimension.Left + _playerShip.GetBoundingBox().Dimension.Width / 2, _playerShip.GetBoundingBox().Dimension.Top));
                        _weapon.Add(_newWeapon);
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
