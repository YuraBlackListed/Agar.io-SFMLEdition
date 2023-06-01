using SFML.Graphics;
using SFML.System;

using System;

namespace Agario.Engine.ExtensionMethods.GameObjectExtentionMethods
{
    public static class GameObjectExtentionMethods
    {
        public static bool CollidesWith(this GameObject object1, GameObject object2)
        {
            //return object1.Mesh.GetGlobalBounds().Intersects(object2.Mesh.GetGlobalBounds());

            if(object1.Mesh is CircleShape circle1 && object2.Mesh is CircleShape circle2)
            {
                Vector2f circle1Center = object1.Position;
                Vector2f circle2Center = object2.Position;

                float distance = (float)Math.Sqrt(Math.Pow(circle2Center.X - circle1Center.X, 2) + Math.Pow(circle2Center.Y - circle1Center.Y, 2));

                return distance <= circle1.Radius + circle2.Radius;
            }
            return false;
        }
    }
}
