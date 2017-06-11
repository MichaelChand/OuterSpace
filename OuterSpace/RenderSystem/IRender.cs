using OuterSpace.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.RenderSystem
{
    public interface IRender
    {
        List<IAGameObject> GetGameObjectList();
        void SetupWorldObjects(params IAGameObject[] gameObjects);
        void AddWorldObject(IAGameObject gameObject);
        void AddWorldObjects(List<IAGameObject> gameObjects);
        void RemoveWorldObject(IAGameObject gameObject);
        void Render();
        void Render(List<IAGameObject> gameObjects);
    }
}
