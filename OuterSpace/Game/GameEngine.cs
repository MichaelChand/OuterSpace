using CommonRelay.Common;
using GameObjects.Interfaces;

using RenderSystem;
using RenderSystem.Interfaces;
using System.Collections.Generic;

namespace OuterSpace.Game
{
    public class GameEngine
    {
        private IRender _renderer;
        private Mathematics _maths = new Mathematics();

        private List<IAGameObject> _worldObjects;

        public GameEngine (IRender renderPage)
        {
            Initialise(renderPage);
        }

        private void Initialise(IRender renderPage)
        {
            _renderer = renderPage;
        }

        public void AddWorldObject(IAGameObject worldObject)
        {
            _renderer.AddWorldObject(worldObject);
            _worldObjects = _renderer.GetGameObjectList();
        }

        public void AddWorldObjects(List<IAGameObject> worldObjects)
        {
            _renderer.AddWorldObjectList(worldObjects);
            _worldObjects = _renderer.GetGameObjectList();
        }

        public void DynamicAdd(IAGameObject gameObject)
        {
            _renderer.DynamicAdd(gameObject);
        }

        public void Render()
        {
            _renderer.Render();
        }
        
        public void Update()
        {
            for (int i = _worldObjects.Count - 1; i >= 0; i--)
                _worldObjects[i].Update();
        }
    }
}
