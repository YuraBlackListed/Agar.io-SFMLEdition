using System.Diagnostics;

namespace Agario.Engine
{
    public static class Time
    {
        public static float deltaTime;
        private static Stopwatch timer = new Stopwatch();

        public static void Start()
        {
            timer.Start();
            deltaTime = 0;
        }

        public static void Update()
        {
            deltaTime = (float)timer.Elapsed.TotalMilliseconds * 0.001f;
            timer.Restart();
        }
    }
}
