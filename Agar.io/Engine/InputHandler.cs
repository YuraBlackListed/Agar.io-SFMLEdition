using SFML.Window;

namespace Agar.io.Engine
{
    class InputHandler
    {
        private Keyboard.Key upKey;
        private Keyboard.Key downKey;

        public InputHandler(Keyboard.Key _upKey, Keyboard.Key _downKey)
        {
            upKey = _upKey;
            downKey = _downKey;
        }

        public float HandleInput()
        {
            if (Keyboard.IsKeyPressed(upKey))
            {
                return -300f;
            }
            else if (Keyboard.IsKeyPressed(downKey))
            {
                return 300f;
            }
            else
            {
                return 0f;
            }
        }
    }
}
