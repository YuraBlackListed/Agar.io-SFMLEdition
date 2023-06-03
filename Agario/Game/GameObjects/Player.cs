using System;
using SFML.Graphics;
using SFML.System;
using Agario.Engine;
using Agario.Engine.Interfaces;

namespace Agario.Game.GameObjects
{
    class Player : GameObject, IUpdatable, IDrawable
    {
        public float size = 30;
        private float speed;
        private float time;

        public bool IsAI;

        private CircleShape shape;

        private Vector2f target;

        private Random random;

        public Player(float _speed, RenderWindow scene, bool _IsAI) : base(scene)
        {
            Initialize(_speed, scene, _IsAI, RandomPosition());
        }
        public Player(float _speed, RenderWindow scene, bool _IsAI, Vector2f position) : base(scene)
        {
            Initialize(_speed, scene, _IsAI, position);
        }
        private void Initialize(float _speed, RenderWindow scene, bool _IsAI, Vector2f position)
        {
            IsAI = _IsAI;
            speed = _speed;

            random = new();

            Color randomColor = new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

            shape = new CircleShape();
            shape.Radius = size / 2;
            shape.Position = Position;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            shape.FillColor = randomColor;

            Mesh = shape;

            Position = position;

            target = Position;

            InputHandler.MovePlayer += MoveToMouse;
        }
        public void Update(float _time)
        {
            time = _time;
            Move();
        }
        private void Move()
        {
            if (IsAI)
            {
                MoveToRandomPoint();
            }
        }
        private void MoveToMouse(Vector2f lastMousePos)
        {
            if (!IsAI)
            {
                target = lastMousePos;
            }

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
        private void MoveToRandomPoint()
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
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);

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
        public void HandleCollision(Player defender)
        {
            if (size > defender.size)
            {
                Grow(defender.size);
                defender.Destroy();
            }
            else if (size == defender.size)
            {
                int randomPlayerID = random.Next(1, 3);
                switch (randomPlayerID)
                {
                    case 1:
                        Grow(defender.size);
                        defender.Destroy();
                        break;
                    case 2:
                        defender.Grow(size);
                        Destroy();
                        break;
                }
            }
        }
    }
}
