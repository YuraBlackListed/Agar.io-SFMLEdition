using SFML.Graphics;
using SFML.Window;
using Agario.Engine.Interfaces;
using Agario.Engine.ExtensionMethods.GameObjectExtentionMethods;
using Agario.Engine.Input;
using System.Collections.Generic;
using System;

namespace Agario.Game
{
    class Agario : IUpdatable
    {
        public RenderWindow scene;

        private int foodVolume;
        private int playerVolume;

        public static List<GameObjects.Food> foodList = new();
        public static List<GameObjects.Player> playersList = new();

        private GameObjects.Player acivePlayer;

        private Random random;

        public Agario(int _foodVolume, int _playerAmount, RenderWindow _scene)
        {
            scene = _scene;
            foodVolume = _foodVolume;
            playerVolume = _playerAmount;

            random = new();
            Start();
        }
        private void Start()
        {
            acivePlayer = new GameObjects.Player(100, scene, false);
            playersList.Add(acivePlayer);

            for (int i = 0; i < playerVolume; i++)
            {
                GameObjects.Player _player = new GameObjects.Player(100, scene, true);

                playersList.Add(_player);
            }
            InputHandler.CreateAction(Keyboard.Key.F, SwapPlayerControll);
            InputHandler.CreateAction(Keyboard.Key.G, GrowPlayer);
        }
        public void Update()
        {
            GenerateFood();
            CheckForPlayerCollissions();
            CheckForFoodCollissions();
        }
        private void GenerateFood()
        {
            if(foodList.Count < foodVolume)
            {
                GameObjects.Food food = new GameObjects.Food(scene);

                foodList.Add(food);
            }
        }
        private void CheckForFoodCollissions()
        {
            for (int playerID = 0; playerID < playersList.Count; playerID++)
            {
                GameObjects.Player atacker = playersList[playerID];
                for (int foodID = 0; foodID < foodList.Count; foodID++)
                {
                    if (playersList.Count > playerID)
                    {
                        if (atacker.CollidesWith(foodList[foodID]))
                        {
                            atacker.Grow(0.5f);
                            foodList[foodID].Destroy();
                        }
                    }
                }
            }
        }
        private void CheckForPlayerCollissions()
        {
            for (int playerID = 0; playerID < playersList.Count; playerID++)
            {
                for (int player2ID = 0; player2ID < playersList.Count; player2ID++)
                {
                    if (playerID != player2ID)
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
