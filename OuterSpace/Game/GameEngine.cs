﻿using OuterSpace.Common;
using OuterSpace.Game.Levels;
using OuterSpace.GameObjects;
using OuterSpace.RenderSystem;
using System;
using System.Collections.Generic;

namespace OuterSpace.Game
{
    public class GameEngine
    {
        private IRender _renderer;
        private List<ILevel> _levels = new List<ILevel>();
        private Mathematics _maths = new Mathematics();

        private List<IAGameObject> _worldObjects;

        public GameEngine (IRender renderPage)
        {
            Initialise(renderPage);
        }

        public void AddWorldObject(IAGameObject worldObject)
        {
            _renderer.AddWorldObject(worldObject);
            _worldObjects = _renderer.GetGameObjectList();
        }

        public void AddWorldObjects(List<IAGameObject> worldObjects)
        {
            _renderer.AddWorldObjects(worldObjects);
            _worldObjects = _renderer.GetGameObjectList();
        }

        private void Initialise(IRender renderPage)
        {
            _renderer = renderPage;
        }

        private void RenderUpdate()
        {
            _renderer.Render();
        }
        
        public void Update()
        {
            for (int i = _worldObjects.Count - 1; i >= 0; i--)
                _worldObjects[i].Update();
            RenderUpdate();
        }

        private void CleanUp()
        {
            //_keyboardInput?.Dispose();
        }
    }
}
