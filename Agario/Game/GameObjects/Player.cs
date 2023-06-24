using SFML.Graphics;
using SFML.System;
using Agario.Engine;
using Agario.Engine.Interfaces;
using Agario.Engine.Input;
using System;

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

            Color color;
            if (IsAI)
            {
                color = new Color(0, 0, 0);
            }
            else
            {
                color = new Color(250, 0, 0);
            }

            shape = new CircleShape();
            shape.Radius = size / 2;
            shape.Position = Position;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            shape.FillColor = color;

            Mesh = shape;

            Position = position;

            target = Position;

            InputHandler.MovePlayer += Move;
        }
        public void Update()
        {
            time = Time.deltaTime;
        }
        private void Move(Vector2f lastMousePos)
        {
            if (IsAI)
            {
                MoveToRandomPoint();
            }
            else
            {
                MoveToMouse(lastMousePos);
            }
        }
        private void MoveToMouse(Vector2f lastMousePos)
        {

            target = lastMousePos;


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
            if (Mesh is Shape)
            {
                scene.Draw((Shape)Mesh);
            }
            else if (Mesh is Sprite)
            {
                scene.Draw((Sprite)Mesh);
            }
        }
        public void HandleCollision(Player defender)
        {
            if (size > defender.size)
            {
                IGotEatenBy(defender);
            }
            else if (size == defender.size)
            {
                int randomPlayerID = random.Next(1, 3);
                switch (randomPlayerID)
                {
                    case 1:
                        SomebodyGotEaten(defender);
                        break;
                    case 2:
                        IGotEatenBy(defender);
                        break;
                }
            }
        }
        private void IGotEatenBy(Player player)
        {
            player.Grow(size);
            //you can put any sound for this case
            AudioSystem.PlaySoundOnce("EatPlayer");
            Destroy();
        }
        private void SomebodyGotEaten(Player player)
        {
            AudioSystem.PlaySoundOnce("EatPlayer");
            player.Destroy();
        }
    }
}
