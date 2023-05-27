using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Agario
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Game game = new Game.Game(1000);
            game.Start();


        }
    }
}
