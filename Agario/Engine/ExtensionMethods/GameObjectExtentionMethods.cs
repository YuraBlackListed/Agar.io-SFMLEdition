namespace Agario.Engine.ExtensionMethods.GameObjectExtentionMethods
{
    public static class GameObjectExtentionMethods
    {
        public static bool CollidesWith(this GameObject object1, GameObject object2)
        {
            return object1.Mesh.GetGlobalBounds().Intersects(object2.Mesh.GetGlobalBounds());
        }
    }
}
