using System;
using SFML.Graphics;
using SFML.System;
using Agario.Engine;
using Agario.Engine.Interfaces;

namespace Agario.GameObjects
{
    class Player : GameObject, IInput, IUpdatable, IDrawable
    {
        private float size = 30;

        private CircleShape shape;

        private float speed;

        private InputHandler input;

        public bool IsAI;

        private Vector2f target;

        public Player(float _speed, RenderWindow scene, bool _IsAI) : base(scene)
        {
            IsAI = _IsAI;
            speed = _speed;

            Random random = new();

            Color randomColor = new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

            shape = new CircleShape();
            shape.Radius = size / 2;
            shape.Position = Position;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            shape.FillColor = randomColor;

            Mesh = shape;

            Position = new Vector2f(0, 0);

            target = Position;

            input = new InputHandler(scene);
        }
        public void GetInput()
        {
            if (!IsAI)
            {
                target = input.HandleMousePosition();
            }
        }
        public void Update(float time)
        {
            Move(time);
        }
        private void Move(float time)
        {
            if (IsAI)
            {
                MoveToRandomPoint(time);

            }
            else
            {
                MoveToMouse(time);
            }
        }
        private void MoveToMouse(float time)
        {
            Velocity = new Vector2f(target.X - Position.X, target.Y - Position.Y);

            float distance = (float)Math.Sqrt(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y);

            if (distance > 0)
            {
                Velocity /= distance;
                float playerSpeed = speed * (distance / 100);

                playerSpeed = Math.Min(playerSpeed, speed);
                Position += Velocity * playerSpeed * time;
            }
        }
        private void MoveToRandomPoint(float time)
        {

            if (target == Position)
            {
                target = RandomPosition();
            }

            Velocity = new Vector2f(target.X - Position.X, target.Y - Position.Y);

            float distance = (float)Math.Sqrt(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y);

            if (distance > 0)
            {
                Velocity /= distance;
                float playerSpeed = speed * (distance / 100);

                playerSpeed = Math.Min(playerSpeed, speed);
                playerSpeed = Math.Max(playerSpeed, 10f);
                Position += Velocity * playerSpeed * time;
            }
        }
        public void Grow(float strength)
        {
            size += strength;
            shape.Radius = size / 2;
            Mesh = shape;
        }
        public new void Destroy()
        {
            base.Destroy();
            Game.Agario.playersList.Remove(this);
        }
        public void Draw()
        {
            scene.Draw(Mesh);
        }

        public Vector2f RandomPosition()
        {
            Random random = new();

            return new Vector2f(random.Next(0, 800), random.Next(0, 800));
        }
    }
}
