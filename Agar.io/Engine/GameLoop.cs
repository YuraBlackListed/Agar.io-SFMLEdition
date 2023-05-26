using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Agar.io.Engine.Interfaces;
using System.Collections.Generic;

namespace Agar.io.Engine
{
    class GameLoop
    {
        private bool running = false;

        private const int windiwWidth = 1200;
        private const int windowHeight = 600;

        private RenderWindow scene;

        private Clock clock = new Clock();

        public List<IDrawable> drawablesObjects = new();
        public List<IUpdatable> updatableObjects= new();

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

            scene = new RenderWindow(new VideoMode(windiwWidth, windowHeight), "Game window");
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
    }
}
