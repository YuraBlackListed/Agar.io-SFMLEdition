using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Agario.Engine.Interfaces;
using System.Collections.Generic;

namespace Agario.Engine
{
    class GameLoop
    {
        private int foodVolume;

        public bool running = false;

        private RenderWindow scene;

        private Clock clock = new Clock();

        public static List<IUpdatable> updatableObjects= new();
        public static List<IDrawable> drawableObjects = new();

        private Vector2u mapSize;

        private Game.Agario game;

        private InputHandler input;

        public GameLoop()
        {
            foodVolume = 200;

            mapSize = new Vector2u(800, 800);

            scene = new RenderWindow(new VideoMode(mapSize.X, mapSize.Y), "Game window");

            input = new InputHandler(scene);

            game = new Game.Agario(foodVolume, scene);
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
            input.CheckInput();
        }
    }
}
