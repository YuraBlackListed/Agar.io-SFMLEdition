using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario.Engine
{
    class InputHandler
    {
        RenderWindow scene;

        public InputHandler(RenderWindow _scene)
        {
            scene = _scene;
        }
        public Vector2i HandleMousePosition() 
        {
            Vector2i mousePosition = Mouse.GetPosition(scene);
            return mousePosition;
        }
    }
}
