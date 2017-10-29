using GameObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderSystem.Interfaces
{
    public interface IRender
    {
        List<IAGameObject> GetGameObjectList();
        void SetupWorldObjects(params IAGameObject[] gameObjects);
        void AddWorldObject(IAGameObject gameObject);
        void AddWorldObjectList(List<IAGameObject> gameObjects);
        void DynamicAdd(IAGameObject gameObject);
        void RemoveWorldObject(IAGameObject gameObject);
        void ClearAll();
        void Render();
        void Render(List<IAGameObject> gameObjects);
    }
}
