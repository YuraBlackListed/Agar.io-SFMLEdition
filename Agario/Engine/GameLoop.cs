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

        public static List<IInput> inputableObjects= new();
        public static List<IUpdatable> updatableObjects= new();
        public static List<IDrawable> drawableObjects = new();

        private IUpdatable game;

        public GameLoop(RenderWindow _scene, IUpdatable _game)
        {
            game = _game;
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

            game.Update(seconds);

            scene.DispatchEvents();
        }
        private void Render()
        {
            scene.Clear(Color.Black);

            foreach (IDrawable drawable in drawableObjects)
            {
                drawable.Draw();
            }

            scene.Display();
        }
        private void CheckInput()
        {
            foreach (IInput inputable in inputableObjects)
            {
                inputable.GetInput();
            }
        }
    }
}
