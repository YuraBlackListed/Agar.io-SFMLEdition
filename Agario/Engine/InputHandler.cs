using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario.Engine
{
    class InputHandler
    {
        private RenderWindow scene;

        public InputHandler(RenderWindow _scene)
        {
            scene = _scene;
        }

        public bool KeyPressed(Keyboard.Key key)
        {
            if (Keyboard.IsKeyPressed(key))
            {
                return true;
            }
            return false;
        }

        public Vector2f HandleMousePosition()
        {
            Vector2f mousePosition = (Vector2f)Mouse.GetPosition(scene);
            return mousePosition;
        }
    }

}
