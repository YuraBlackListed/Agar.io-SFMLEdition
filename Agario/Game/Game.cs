using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.GameObjects;
using Agario.Engine.Interfaces;
using Agario.Engine.ExtensionMethods.GameObjectExtentionMethods;
using System.Collections.Generic;
using System;

namespace Agario.Game
{
    class Game : IUpdatable
    {
        private RenderWindow scene;

        private Engine.GameLoop gameLoop;

        private int foodVolume;

        public static List<Food> foodList = new();
        public static List<Player> playersList = new();

        private Vector2u mapSize = new Vector2u(800, 800);

        private Player player;

        public Game(int _foodVolume)
        {
            foodVolume = _foodVolume;
        }
        public void Start()
        {
            scene = new RenderWindow(new VideoMode(mapSize.X, mapSize.Y), "Game window");

            gameLoop = new Engine.GameLoop(scene, this);

            for (int i = 0; i < foodVolume; i++)
            {
                Food food = new Food(RandomPosition(), scene);

                foodList.Add(food);

                gameLoop.RegisterGameObject(food);
            }

            player = new Player(100, scene);
            playersList.Add(player);
            gameLoop.RegisterGameObject(player);

            gameLoop.Run();
        }
        public void Update(float time)
        {
            for (int playerID = 0; playerID < playersList.Count; playerID++)
            {
                CheckForPlayerCollissions(playerID);
                CheckForFoodCollissions(playerID);
            }
            
        }
        private void CheckForFoodCollissions(int playerID)
        {
            for (int foodID = 0; foodID < foodList.Count; foodID++)
            {
                if (playersList[playerID].CollidesWith(foodList[foodID]))
                {
                    playersList[playerID].Grow(1);
                    foodList[foodID].Destroy();
                }
            }
        }
        private void CheckForPlayerCollissions(int playerID)
        {

        }
        public Vector2f RandomPosition()
        {
            Random random = new();

            return new Vector2f(random.Next(0, (int)mapSize.X), random.Next(0, (int)mapSize.Y));
        }
    }
}
