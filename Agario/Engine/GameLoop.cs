global using Time = Agario.Engine.Time;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Agario.Engine.Interfaces;
using Agario.Engine.ExtensionMethods.PathExtentionMethods;
using Agario.Engine.Input;
using System.Collections.Generic;
using System.IO;
using System;

namespace Agario.Engine
{
    class GameLoop
    {
        private int foodVolume;
        private int playerAmount;

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
            playerAmount = 6;

            mapSize = new Vector2u(800, 800);
        }

        public void Run()
        {
            Start();
            while (scene.IsOpen)
            {
                Render();
                CheckInput();
                Update();
            }
        }
        private void Start()
        {
            scene = new RenderWindow(new VideoMode(mapSize.X, mapSize.Y), "Game window");
            scene.Closed += (sender, e) => scene.Close();

            input = new InputHandler(scene);

            game = new Game.Agario(foodVolume, playerAmount, scene);

            scene.DispatchEvents();

            Time.Start();
        }
        private void Update()
        {
            Time.Update();

            foreach (IUpdatable updatable in updatableObjects)
            {
                updatable.Update();
            }

            game.Update();

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
            string currentDirectory = PathExtentionMethods.GetFontDirectory();
            string filePath = currentDirectory + "/Engine/" + @"congifg.cfg";
            Console.WriteLine(filePath);
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(fs);

                    string data;
                    for (data = "1"; data != null; data = reader.ReadLine())
                    {
                        string[] dataSplit = data.Split(':');

                        switch (dataSplit[0])
                        {
                            case "foodVolume":
                                if (int.TryParse(dataSplit[1], out int newFoodVolume))
                                    foodVolume = newFoodVolume;
                                break;
                            case "mapSize":
                                if (uint.TryParse(dataSplit[1], out uint x))
                                    mapSize.X = x;
                                if (uint.TryParse(dataSplit[2], out uint y))
                                    mapSize.Y = y;
                                break;
                            case "playerAmount":
                                if (int.TryParse(dataSplit[1], out int newPlayerAmount))
                                    playerAmount = newPlayerAmount;
                                break;
                        }
                    }
                }
            }
        }
    }
}
