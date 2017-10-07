using CommonRelay.DataObjects;
using OuterSpace.Game.Loaders;
using OuterSpace.GameObjects.Ships.Enemy;
using OuterSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.GameEntities.Ships.Enemy
{
    public class AiFactory
    {
        private EnemyShipParser _enemyShipParser;
        private GameData _gameData;

        public AiFactory(EnemyShipParser enemyShipParser, GameData gameData)
        {
            _enemyShipParser = enemyShipParser;
            _gameData = gameData;
        }
        
        private EnemyShip FindShip(int id, List<AiModel> aiModelList)
        {
            return new AiShip((from aiModel in aiModelList
                               where aiModel.ID == id
                               select aiModel).FirstOrDefault(), _gameData);
        }

        public EnemyShip GetAi(int id)
        {
            List<AiModel> aiModelList = _enemyShipParser.GetEnemiesList();
            return FindShip(id, aiModelList);
        }
    }
}
