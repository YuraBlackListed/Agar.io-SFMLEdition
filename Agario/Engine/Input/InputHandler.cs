using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Agario.Engine.Input
{
    class InputHandler
    {
        private RenderWindow scene;

        public static Action<Vector2f> MovePlayer;

        private static List<KeyAction> keyActions = new List<KeyAction>();

        public InputHandler(RenderWindow _scene)
        {
            scene = _scene;
        }

        public void CheckInput()
        {
            MovePlayer.Invoke(HandleMousePosition());

            foreach (var action in keyActions)
            {
                action.CheckInput();
            }
        }
        private Vector2f HandleMousePosition()
        {
            Vector2f mousePosition = (Vector2f)Mouse.GetPosition(scene);
            return mousePosition;
        }

        public static void CreateAction(Keyboard.Key key, Action action)
        {
            keyActions.Add(new KeyAction(key, action));
        }
        public static void RemoveAction(Keyboard.Key bind)
        {
            for (int i = 0; i < keyActions.Count; i++)
            {
                if (keyActions[i].key == bind)
                {
                    keyActions.RemoveAt(i);
                }
            }
        }

    }

}
