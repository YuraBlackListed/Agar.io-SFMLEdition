namespace Agario.Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLoop gameLoop = GameLoop.NewGameLoop();
            gameLoop.Run();
        }
    }
}
