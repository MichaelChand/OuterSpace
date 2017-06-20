using OuterSpace.Game;
using OuterSpace.GameObjects.Armory.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.GameObjects.Armory
{
    public class MunitionsFactory
    {
        private GameData _gameData;

        public MunitionsFactory(GameData gameData)
        {
            _gameData = gameData;
        }

        public IAGameObject MakeArmament(ArmamentType armamentType, Point firedFromPosition)
        {
            switch(armamentType)
            {
                case ArmamentType.Pulsecannon:
                    return new PulseCannon(_gameData, firedFromPosition);
                case ArmamentType.Missile:
                    return new Missile(_gameData, firedFromPosition);
                default :
                    return null;
            }
        }
    }
}
