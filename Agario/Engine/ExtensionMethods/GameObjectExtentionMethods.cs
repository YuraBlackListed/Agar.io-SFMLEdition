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
            Vector2f object1Center = object1.Position;
            Vector2f object2Center = object2.Position;

            if (object1.Mesh is CircleShape circle1 && object2.Mesh is CircleShape circle2)
            {
                float distance = (float)Math.Sqrt(Math.Pow(object2Center.X - object1Center.X, 2) + Math.Pow(object2Center.Y - object1Center.Y, 2));

                return distance <= circle1.Radius + circle2.Radius;
            }
            else if (object1.Mesh is Sprite sprite1 && object2.Mesh is CircleShape circle)
            {
                float distance = (float)Math.Sqrt(Math.Pow(object2Center.X - object1Center.X, 2) + Math.Pow(object2Center.Y - object1Center.Y, 2));

                float spriteRadius = Math.Max(sprite1.TextureRect.Width, sprite1.TextureRect.Height) / 2;

                return distance <= spriteRadius + circle.Radius;
            }
            else if (object1.Mesh is CircleShape circle3 && object2.Mesh is Sprite sprite2)
            {
                float distance = (float)Math.Sqrt(Math.Pow(object1Center.X - object2Center.X, 2) + Math.Pow(object1Center.Y - object2Center.Y, 2));

                float spriteRadius = Math.Max(sprite2.TextureRect.Width, sprite2.TextureRect.Height) / 2;

                return distance <= spriteRadius + circle3.Radius;
            }
            return false;
        }
    }
}
