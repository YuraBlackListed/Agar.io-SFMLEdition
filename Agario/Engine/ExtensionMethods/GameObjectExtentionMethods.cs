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
            else if (object1.Mesh is Sprite sprite1 && object2.Mesh is CircleShape circle)
            {
                Vector2f spriteCenter = object1.Position;
                Vector2f circleCenter = object2.Position;

                float distance = (float)Math.Sqrt(Math.Pow(circleCenter.X - spriteCenter.X, 2) + Math.Pow(circleCenter.Y - spriteCenter.Y, 2));

                float spriteRadius = Math.Max(sprite1.TextureRect.Width, sprite1.TextureRect.Height) / 2;

                return distance <= spriteRadius + circle.Radius;
            }
            else if (object1.Mesh is CircleShape circle3 && object2.Mesh is Sprite sprite2)
            {
                (object1, object2) = (object2, object1);

                Vector2f spriteCenter = object1.Position;
                Vector2f circleCenter = object2.Position;

                float distance = (float)Math.Sqrt(Math.Pow(circleCenter.X - spriteCenter.X, 2) + Math.Pow(circleCenter.Y - spriteCenter.Y, 2));

                float spriteRadius = Math.Max(sprite2.TextureRect.Width, sprite2.TextureRect.Height) / 2;

                return distance <= spriteRadius + circle3.Radius;
            }
            return false;
        }
    }
}
