using CommonRelay.Common;
using CommonRelay.DataObjects;
using GameObjects.Interfaces;
using OuterSpace.GameObjects;
using OuterSpace.GameObjects.Armory;
using OuterSpace.GameObjects.Ships;
using OuterSpace.GameObjects.Ships.Enemy;
using System;
using System.Collections.Generic;

namespace OuterSpace.Game
{
    public class AiManager
    {
        private IAGameObject _aiPlayer;
        private Random _random;
        private int _firingFrequency;
        private int highRange = 5;
        private long timeStamp;
        private List<IAGameObject> _aiWeapons;
        private GameData _gameData;
        private MunitionsFactory _munitionsFactory;
        private Mathematics _maths;

        public AiManager(GameData gameData, List<IAGameObject> aiWeapons, MunitionsFactory munitionsFactory)
        {
            _munitionsFactory = munitionsFactory;
            _gameData = gameData;
            _aiWeapons = aiWeapons;
            ManagerInitialise();
            _maths = new Mathematics();
        }

        private void ManagerInitialise()
        {
            _random = new Random();
        }

        public void SetAi(IAGameObject ai)
        {
            _aiPlayer = ai;
        }

        private bool IsTimeUp()
        {
            long time = DateTime.Now.Millisecond;
            if (time - timeStamp >= _firingFrequency)
                return true;
            return false;
        }

        private void Fire()
        {
            IAGameObject weapon = _munitionsFactory.MakeArmament((_aiPlayer as Ship)._armamentType, (_aiPlayer as Ship).GetPositionOfCentre());
            (weapon as Armory).Fired = false;
            _aiWeapons.Add(weapon);
            (_aiPlayer as Ship).FrameTimeStamp = 0;
            (_aiPlayer as EnemyShip).AdjustFiringClock();
        }

        public void Update()
        {
            double time = _maths.FramesToSeconds((_aiPlayer as Ship).FrameTimeStamp++, _gameData.FramesPerSecond);
            if (_maths.FramesToSeconds((_aiPlayer as Ship).FrameTimeStamp++, _gameData.FramesPerSecond) >= _maths.FramesToSeconds((int)(_aiPlayer as Ship).FiringClock, _gameData.FramesPerSecond))
                Fire();
        }
    }
}
