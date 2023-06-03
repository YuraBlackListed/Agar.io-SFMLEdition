using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Agario.Engine
{
    class InputHandler
    {
        private RenderWindow scene;

        public static Action SwapPlayersAction;
        public static Action GrowPlayersAction;
        public static Action<Vector2f> MovePlayer;

        private Keyboard.Key swapPlayersKey = Keyboard.Key.F;
        private Keyboard.Key growPlayersKey = Keyboard.Key.G;

        public InputHandler(RenderWindow _scene)
        {
            scene = _scene;
        }

        public void CheckInput()
        {
            HandleInput();
        }
        private bool KeyPressed(Keyboard.Key key)
        {
            if (Keyboard.IsKeyPressed(key))
            {
                return true;
            }
            return false;
        }
        private void HandleInput()
        {
            if (KeyPressed(swapPlayersKey))
            {
                SwapPlayersAction.Invoke();
            }
            if (KeyPressed(growPlayersKey))
            {
                GrowPlayersAction.Invoke();
            }
            MovePlayer.Invoke(HandleMousePosition());
        }
        private Vector2f HandleMousePosition()
        {
            Vector2f mousePosition = (Vector2f)Mouse.GetPosition(scene);
            return mousePosition;
        }
    }

}
