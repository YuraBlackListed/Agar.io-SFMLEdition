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

        public Game(int _foodVolume)
        {
            foodVolume = _foodVolume;
        }
        public void Run()
        {
            scene = new RenderWindow(new VideoMode(800, 800), "Game window");

            gameLoop = new Engine.GameLoop(scene);

            for (int i = 0; i < foodVolume; i++)
            {
                Food food = new Food(RandomPosition(), scene);

                listOfFood.Add(food);

                gameLoop.RegisterGameObject(food);
            }
            
            gameLoop.Run();
        }
        public Vector2f RandomPosition()
        {
            Random random = new();

            return new Vector2f(random.Next(0, (int)mapSize.X), random.Next(0, (int)mapSize.Y));
        }
    }
}
