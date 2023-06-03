using SFML.Graphics;
using Agario.Engine;
using Agario.Engine.Interfaces;
using Agario.Engine.ExtensionMethods.GameObjectExtentionMethods;
using System.Collections.Generic;
using System;

namespace Agario.Game
{
    class Agario : IUpdatable
    {
        public RenderWindow scene;

        private int foodVolume;

        public static List<GameObjects.Food> foodList = new();
        public static List<GameObjects.Player> playersList = new();

        private GameObjects.Player acivePlayer;

        private Random random;

        public Agario(int _foodVolume, RenderWindow _scene)
        {
            scene = _scene;
            foodVolume = _foodVolume;

            random = new();
            Start();
        }
        private void Start()
        {
            acivePlayer = new GameObjects.Player(100, scene, false);
            playersList.Add(acivePlayer);

            for (int i = 0; i < 6; i++)
            {
                GameObjects.Player _player = new GameObjects.Player(100, scene, true);

                playersList.Add(_player);
            }
            InputHandler.SwapPlayersAction += SwapPlayerControll;
            InputHandler.GrowPlayersAction += GrowPlayer;
        }
        public void Update(float time)
        {
            GenerateFood();
            for (int playerID = 0; playerID < playersList.Count; playerID++)
            {
                CheckForPlayerCollissions(playerID);
                CheckForFoodCollissions(playerID);
            }
            CheckForFoodCollissions(0);
        }
        private void GenerateFood()
        {
            if(foodList.Count < foodVolume)
            {
                GameObjects.Food food = new GameObjects.Food(scene);

                foodList.Add(food);
            }
        }
        private void CheckForFoodCollissions(int playerID)
        {
            for (int foodID = 0; foodID < foodList.Count; foodID++)
            {
                if(playersList.Count > playerID )
                {
                    if (playersList[playerID].CollidesWith(foodList[foodID]))
                    {
                        playersList[playerID].Grow(0.5f);
                        foodList[foodID].Destroy();
                    }
                }
            }
        }
        private void CheckForPlayerCollissions(int playerID)
        {
            for (int player2ID = 0; player2ID < playersList.Count; player2ID++)
            {
                if(playerID != player2ID)
                {
                    GameObjects.Player atacker = playersList[playerID];
                    GameObjects.Player defender = playersList[player2ID];
                    if (atacker.CollidesWith(defender))
                    {
                        atacker.HandleCollision(defender);
                    }
                }
            }
        }
        private void SwapPlayerControll()
        {
            acivePlayer.IsAI = true;

            GameObjects.Player randomPlayer = playersList[random.Next(0, playersList.Count - 1)];
            randomPlayer.IsAI = false;

            acivePlayer = randomPlayer;
        }
        private void GrowPlayer()
        {
            acivePlayer.Grow(0.3f);
        }
    }
}
