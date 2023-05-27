using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.Engine;
using Agario.Engine.Interfaces;

namespace Agario.GameObjects
{
    class Player : GameObject, IUpdatable, IDrawable
    {
        private CircleShape shape;
        private float speed;
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
        }
        public void Update(float time)
        {
            Vector2i mousePosition = Mouse.GetPosition(scene);
            Vector2f direction = new Vector2f(mousePosition.X - Position.X, mousePosition.Y - Position.Y);
            float distance = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

            if (distance > 0)
            {
                direction /= distance;
                float playerSpeed = speed * (distance / 100);

                playerSpeed = Math.Min(playerSpeed, speed);
                Position += direction * playerSpeed * time;
            }

        }
        public void Draw()
        {
            scene.Draw(Mesh);
        }
    }
}
