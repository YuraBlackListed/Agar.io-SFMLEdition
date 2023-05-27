using System;
using SFML.Graphics;
using SFML.System;
using Agario.Engine;
using Agario.Engine.Interfaces;

namespace Agario.GameObjects
{
    class Player : GameObject, IUpdatable, IDrawable
    {
        private CircleShape shape;

        private float speed;

        private InputHandler input;

        public Player(float _speed, RenderWindow scene) : base(scene)
        {
            speed = _speed;

            Random random = new();

            Color randomColor = new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

            shape = new CircleShape();
            shape.Radius = random.Next(30, 50);
            shape.Position = Position;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            shape.FillColor = randomColor;

            Mesh = shape;

            Position = new Vector2f(0, 0);

            input = new InputHandler(scene);
        }
        public void Update(float time)
        {
            MoveToMouse(time);

        }
        private void MoveToMouse(float time)
        {
            Vector2i mousePosition = input.HandleMousePosition();

            Velocity = new Vector2f(mousePosition.X - Position.X, mousePosition.Y - Position.Y);

            float distance = (float)Math.Sqrt(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y);

            if (distance > 0)
            {
                Velocity /= distance;
                float playerSpeed = speed * (distance / 100);

                playerSpeed = Math.Min(playerSpeed, speed);
                Position += Velocity * playerSpeed * time;
            }
        }
        public void Draw()
        {
            scene.Draw(Mesh);
        }
    }
}
