using SFML.System;
using SFML.Graphics;

namespace Agar.io.Engine
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

        private RenderWindow scene;

        public GameObject(RenderWindow window)
        {
            scene = window;
        }

        public void Draw()
        {
            scene.Draw(Mesh);
        }
    }

}
