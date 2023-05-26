using SFML.Graphics;
using SFML.System;
using System;
using Agario.Engine;
using Agario.Engine.Interfaces;

namespace Agario.GameObjects
{
    class Food : GameObject, IDrawable
    {
		public CircleShape shape;

		private float radius;

		public Food(Vector2f position, RenderWindow window) : base(window)
		{
			Random random = new();

			Color randomColor = new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

			radius = random.Next(3, 5);

			shape = new CircleShape(radius);
			shape.Position = position;
			shape.Origin = new Vector2f(radius, radius);
			shape.FillColor = randomColor;
		}

		public void Draw(RenderWindow scene)
		{
			scene.Draw(shape);
		}

	}
}
