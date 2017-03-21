using OuterSpace.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Game.Levels
{
    public interface ILevel
    {
        void Load();
        List<IAGameObject> GetLevelObjects();
        void Update();
    }
}
