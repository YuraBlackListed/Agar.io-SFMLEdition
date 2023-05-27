using SFML.System;
using SFML.Graphics;
using Agario.Engine.Interfaces;
using Agario.Engine;

namespace Agario.Engine
{
    public class GameObject
    {
        private Vector2f position = new Vector2f(0, 0);
        public Vector2f Position
        {
            get { return position; }
            set { position = value; Mesh.Position = position; }
        }

        public Vector2f Velocity = new Vector2f(0, 0);

        public Shape Mesh;

        internal RenderWindow scene;

        public GameObject(RenderWindow window)
        {
            scene = window;
        }

        internal void Destroy()
        {
            if (this is IUpdatable updatable)
            {
                GameLoop.updatableObjects.Remove(updatable);
            }

            if (this is IDrawable drawable)
            {
                GameLoop.drawablesObjects.Remove(drawable);
            }
        }

    }

}
