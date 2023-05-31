﻿using SFML.Graphics;
using SFML.Window;
using Agario.GameObjects;
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

        public static List<Food> foodList = new();
        public static List<Player> playersList = new();

        private Player acivePlayer;

        private InputHandler input;

        public Agario(int _foodVolume, RenderWindow _scene)
        {
            scene = _scene;
            foodVolume = _foodVolume;
            Start();
        }
        private void Start()
        {

            for (int i = 0; i < foodVolume; i++)
            {
                Food food = new Food(scene);

                foodList.Add(food);
            }

            acivePlayer = new Player(100, scene, false);
            playersList.Add(acivePlayer);

            for (int i = 0; i < 6; i++)
            {
                Player _player = new Player(100, scene, true);

                playersList.Add(_player);
            }

            input = new InputHandler(scene);
        }
        public void Update(float time)
        {
            GenerateFood();
            for (int playerID = 0; playerID < playersList.Count; playerID++)
            {
                CheckForPlayerCollissions(playerID);
                CheckForFoodCollissions(playerID);
            }

            if(input.KeyPressed(Keyboard.Key.F))
            {
                Random random = new();

                acivePlayer.IsAI = true;

                Player randomPlayer = playersList[random.Next(0, playersList.Count - 1)];
                randomPlayer.IsAI = false;
                acivePlayer = randomPlayer;
            }
        }
        private void GenerateFood()
        {
            if(foodList.Count < foodVolume)
            {
                Food food = new Food(scene);

                foodList.Add(food);
            }
        }
        private void CheckForFoodCollissions(int playerID)
        {
            for (int foodID = 0; foodID < foodList.Count; foodID++)
            {
                if (playersList[playerID].CollidesWith(foodList[foodID]))
                {
                    playersList[playerID].Grow(0.5f);
                    foodList[foodID].Destroy();
                }
            }
        }
        private void CheckForPlayerCollissions(int playerID)
        {

        }
        
    }
}