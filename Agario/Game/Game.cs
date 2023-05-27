using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.GameObjects;
using System.Collections.Generic;
using System;

namespace Agario.Game
{
    class Game
    {
        private RenderWindow scene;

        private Engine.GameLoop gameLoop;

        private int foodVolume;

        private List<Food> listOfFood = new();

        private Vector2u mapSize = new Vector2u(800, 800);

        private Player player;

        public Game(int _foodVolume)
        {
            foodVolume = _foodVolume;
        }
        public void Start()
        {
            scene = new RenderWindow(new VideoMode(mapSize.X, mapSize.Y), "Game window");

            gameLoop = new Engine.GameLoop(scene);

            for (int i = 0; i < foodVolume; i++)
            {
                Food food = new Food(RandomPosition(), scene);

                listOfFood.Add(food);

                gameLoop.RegisterGameObject(food);
            }

            player = new Player(100, scene);
            gameLoop.RegisterGameObject(player);

            gameLoop.Run();
        }

        public Vector2f RandomPosition()
        {
            Random random = new();

            return new Vector2f(random.Next(0, (int)mapSize.X), random.Next(0, (int)mapSize.Y));
        }
    }
}
