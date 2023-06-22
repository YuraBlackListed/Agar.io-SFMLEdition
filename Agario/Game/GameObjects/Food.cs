using SFML.Graphics;
using SFML.System;
using Agario.Engine;
using Agario.Engine.Interfaces;
using Agario.Engine.ExtensionMethods.AnimationExtensionMethods;

namespace Agario.Game.GameObjects
{
    class Food : GameObject, IUpdatable, IDrawable
    {
		private Sprite shape;

		private float radius;

		private Animation animation;

		private float frameTime = 0.2f;
		private float frameTimer;

		public Food(RenderWindow scene) : base(scene)
		{
			Initialize(RandomPosition());
		}
		public Food(RenderWindow scene, Vector2f position) : base(scene)
		{
			Initialize(position);
		}

		private void Initialize(Vector2f position)
		{
			radius = 3;

            animation = new Animation("Food");

            shape = new Sprite(animation.frames[0]);

			UpdateMesh(0.5f);

            Position = position;
        }
		public new void Destroy()
        {
			base.Destroy();
			animation.Destroy();

            Game.Agario.foodList.Remove(this);
		}
		public void Update()
		{
            frameTimer += Time.deltaTime;
            if (frameTimer < frameTime)
                return;
            UpdateMesh(0.5f);
            frameTimer = 0;
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
		private void UpdateMesh(float scale)
		{
            shape = shape.SwapFrame(animation);
            shape.Origin = new Vector2f(radius, radius);
            shape.Position = Position;

            Mesh = shape;
            Mesh.Scale = new Vector2f(scale, scale);
        }
	}
}
