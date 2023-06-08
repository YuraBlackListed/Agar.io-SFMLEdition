using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Agario.Engine.Interfaces;
using System.Collections.Generic;
using System.IO;

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

        private GameLoop()
        {
            foodVolume = 200;

            mapSize = new Vector2u(800, 800);

            scene = new RenderWindow(new VideoMode(mapSize.X, mapSize.Y), "Game window");

            input = new InputHandler(scene);

            
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

            game = new Game.Agario(foodVolume, scene);

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

        public static GameLoop NewGameLoop()
        {
            GameLoop gameloop = new GameLoop();

            gameloop.LoadInformationFromFile();

            return gameloop;
        }
        public void LoadInformationFromFile()
        {
            string filePath = @"congifg.cfg";
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                StreamReader reader = new StreamReader(fs);

                string data = reader.ReadLine();

                string[] dataSplit = data.Split(':');

                switch(dataSplit[0]) 
                {
                    case "foodVolume":
                        if (int.TryParse(dataSplit[1], out int newFoodVolume))
                            foodVolume = newFoodVolume;
                        break;
                }
            }
        }
    }
}
