using SFML.System;
using SFML.Graphics;
using Agario.Engine.Interfaces;
using System.Collections.Generic;

namespace Agario.Engine
{
    class GameLoop
    {
        private bool running = false;

        private RenderWindow scene;

        private Clock clock = new Clock();

        public static List<IDrawable> drawablesObjects = new();
        public static List<IUpdatable> updatableObjects= new();

        public GameLoop(RenderWindow _scene)
        {
            scene = _scene;
        }

        public void Run()
        {
            Start();
            while (running)
            {
                Render();
                CheckInput();
                Update();
            }
        }
        private void Start()
        {
            running = true;

            scene.DispatchEvents();
        }
        private void Update()
        {
            Time deltaTime = clock.Restart();
            float seconds = deltaTime.AsSeconds();

            foreach (IUpdatable updatable in updatableObjects)
            {
                updatable.Update(seconds);
            }
        }
        private void Render()
        {
            scene.Clear(Color.Black);

            foreach (IDrawable drawable in drawablesObjects)
            {
                drawable.Draw(scene);
            }

            scene.Display();
        }
        private void CheckInput()
        {

        }

        public void RegisterGameObject(GameObject gameObject)
        {
            if (gameObject is IDrawable)
            {
                drawablesObjects.Add((IDrawable)gameObject);
            }
            if (gameObject is IUpdatable)
            {
                updatableObjects.Add((IUpdatable)gameObject);
            }
        }
        public void UnregisterGameObject(GameObject gameObject)
        {
            if (gameObject is IDrawable)
            {
                drawablesObjects.Remove((IDrawable)gameObject);
            }
            if (gameObject is IUpdatable)
            {
                updatableObjects.Remove((IUpdatable)gameObject);
            }
        }
    }
}
